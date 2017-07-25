using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Pricing manager tests
    /// </summary>
    public class PricingManagerTest
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingManagerTest"/> class.
        /// </summary>
        public PricingManagerTest()
        {
            _pricingManager = new PricingManager(MockRiskGroupRepository.Object, MockLegalEntityRepository.Object,
                MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that GetLegalEntitiesForRiskGroupNumber executes positive
        /// </summary>
        [Fact]
        public void GetLegalEntitiesForRiskGroupNumber_Executes_Positive()
        {
            MockRiskGroupRepository.Setup(_ => _.ReadByRiskGroupNumber(It.IsAny<int>())).Returns(new RiskGroup
            {
                RiskGroupNumber = 1,
                RiskGroupName = "Unit Test Risk Group",
                IsActive = true,
                Id = 1
            });

            MockLegalEntityRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>())).Returns(new[]
            {
                new LegalEntity
                {
                    IsActive = true,
                    Id = 1,
                    CustomerName = "Unit Test",
                    CustomerNumber = "001",
                    RiskGroupId = 1,
                    RiskGroupName = "Unit Test Risk Group",
                    MarketSegmentId = 1
                }
            });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Test");

            var result = _pricingManager.GetLegalEntitiesForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
