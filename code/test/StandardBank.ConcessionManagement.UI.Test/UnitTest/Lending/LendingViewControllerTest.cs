using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.UI.Controllers.Lending;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Lending
{
    /// <summary>
    /// Lending view controller tests
    /// </summary>
    public class LendingViewControllerTest
    {
        /// <summary>
        /// The lending view controller
        /// </summary>
        private readonly LendingViewController _lendingViewController;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendingViewControllerTest"/> class.
        /// </summary>
        public LendingViewControllerTest()
        {
            _lendingViewController = new LendingViewController(MockPricingManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            var riskGroup = new RiskGroup { Id = 1, Name = "Unit Test Risk Group", Number = 1 };
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            var result = _lendingViewController.Get(1);

            Assert.NotNull(result);
            Assert.NotNull(result.RiskGroup);
            Assert.Equal(riskGroup.Id, result.RiskGroup.Id);
            Assert.Equal(riskGroup.Name, result.RiskGroup.Name);
            Assert.Equal(riskGroup.Number, result.RiskGroup.Number);
        }
    }
}
