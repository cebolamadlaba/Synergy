using System.Threading.Tasks;
using Moq;
using StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest.ScheduledJobs
{
    /// <summary>
    /// Import Sap Data tests
    /// </summary>
    public class ImportSapDataTest
    {
        /// <summary>
        /// Tests that run for new records executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Run_For_New_Records_Executes_Positive()
        {
            var mockSapDataImportConfigurationRepository = new Mock<ISapDataImportConfigurationRepository>();
            var mockFileUtiltity = new Mock<IFileUtiltity>();
            var mockSapDataImportRepository = new Mock<ISapDataImportRepository>();

            mockSapDataImportConfigurationRepository.Setup(_ => _.ReadAll()).Returns(new[]
            {
                new SapDataImportConfiguration
                {
                    FileImportLocation = @"C:\Temp\SapImport",
                    SupportEmailAddress = "heathesh@kodhe.io",
                    FileExportLocation = @"C:\Temp\SapExport",
                    Id = 1
                }
            });

            mockFileUtiltity.Setup(_ => _.GetFilesInDirectory(It.IsAny<string>(), It.IsAny<bool>())).Returns(new[]
            {
                "importfile01.txt"
            });

            mockFileUtiltity.Setup(_ => _.ReadFileLines(It.IsAny<string>(), It.IsAny<bool>())).Returns(new[]
            {
                "PricepointId|CustomerId|AccountName|ProductId|Description|GroupId|SubGroupId|BankIdentifierId|AccountNo|OptionId|UserId|TierFromValue|TierToValue|AdvaloremFee|MinimumFee|MaximumFee|FlatFee|CommunicationFee|TableNo|TransactionVolume|TransactionRevenue|ProductName|Channel|MarketSegment|SequenceId|EntryDate|EffectiveDate|ExpiryDate|TerminationDate|Status",
                "4|100065956|0|AS_1_AutoSafe_CDF_10|Cash deposit fee per R100 (applicable if autosafe deposits are processed through the cash centre)| | | |000083003| | |0.0000|9999.9900|1.95000000|0.00000000|0.00000000|7.00000000|0.00000000|199|0|0.00000000|CDF|108|554|2|2017/10/24 12:00:00 AM|2017/10/24 12:00:00 AM|9999/12/31 12:00:00 AM|9999/12/31 12:00:00 AM|A",
                "34681|533535202|0|SX902_1|Advalorem on the value of the cheque (cents per R100) + Base Fee| | | |000111112| | |0.0000|9999999999999.9900|2.20000000|0.00000000|0.00000000|40.00000000|0.00000000|71|0|0.00000000|CEF|154|400|19061|2017/10/24 12:00:00 AM|2017/10/24 12:00:00 AM|9999/12/31 12:00:00 AM|9999/12/31 12:00:00 AM|A",
                "34692|85816530|0|SX902_1|Advalorem on the value of the cheque (cents per R100) + Base Fee| | | |000104019| | |0.0000|9999999999999.9900|2.20000000|0.00000000|0.00000000|40.00000000|0.00000000|71|0|0.00000000|CEF|154|401|19072|2017/10/24 12:00:00 AM|2017/10/24 12:00:00 AM|9999/12/31 12:00:00 AM|9999/12/31 12:00:00 AM|A"
            });

            var importSapData = new ImportSapData(mockSapDataImportConfigurationRepository.Object,
                MockEmailManager.Object, mockSapDataImportRepository.Object, mockFileUtiltity.Object,
                MockBackgroundJobClient.Object, InstantiatedDependencies.ConfigurationData);

            await importSapData.Run();

            //the create must be called three times because there are three import records
            mockSapDataImportRepository.Verify(_ => _.Create(It.IsAny<SapDataImport>()), Times.Exactly(3));

            //the update must never be called
            mockSapDataImportRepository.Verify(_ => _.Update(It.IsAny<SapDataImport>()), Times.Never);
        }

        /// <summary>
        /// Tests that run for new and updated records executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Run_For_New_And_Updated_Records_Executes_Positive()
        {
            var mockSapDataImportConfigurationRepository = new Mock<ISapDataImportConfigurationRepository>();
            var mockFileUtiltity = new Mock<IFileUtiltity>();
            var mockSapDataImportRepository = new Mock<ISapDataImportRepository>();

            mockSapDataImportConfigurationRepository.Setup(_ => _.ReadAll()).Returns(new[]
            {
                new SapDataImportConfiguration
                {
                    FileImportLocation = @"C:\Temp\SapImport",
                    SupportEmailAddress = "heathesh@kodhe.io",
                    FileExportLocation = @"C:\Temp\SapExport",
                    Id = 1
                }
            });

            mockFileUtiltity.Setup(_ => _.GetFilesInDirectory(It.IsAny<string>(), It.IsAny<bool>())).Returns(new[]
            {
                "importfile01.txt"
            });

            mockFileUtiltity.Setup(_ => _.ReadFileLines(It.IsAny<string>(), It.IsAny<bool>())).Returns(new[]
            {
                "PricepointId|CustomerId|AccountName|ProductId|Description|GroupId|SubGroupId|BankIdentifierId|AccountNo|OptionId|UserId|TierFromValue|TierToValue|AdvaloremFee|MinimumFee|MaximumFee|FlatFee|CommunicationFee|TableNo|TransactionVolume|TransactionRevenue|ProductName|Channel|MarketSegment|SequenceId|EntryDate|EffectiveDate|ExpiryDate|TerminationDate|Status",
                "786786|100065956|0|AS_1_AutoSafe_CDF_10|Cash deposit fee per R100 (applicable if autosafe deposits are processed through the cash centre)| | | |000083003| | |0.0000|9999.9900|1.95000000|0.00000000|0.00000000|7.00000000|0.00000000|199|0|0.00000000|CDF|108|554|2|2017/10/24 12:00:00 AM|2017/10/24 12:00:00 AM|9999/12/31 12:00:00 AM|9999/12/31 12:00:00 AM|A",
                "34681|533535202|0|SX902_1|Advalorem on the value of the cheque (cents per R100) + Base Fee| | | |000111112| | |0.0000|9999999999999.9900|2.20000000|0.00000000|0.00000000|40.00000000|0.00000000|71|0|0.00000000|CEF|154|400|19061|2017/10/24 12:00:00 AM|2017/10/24 12:00:00 AM|9999/12/31 12:00:00 AM|9999/12/31 12:00:00 AM|A",
                "34692|85816530|0|SX902_1|Advalorem on the value of the cheque (cents per R100) + Base Fee| | | |000104019| | |0.0000|9999999999999.9900|2.20000000|0.00000000|0.00000000|40.00000000|0.00000000|71|0|0.00000000|CEF|154|401|19072|2017/10/24 12:00:00 AM|2017/10/24 12:00:00 AM|9999/12/31 12:00:00 AM|9999/12/31 12:00:00 AM|A"
            });

            mockSapDataImportRepository.Setup(_ => _.ReadById(786786)).Returns(new SapDataImport());

            var importSapData = new ImportSapData(mockSapDataImportConfigurationRepository.Object,
                MockEmailManager.Object, mockSapDataImportRepository.Object, mockFileUtiltity.Object,
                MockBackgroundJobClient.Object, InstantiatedDependencies.ConfigurationData);

            await importSapData.Run();

            //the create must be called twices times because there are two new import records
            mockSapDataImportRepository.Verify(_ => _.Create(It.IsAny<SapDataImport>()), Times.Exactly(2));

            //the update must be called once because there is one update record
            mockSapDataImportRepository.Verify(_ => _.Update(It.IsAny<SapDataImport>()), Times.Exactly(1));
        }
    }
}
