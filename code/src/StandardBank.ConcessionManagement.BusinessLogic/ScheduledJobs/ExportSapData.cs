using System;
using System.Collections.Generic;
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
        /// The concession inbox view repository
        /// </summary>
        private readonly IConcessionInboxViewRepository _concessionInboxViewRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportSapData"/> class.
        /// </summary>
        /// <param name="sapDataImportConfigurationRepository">The sap data import configuration repository.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="sapDataImportRepository">The sap data import repository.</param>
        /// <param name="fileUtiltity">The file utiltity.</param>
        /// <param name="backgroundJobClient">The background job client.</param>
        /// <param name="concessionInboxViewRepository">The concession inbox view repository.</param>
        public ExportSapData(ISapDataImportConfigurationRepository sapDataImportConfigurationRepository,
            IEmailManager emailManager, ISapDataImportRepository sapDataImportRepository, IFileUtiltity fileUtiltity,
            IBackgroundJobClient backgroundJobClient, IConcessionInboxViewRepository concessionInboxViewRepository)
        {
            _sapDataImportConfigurationRepository = sapDataImportConfigurationRepository;
            _emailManager = emailManager;
            _sapDataImportRepository = sapDataImportRepository;
            _fileUtiltity = fileUtiltity;
            _backgroundJobClient = backgroundJobClient;
            _concessionInboxViewRepository = concessionInboxViewRepository;
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
                    //TODO: Call the GenerateSapExport stored proc and get back the list of records to export

                    //TODO: Export the list of records

                    //TODO: Reset the "ExportRow" flag on each record that's been exported
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
    }
}
