using System;
using System.Threading.Tasks;
using Moq;
using StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Model.Repository;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest.ScheduledJobs
{
    /// <summary>
    /// Export sap data test
    /// </summary>
    public class ExportSapDataTest
    {
        /// <summary>
        /// The export sap data
        /// </summary>
        private readonly ExportSapData _exportSapData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportSapDataTest"/> class.
        /// </summary>
        public ExportSapDataTest()
        {
            _exportSapData = new ExportSapData(MockSapDataImportConfigurationRepository.Object, MockEmailManager.Object,
                MockSapDataImportRepository.Object, MockFileUtiltity.Object, MockBackgroundJobClient.Object);
        }

        /// <summary>
        /// Tests that run executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Run_Executes_Positive()
        {
            MockSapDataImportConfigurationRepository.Setup(_ => _.ReadAll()).Returns(new[]
            {
                new SapDataImportConfiguration
                {
                    FileImportLocation = @"C:\Temp\SapImport",
                    SupportEmailAddress = "heathesh@kodhe.io",
                    FileExportLocation = @"C:\Temp\SapExport",
                    Id = 1
                }
            });

            MockSapDataImportRepository.Setup(_ => _.GenerateSapExport()).Returns(new[]
            {
                new SapDataImport
                {
                    PricepointId = 1,
                    ImportDate = DateTime.Now,
                    ExportRow = true,
                    LastUpdatedDate = DateTime.Now,
                    ExpiryDate = "2017/10/01",
                    AccountName = "Test Account",
                    AccountNo = "00123",
                    AdvaloremFee = "9.99",
                    BankIdentifierId = "43435",
                    Channel = "234324",
                    CommunicationFee = "232",
                    CustomerId = "654546",
                    Description = "Unit Test Description",
                    EffectiveDate = "2017/11/10",
                    EntryDate = "2016/01/01",
                    FlatFee = "3.5",
                    GroupId = "24",
                    MarketSegment = "400",
                    MaximumFee = "10",
                    MinimumFee = "4",
                    OptionId = null,
                    ProductId = "78",
                    ProductName = "Unit Test",
                    SequenceId = "1",
                    Status = "A",
                    SubGroupId = "100",
                    TableNo = "200",
                    TerminationDate = "2020/10/10",
                    TierFromValue = "1003",
                    TierToValue = "23",
                    TransactionRevenue = "2343",
                    TransactionVolume = "234324",
                    UserId = "A234324"
                }
            });

            await _exportSapData.Run();

            MockFileUtiltity.Verify(
                _ => _.WriteFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()),
                Times.AtLeastOnce);

            MockSapDataImportRepository.Verify(_ => _.Update(It.IsAny<SapDataImport>()), Times.AtLeastOnce);
        }
    }
}
