using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
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
            var importFiles = _fileUtiltity.GetFilesInDirectory(configuration.FileImportLocation);

            if (importFiles == null || !importFiles.Any())
            {
                _backgroundJobClient.Schedule(
                    () => _emailManager.SendEmail(configuration.SupportEmailAddress, $"CMS {Name} Error",
                        $"CMS {Name} file was not found in location: {configuration.FileImportLocation}"),
                    DateTime.Now);
            }
            else
            {
                foreach (var importFile in importFiles)
                {
                    //process the file
                    ProcessFile(configuration, importFile);

                    //delete the file when done
                    _fileUtiltity.DeleteFile(importFile);
                }
            }
            
        }

        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="importFile">The import file.</param>
        private void ProcessFile(SapDataImportConfiguration configuration, string importFile)
        {
            var isHeaderRow = true;
            string[] columnHeadings = null;

            //use a stream reader to read the file and process line by line
            using (var file = File.OpenRead(importFile))
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var fileData = reader.ReadLine();

                        if (!string.IsNullOrWhiteSpace(fileData))
                        {
                            if (isHeaderRow)
                            {
                                columnHeadings = fileData.Split('|');
                                isHeaderRow = false;
                            }
                            else
                            {
                                ProcessRow(configuration, fileData, columnHeadings);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes the row.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="fileData">The file data.</param>
        /// <param name="columnHeadings">The column headings.</param>
        private void ProcessRow(SapDataImportConfiguration configuration, string fileData, string[] columnHeadings)
        {
            var recordData = fileData.Split('|');

            if (columnHeadings.Length != recordData.Length)
            {
                var message =
                    $"Record: ({fileData}) has {recordData.Length} columns instead of required {columnHeadings.Length} columns ({fileData})";

                _backgroundJobClient.Schedule(
                    () => _emailManager.SendEmail(configuration.SupportEmailAddress, $"CMS {Name} Error", message),
                    DateTime.Now);
            }
            else
            {
                var sapDataImport = GetSapDataImport(columnHeadings, recordData);

                InsertOrUpdateSapDataImport(sapDataImport);

                UpdateLoadedPricesAndIsMismatched(sapDataImport);
            }
        }

        /// <summary>
        /// Updates the loaded prices and is mismatched.
        /// </summary>
        /// <param name="sapDataImport">The sap data import.</param>
        private void UpdateLoadedPricesAndIsMismatched(SapDataImport sapDataImport)
        {
            //TODO: Update the loaded prices and is mismatched for the record we're importing
        }

        /// <summary>
        /// Inserts the or update sap data import.
        /// </summary>
        /// <param name="sapDataImport">The sap data import.</param>
        private void InsertOrUpdateSapDataImport(SapDataImport sapDataImport)
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
                    
                    if (columnToFind == "PricepointId")
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
