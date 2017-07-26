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
            _pricingManager = new PricingManager(MockRiskGroupRepository.Object);
        }

        /// <summary>
        /// Tests that GetRiskGroupName for an active record executes positive
        /// </summary>
        [Fact]
        public void GetRiskGroupName_ActiveRecord_Executes_Positive()
        {
            var riskGroup = new RiskGroup
            {
                RiskGroupNumber = 1,
                RiskGroupName = "Unit Test Risk Group",
                IsActive = true,
                Id = 1
            };

            MockRiskGroupRepository.Setup(_ => _.ReadByRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            var result = _pricingManager.GetRiskGroupName(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result, riskGroup.RiskGroupName);
        }

        /// <summary>
        /// Tests that GetRiskGroupName for an in-active record executes positive
        /// </summary>
        [Fact]
        public void GetRiskGroupName_InActiveRecord_Executes_Positive()
        {
            var riskGroup = new RiskGroup
            {
                RiskGroupNumber = 1,
                RiskGroupName = "Unit Test Risk Group",
                IsActive = false,
                Id = 1
            };

            MockRiskGroupRepository.Setup(_ => _.ReadByRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            var result = _pricingManager.GetRiskGroupName(1);

            Assert.Empty(result);
            Assert.NotEqual(result, riskGroup.RiskGroupName);
        }
    }
}
