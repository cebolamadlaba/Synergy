using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
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
            _pricingManager = new PricingManager(MockRiskGroupRepository.Object, InstantiatedDependencies.Mapper);
        }

        /// <summary>
        /// Tests that GetRiskGroupForRiskGroupNumber for an active record executes positive
        /// </summary>
        [Fact]
        public void GetRiskGroupForRiskGroupNumber_ActiveRecord_Executes_Positive()
        {
            var riskGroup = new RiskGroup
            {
                RiskGroupNumber = 1,
                RiskGroupName = "Unit Test Risk Group",
                IsActive = true,
                Id = 1
            };

            MockRiskGroupRepository.Setup(_ => _.ReadByRiskGroupNumberIsActive(It.IsAny<int>(), true)).Returns(riskGroup);

            var result = _pricingManager.GetRiskGroupForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.Equal(result.Name, riskGroup.RiskGroupName);
        }

        /// <summary>
        /// Tests that GetRiskGroupForRiskGroupNumber for an in-active record executes positive
        /// </summary>
        [Fact]
        public void GetRiskGroupForRiskGroupNumber_InActiveRecord_Executes_Positive()
        {
            RiskGroup riskGroup = null;
            MockRiskGroupRepository.Setup(_ => _.ReadByRiskGroupNumberIsActive(It.IsAny<int>(), true)).Returns(riskGroup);

            var result = _pricingManager.GetRiskGroupForRiskGroupNumber(1);

            Assert.Null(result);
        }
    }
}
