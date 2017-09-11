using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.Model.UserInterface;
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
            _lendingController = new LendingController(MockLendingManager.Object, MockSiteHelper.Object,
                MockMediator.Object);
        }

        /// <summary>
        /// Tests that LendingView executes positive
        /// </summary>
        [Fact]
        public void LendingView_Executes_Positive()
        {
            var riskGroup = new RiskGroup { Id = 1, Name = "Unit Test Risk Group", Number = 1 };
            MockLendingManager.Setup(_ => _.GetLendingViewData(It.IsAny<int>())).Returns(new LendingView
            {
                RiskGroup = riskGroup,
                LendingConcessions = new[] {new LendingConcession()},
                LendingProducts = new[] {new LendingProduct()},
                LendingFinancial = new LendingFinancial
                {
                    TotalExposure = 100,
                    WeightedAverageMap = 200,
                    WeightedCrsOrMrs = 300
                }
            });

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

        /// <summary>
        /// Tests that LendingConcessionData executes positive
        /// </summary>
        [Fact]
        public void LendingConcessionData_Executes_Positive()
        {
            MockLendingManager.Setup(_ => _.GetLendingConcession(It.IsAny<string>(), It.IsAny<User>())).Returns(new LendingConcession());

            var result = _lendingController.LendingConcessionData("L001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingConcession);
        }

        /// <summary>
        /// Tests that NewLending executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task NewLending_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var lendingConcession = new LendingConcession
            {
                Concession = new Concession(),
                LendingConcessionDetails = new[] { new LendingConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            var result = await _lendingController.NewLending(lendingConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingConcession);
        }

        /// <summary>
        /// Tests that UpdateLending executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateLending_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var lendingConcession = new LendingConcession
            {
                Concession = new Concession(),
                LendingConcessionDetails = new[] { new LendingConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockLendingManager.Setup(_ => _.GetLendingConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(lendingConcession);

            var result = await _lendingController.UpdateLending(lendingConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingConcession);
        }

        /// <summary>
        /// Tests that ExtendConcession executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ExtendConcession_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var lendingConcession = new LendingConcession
            {
                Concession = new Concession(),
                LendingConcessionDetails = new[] { new LendingConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockLendingManager.Setup(_ => _.GetLendingConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(lendingConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _lendingController.ExtendConcession("L001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingConcession);
        }

        /// <summary>
        /// Tests that RenewLending executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RenewLending_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var lendingConcession = new LendingConcession
            {
                Concession = new Concession(),
                LendingConcessionDetails = new[] { new LendingConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockLendingManager.Setup(_ => _.GetLendingConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(lendingConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _lendingController.RenewLending(lendingConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingConcession);
        }

        /// <summary>
        /// Tests that UpdateRecalledLending executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateRecalledLending_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User());

            var lendingConcession = new LendingConcession
            {
                Concession = new Concession(),
                LendingConcessionDetails = new[] { new LendingConcessionDetail() },
                ConcessionConditions = new[] { new ConcessionCondition() }
            };

            MockLendingManager.Setup(_ => _.GetLendingConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(lendingConcession);

            var result = await _lendingController.UpdateRecalledLending(lendingConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingConcession);
        }

        /// <summary>
        /// Tests that LatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void LatestCrsOrMrs_Executes_Positive()
        {
            MockLendingManager.Setup(_ => _.GetLatestCrsOrMrs(It.IsAny<int>())).Returns(500);

            var result = _lendingController.LatestCrsOrMrs(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is decimal);
            Assert.Equal(Convert.ToDecimal(apiResult.Value), 500);
        }

        /// <summary>
        /// Tests that LendingFinancial executes positive.
        /// </summary>
        [Fact]
        public void LendingFinancial_Executes_Positive()
        {
            MockLendingManager.Setup(_ => _.GetLendingFinancialForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new LendingFinancial());

            var result = _lendingController.LendingFinancial(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is LendingFinancial);
        }
    }
}