using System;
using System.Threading;
using StandardBank.ConcessionManagement.UI.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
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
            _cashController = new CashController(new FakeSiteHelper(), MockCashManager.Object, MockMediator.Object, null, null);
        }

        /// <summary>
        /// Tests that CashView executes positive.
        /// </summary>
        [Fact]
        public void CashView_Executes_Positive()
        {
            var riskGroup = new RiskGroup { Id = 1, Name = "Unit Test Risk Group", Number = 1 };

            MockCashManager.Setup(_ => _.GetCashViewData(It.IsAny<int>(), 0, null))
                .Returns(new CashView { RiskGroup = riskGroup });

            var result = _cashController.CashView(1, 0);
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
                CashConcessionDetails = new[] { new CashConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
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

        /// <summary>
        /// Tests that ExtendConcession executes positive.
        /// </summary>
        [Fact]
        public async Task ExtendConcession_Executes_Positive()
        {
            var cashConcession = new CashConcession
            {
                Concession = new Concession(),
                CashConcessionDetails = new[] { new CashConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockCashManager.Setup(_ => _.GetCashConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(cashConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _cashController.ExtendConcession("C001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashConcession);
        }

        /// <summary>
        /// Tests that RenewCash executes positive.
        /// </summary>
        [Fact]
        public async Task RenewCash_Executes_Positive()
        {
            var cashConcession = new CashConcession
            {
                Concession = new Concession(),
                CashConcessionDetails = new[] { new CashConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockCashManager.Setup(_ => _.GetCashConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(cashConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _cashController.RenewCash(cashConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashConcession);
        }

        /// <summary>
        /// Tests that UpdateRecalledCash executes positive.
        /// </summary>
        [Fact]
        public async Task UpdateRecalledCash_Executes_Positive()
        {
            var cashConcession = new CashConcession
            {
                Concession = new Concession(),
                CashConcessionDetails = new[] { new CashConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockCashManager.Setup(_ => _.GetCashConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(cashConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _cashController.UpdateRecalledCash(cashConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashConcession);
        }

        /// <summary>
        /// Tests that LatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void LatestCrsOrMrs_Executes_Positive()
        {
            MockCashManager.Setup(_ => _.GetLatestCrsOrMrs(It.IsAny<int>())).Returns(500);

            var result = _cashController.LatestCrsOrMrs(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is decimal);
            Assert.Equal(Convert.ToDecimal(apiResult.Value), 500);
        }

        /// <summary>
        /// Tests that CashFinancial executes positive.
        /// </summary>
        [Fact]
        public void CashFinancial_Executes_Positive()
        {
            MockCashManager.Setup(_ => _.GetCashFinancialForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new CashFinancial());

            var result = _cashController.CashFinancial(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is CashFinancial);
        }
    }
}

