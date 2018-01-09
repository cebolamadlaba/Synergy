using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Imports the SAP data
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class ImportSapData : IDailyScheduledJob
    {
        /// <summary>
        /// The sap data import configuration repository
        /// </summary>
        private readonly ISapDataImportConfigurationRepository _sapDataImportConfigurationRepository;

        /// <summary>
        /// The email manager
        /// </summary>
        private readonly IEmailManager _emailManager;

        /// <summary>
        /// The sap data import repository
        /// </summary>
        private readonly ISapDataImportRepository _sapDataImportRepository;

        /// <summary>
        /// The file utiltity
        /// </summary>
        private readonly IFileUtiltity _fileUtiltity;

        /// <summary>
        /// The background job client
        /// </summary>
        private readonly IBackgroundJobClient _backgroundJobClient;

        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportSapData"/> class.
        /// </summary>
        /// <param name="sapDataImportConfigurationRepository">The sap data import configuration repository.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="sapDataImportRepository">The sap data import repository.</param>
        /// <param name="fileUtiltity">The file utiltity.</param>
        /// <param name="backgroundJobClient">The background job client.</param>
        /// <param name="configurationData">The configuration data.</param>
        public ImportSapData(ISapDataImportConfigurationRepository sapDataImportConfigurationRepository,
            IEmailManager emailManager, ISapDataImportRepository sapDataImportRepository, IFileUtiltity fileUtiltity,
            IBackgroundJobClient backgroundJobClient, IConfigurationData configurationData)
        {
            _sapDataImportConfigurationRepository = sapDataImportConfigurationRepository;
            _emailManager = emailManager;
            _sapDataImportRepository = sapDataImportRepository;
            _fileUtiltity = fileUtiltity;
            _backgroundJobClient = backgroundJobClient;
            _configurationData = configurationData;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            //1. get configuration data
            var configurations = _sapDataImportConfigurationRepository.ReadAll();

            foreach (var configuration in configurations)
            {
                try
                {
                    ProcessConfiguration(configuration);
                }
                catch (Exception ex)
                {
                    _backgroundJobClient.Schedule(
                        () => _emailManager.SendEmail(configuration.SupportEmailAddress, $"CMS {Name} Error",
                            $"File Import Failed With: {ex}"), DateTime.Now);
                }
            }
        }

        /// <summary>
        /// Processes the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        private void ProcessConfiguration(SapDataImportConfiguration configuration)
        {
            //2. check for the import file
            var files = _fileUtiltity.GetFilesInDirectory(configuration.FileImportLocation, true);

            //3. if no import file notify support email address
            if (files == null || !files.Any())
            {
                _backgroundJobClient.Schedule(
                    () => _emailManager.SendEmail(configuration.SupportEmailAddress, $"CMS {Name} Error",
                        $"CMS {Name} file was not found in location: {configuration.FileImportLocation}"),
                    DateTime.Now);
            }
            else
            {
                foreach (var file in files)
                {
                    //4. if there is a file import into CMS database
                    var sapDataImports = ImportData(file, configuration);

                    //5. update the prices and is mismatched records
                    UpdateLoadedPricesAndIsMismatched(sapDataImports);

                    //6. delete the file
                    _fileUtiltity.DeleteFile(file);
                }
            }
        }

        /// <summary>
        /// Updates the loaded prices and is mismatched.
        /// </summary>
        /// <param name="sapDataImports">The sap data imports.</param>
        private void UpdateLoadedPricesAndIsMismatched(IEnumerable<SapDataImport> sapDataImports)
        {
            foreach (var sapDataImport in sapDataImports)
                _sapDataImportRepository.UpdateLoadedPrices(sapDataImport);

            _sapDataImportRepository.UpdateMismatches();

            //_sapDataImportRepository.UpdatePricesAndMismatches();
        }

        /// <summary>
        /// Imports the data.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="configuration">The configuration.</param>
        private IEnumerable<SapDataImport> ImportData(string file, SapDataImportConfiguration configuration)
        {
            var fileData = _fileUtiltity.ReadFileLines(file, true);
            var sapDataImports = GetDataFromFile(fileData.ToArray(), configuration);

            foreach (var sapDataImport in sapDataImports)
            {
                //if the price point id exists that means it's an update, otherwise it's an insert
                var existingSapDataImport = _sapDataImportRepository.ReadById(sapDataImport.PricepointId);
                sapDataImport.LastUpdatedDate = DateTime.Now;
                sapDataImport.ExportRow = false;

                if (existingSapDataImport != null)
                {
                    sapDataImport.ImportDate = existingSapDataImport.ImportDate;
                    _sapDataImportRepository.Update(sapDataImport);
                }
                else
                {
                    sapDataImport.ImportDate = DateTime.Now;
                    _sapDataImportRepository.Create(sapDataImport);
                }
            }

            return sapDataImports;
        }

        /// <summary>
        /// Gets the data from file.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        private IEnumerable<SapDataImport> GetDataFromFile(IReadOnlyList<string> fileData,
            SapDataImportConfiguration configuration)
        {
            var sapDataImports = new List<SapDataImport>();

            //first row is the column headings
            var columnHeadings = fileData[0].Split('|');

            //loop through the data starting from the second row
            for (var i = 1; i < fileData.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(fileData[i]))
                {
                    var recordData = fileData[i].Split('|');

                    if (columnHeadings.Length != recordData.Length)
                    {
                        var message =
                            $"Record: ({fileData[i]}) has {recordData.Length} columns instead of required {columnHeadings.Length} columns ({fileData[0]})";

                        _backgroundJobClient.Schedule(
                            () => _emailManager.SendEmail(configuration.SupportEmailAddress, $"CMS {Name} Error",
                                message),
                            DateTime.Now);
                    }
                    else
                    {
                        sapDataImports.Add(GetSapDataImport(columnHeadings, recordData));
                    }
                }
            }

            return sapDataImports;
        }

        /// <summary>
        /// Gets the sap data import.
        /// </summary>
        /// <param name="columnHeadings">The column headings.</param>
        /// <param name="recordData">The record data.</param>
        /// <returns></returns>
        private SapDataImport GetSapDataImport(string[] columnHeadings, string[] recordData)
        {
            var sapDataImport = new SapDataImport();

            for (var j = 0; j < columnHeadings.Length; j++)
            {
                var columnToFind = columnHeadings[j];
                var columnValue = recordData[j];

                if (!string.IsNullOrWhiteSpace(columnValue))
                {
                    var property = sapDataImport.GetType().GetProperty(columnToFind);

                    if (columnToFind == Constants.SapDataImport.PricepointId)
                        property?.SetValue(sapDataImport, Convert.ToInt32(columnValue), null);
                    else
                        property?.SetValue(sapDataImport, columnValue, null);
                }
            }

            return sapDataImport;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Daily SAP Data Import";

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        public int HourToRun => 5;

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        public int MinuteToRun => 0;
    }
}