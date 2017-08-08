using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Lending controller tests
    /// </summary>
    public class LendingControllerTest
    {
        /// <summary>
        /// The lending controller
        /// </summary>
        private readonly LendingController _lendingController;

        /// <summary>
        /// Initializes the class
        /// </summary>
        public LendingControllerTest()
        {
            _lendingController = new LendingController(MockPricingManager.Object, MockLendingManager.Object,
                MockSiteHelper.Object, MockMediator.Object);
        }

        /// <summary>
        /// Tests that LendingView executes positive
        /// </summary>
        [Fact]
        public void LendingView_Executes_Positive()
        {
            var riskGroup = new RiskGroup { Id = 1, Name = "Unit Test Risk Group", Number = 1 };
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            var result = _lendingController.LendingView(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);

            var returnedLendingView = apiResult.Value as LendingView;

            Assert.NotNull(returnedLendingView);
            Assert.NotNull(returnedLendingView.RiskGroup);
            Assert.Equal(riskGroup.Id, returnedLendingView.RiskGroup.Id);
            Assert.Equal(riskGroup.Name, returnedLendingView.RiskGroup.Name);
            Assert.Equal(riskGroup.Number, returnedLendingView.RiskGroup.Number);
        }
    }
}