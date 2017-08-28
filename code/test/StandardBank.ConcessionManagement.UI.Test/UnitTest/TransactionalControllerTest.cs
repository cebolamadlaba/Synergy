﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
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
            _transactionalController = new TransactionalController(new FakeSiteHelper(), MockPricingManager.Object,
                MockTransactionalManager.Object);
        }

        /// <summary>
        /// Te executes positive.
        /// </summary>
        [Fact]
        public void TransactionalView_Executes_Positive()
        {
            var riskGroup = new RiskGroup {Id = 1, Name = "Unit Test Risk Group", Number = 1};
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            MockTransactionalManager.Setup(_ => _.GetTransactionalConcessionsForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new[] {new TransactionalConcession()});

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
    }
}
