using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Test.Helpers;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Transactional controller tests
    /// </summary>
    public class TransactionalControllerTest
    {
        /// <summary>
        /// The transactional controller
        /// </summary>
        private readonly TransactionalController _transactionalController;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalControllerTest"/> class.
        /// </summary>
        public TransactionalControllerTest()
        {
            _transactionalController = new TransactionalController(new FakeSiteHelper(),
                MockTransactionalManager.Object, MockMediator.Object,null,null);
        }

        /// <summary>
        /// Tests that TransactionalView executes positive.
        /// </summary>
        [Fact]
        public void TransactionalView_Executes_Positive()
        {
            var riskGroup = new RiskGroup {Id = 1, Name = "Unit Test Risk Group", Number = 1};
            MockTransactionalManager.Setup(_ => _.GetTransactionalViewData(It.IsAny<int>())).Returns(
                new TransactionalView
                {
                    RiskGroup = riskGroup,
                    TransactionalFinancial = new TransactionalFinancial(),
                    TransactionalConcessions = new[] {new TransactionalConcession()},
                    TransactionalProductGroups = new[] {new TransactionalProductGroup()}
                });

            var result = _transactionalController.TransactionalView(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalView);

            var returnedCashView = apiResult.Value as TransactionalView;

            Assert.NotNull(returnedCashView);
            Assert.NotNull(returnedCashView.RiskGroup);
            Assert.Equal(riskGroup.Id, returnedCashView.RiskGroup.Id);
            Assert.Equal(riskGroup.Name, returnedCashView.RiskGroup.Name);
            Assert.Equal(riskGroup.Number, returnedCashView.RiskGroup.Number);
        }

        /// <summary>
        /// Tests that TransactionalConcessionData executes positive.
        /// </summary>
        [Fact]
        public void TransactionalConcessionData_Executes_Positive()
        {
            MockTransactionalManager.Setup(_ => _.GetTransactionalConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(new TransactionalConcession());

            var result = _transactionalController.TransactionalConcessionData("T001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalConcession);
        }

        /// <summary>
        /// Tests that NewTransactional executes positive.
        /// </summary>
        [Fact]
        public async Task NewTransactional_Executes_Positive()
        {
            var transactionalConcession = new TransactionalConcession
            {
                Concession = new Concession(),
                ConcessionConditions = new[] {new ConcessionCondition()},
                TransactionalConcessionDetails = new[] {new TransactionalConcessionDetail()},
                CurrentUser = new User()
            };

            var result = await _transactionalController.NewTransactional(transactionalConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalConcession);
        }

        /// <summary>
        /// Tests that UpdateTransactional executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateTransactional_Executes_Positive()
        {
            var transactionalConcession = new TransactionalConcession
            {
                Concession = new Concession(),
                ConcessionConditions = new[] { new ConcessionCondition() },
                TransactionalConcessionDetails = new[] { new TransactionalConcessionDetail() },
                CurrentUser = new User()
            };

            MockTransactionalManager.Setup(_ => _.GetTransactionalConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(transactionalConcession);

            var result = await _transactionalController.UpdateTransactional(transactionalConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalConcession);
        }

        /// <summary>
        /// Tests that LatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void LatestCrsOrMrs_Executes_Positive()
        {
            MockTransactionalManager.Setup(_ => _.GetLatestCrsOrMrs(It.IsAny<int>())).Returns(500);

            var result = _transactionalController.LatestCrsOrMrs(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is decimal);
            Assert.Equal(Convert.ToDecimal(apiResult.Value), 500);
        }

        /// <summary>
        /// Tests that TransactionalFinancial executes positive.
        /// </summary>
        [Fact]
        public void TransactionalFinancial_Executes_Positive()
        {
            MockTransactionalManager.Setup(_ => _.GetTransactionalFinancialForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new TransactionalFinancial());

            var result = _transactionalController.TransactionalFinancial(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalFinancial);
        }

        /// <summary>
        /// Tests that ExtendConcession executes positive.
        /// </summary>
        [Fact]
        public async Task ExtendConcession_Executes_Positive()
        {
            var transactionalConcession = new TransactionalConcession
            {
                Concession = new Concession(),
                ConcessionConditions = new[] { new ConcessionCondition() },
                TransactionalConcessionDetails = new[] { new TransactionalConcessionDetail() },
                CurrentUser = new User()
            };

            MockTransactionalManager.Setup(_ => _.GetTransactionalConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(transactionalConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _transactionalController.ExtendConcession("T001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalConcession);
        }

        /// <summary>
        /// Tests that RenewTransactional executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RenewTransactional_Executes_Positive()
        {
            var transactionalConcession = new TransactionalConcession
            {
                Concession = new Concession(),
                ConcessionConditions = new[] { new ConcessionCondition() },
                TransactionalConcessionDetails = new[] { new TransactionalConcessionDetail() },
                CurrentUser = new User()
            };

            MockTransactionalManager.Setup(_ => _.GetTransactionalConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(transactionalConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _transactionalController.RenewTransactional(transactionalConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalConcession);
        }

        /// <summary>
        /// Tests that UpdateRecalledTransactional executes positive.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateRecalledTransactional_Executes_Positive()
        {
            var transactionalConcession = new TransactionalConcession
            {
                Concession = new Concession(),
                ConcessionConditions = new[] { new ConcessionCondition() },
                TransactionalConcessionDetails = new[] { new TransactionalConcessionDetail() },
                CurrentUser = new User()
            };

            MockTransactionalManager.Setup(_ => _.GetTransactionalConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(transactionalConcession);

            MockMediator.Setup(_ => _.Send(It.IsAny<AddConcession>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Concession());

            var result = await _transactionalController.UpdateRecalledTransactional(transactionalConcession);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is TransactionalConcession);
        }
    }
}
