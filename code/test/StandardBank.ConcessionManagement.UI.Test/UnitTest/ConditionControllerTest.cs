using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Condition controller tests
    /// </summary>
    public class ConditionControllerTest
    {
        /// <summary>
        /// The condition controller
        /// </summary>
        private readonly ConditionController _conditionController;

        /// <summary>
        /// Initializes the class
        /// </summary>
        public ConditionControllerTest()
        {
            _conditionController = new ConditionController(MockLookupTableManager.Object, MockConcessionManager.Object);
        }

        /// <summary>
        /// Tests that ConditionTypes executes positive
        /// </summary>
        [Fact]
        public void ConditionTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetConditionTypes()).Returns(new[] { new ConditionType() });

            var result = _conditionController.ConditionTypes();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ConditionType>);
        }

        /// <summary>
        /// Tests that Periods executes positive
        /// </summary>
        [Fact]
        public void Periods_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetPeriods()).Returns(new[] { new Period() });

            var result = _conditionController.Periods();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<Period>);
        }

        /// <summary>
        /// Tests that PeriodTypes executes positive
        /// </summary>
        [Fact]
        public void PeriodTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetPeriodTypes()).Returns(new[] { new PeriodType() });

            var result = _conditionController.PeriodTypes();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<PeriodType>);
        }
    }
}