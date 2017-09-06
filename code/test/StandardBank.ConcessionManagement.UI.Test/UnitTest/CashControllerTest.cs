using StandardBank.ConcessionManagement.UI.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Cash controller tests
    /// </summary>
    public class CashControllerTest
    {
        /// <summary>
        /// The cash controller
        /// </summary>
        private readonly CashController _cashController;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashControllerTest"/> class.
        /// </summary>
        public CashControllerTest()
        {
            _cashController = new CashController(new FakeSiteHelper(), MockPricingManager.Object,
                MockCashManager.Object, MockMediator.Object);
        }

        /// <summary>
        /// Tests that CashView executes positive.
        /// </summary>
        [Fact]
        public void CashView_Executes_Positive()
        {
            var riskGroup = new RiskGroup { Id = 1, Name = "Unit Test Risk Group", Number = 1 };
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            MockCashManager.Setup(_ => _.GetCashConcessionsForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new[] {new CashConcession()});

            var result = _cashController.CashView(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashView);

            var returnedCashView = apiResult.Value as CashView;

            Assert.NotNull(returnedCashView);
            Assert.NotNull(returnedCashView.RiskGroup);
            Assert.Equal(riskGroup.Id, returnedCashView.RiskGroup.Id);
            Assert.Equal(riskGroup.Name, returnedCashView.RiskGroup.Name);
            Assert.Equal(riskGroup.Number, returnedCashView.RiskGroup.Number);
        }

        /// <summary>
        /// Tests that NewCash executes positive.
        /// </summary>
        [Fact]
        public async Task NewCash_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var cashConcession = new CashConcession
            {
                Concession = new Concession(),
                CashConcessionDetails = new[] {new CashConcessionDetail()},
                ConcessionConditions = new[] {new ConcessionCondition()}
            };

            var result = await _cashController.NewCash(cashConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashConcession);
        }

        /// <summary>
        /// Tests that CashConcessionData executes positive.
        /// </summary>
        [Fact]
        public void CashConcessionData_Executes_Positive()
        {
            MockCashManager.Setup(_ => _.GetCashConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(new CashConcession());

            var result = _cashController.CashConcessionData("C001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashConcession);
        }

        /// <summary>
        /// Tests that UpdateCash executes positive.
        /// </summary>
        [Fact]
        public async Task UpdateCash_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var cashConcession = new CashConcession
            {
                Concession = new Concession(),
                CashConcessionDetails = new[] { new CashConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockCashManager.Setup(_ => _.GetCashConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(cashConcession);

            var result = await _cashController.UpdateCash(cashConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashConcession);
        }
    }
}

