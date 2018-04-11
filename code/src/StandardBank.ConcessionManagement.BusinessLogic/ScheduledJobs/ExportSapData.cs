using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
    /// Exports the SAP data
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class ExportSapData : IDailyScheduledJob
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
        /// Initializes a new instance of the <see cref="ExportSapData"/> class.
        /// </summary>
        /// <param name="sapDataImportConfigurationRepository">The sap data import configuration repository.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="sapDataImportRepository">The sap data import repository.</param>
        /// <param name="fileUtiltity">The file utiltity.</param>
        /// <param name="backgroundJobClient">The background job client.</param>
        public ExportSapData(ISapDataImportConfigurationRepository sapDataImportConfigurationRepository,
            IEmailManager emailManager, ISapDataImportRepository sapDataImportRepository, IFileUtiltity fileUtiltity,
            IBackgroundJobClient backgroundJobClient)
        {
            _sapDataImportConfigurationRepository = sapDataImportConfigurationRepository;
            _emailManager = emailManager;
            _sapDataImportRepository = sapDataImportRepository;
            _fileUtiltity = fileUtiltity;
            _backgroundJobClient = backgroundJobClient;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            //1. get the configuration data
            var configurations = _sapDataImportConfigurationRepository.ReadAll();

            foreach (var configuration in configurations)
            {
                try
                {
                    //Call the GenerateSapExport stored proc and get back the list of records to export
                    var sapDataImportsToExport = _sapDataImportRepository.GenerateSapExport();

                    if (sapDataImportsToExport != null && sapDataImportsToExport.Any())
                    {
                        //Export the list of records
                        ExportData(sapDataImportsToExport, configuration);

                        //Reset the "ExportRow" flag on each record that's been exported
                        foreach (var sapDataImportToExport in sapDataImportsToExport)
                        {
                            sapDataImportToExport.ExportRow = false;
                            _sapDataImportRepository.Update(sapDataImportToExport);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _backgroundJobClient.Schedule(
                        () => _emailManager.SendEmail(configuration.SupportEmailAddress, $"CMS {Name} Error",
                            $"File Export Failed With: {ex}"), DateTime.Now);
                }
            }
        }

        /// <summary>
        /// Exports the data.
        /// </summary>
        /// <param name="sapDataImportsToExport">The sap data imports to export.</param>
        /// <param name="configuration">The configuration.</param>
        private void ExportData(IEnumerable<SapDataImport> sapDataImportsToExport,
            SapDataImportConfiguration configuration)
        {
            var output = new StringBuilder();

            var properties = typeof(SapDataImport).GetProperties();

            var columnHeadings = GetColumnHeadings(properties);

            output.AppendLine(columnHeadings);

            foreach (var sapDataImportToExport in sapDataImportsToExport)
                output.AppendLine(GenerateSapDataImportExportLine(sapDataImportToExport, properties));

            var filename = $"cms_data_export_{DateTime.Now:yyyyMMdd}.txt";
            var fileContents = output.ToString();

            _fileUtiltity.WriteFile(configuration.FileExportLocation, filename, fileContents, true);
        }

        /// <summary>
        /// Generates the sap data import export line.
        /// </summary>
        /// <param name="sapDataImportToExport">The sap data import to export.</param>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        private string GenerateSapDataImportExportLine(SapDataImport sapDataImportToExport, PropertyInfo[] properties)
        {
            var exportLine = new StringBuilder();

            foreach (var property in properties)
            {
                if (SkipProperty(property.Name))
                    continue;
                
                var valueToAppend = " ";
                var properyValue = property.GetValue(sapDataImportToExport);

                if (properyValue != null)
                    valueToAppend = Convert.ToString(properyValue);

                exportLine.Append(exportLine.Length == 0 ? valueToAppend : $"|{valueToAppend}");
            }

            return exportLine.ToString();
        }

        /// <summary>
        /// Gets the column headings.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        private string GetColumnHeadings(PropertyInfo[] properties)
        {
            var columnHeadings = new StringBuilder();

            foreach (var property in properties)
                if (!SkipProperty(property.Name))
                    columnHeadings.Append(columnHeadings.Length == 0 ? property.Name : $"|{property.Name}");

            return columnHeadings.ToString();
        }

        /// <summary>
        /// Skips the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        private bool SkipProperty(string propertyName)
        {
            var propertiesToSkip = new[] {"ImportDate", "LastUpdatedDate", "ExportRow"};

            return propertiesToSkip.Contains(propertyName);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Daily SAP Data Export";

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        public int HourToRun => 19;

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        public int MinuteToRun => 0;

        public string type => "";

    }
}
