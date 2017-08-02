using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers.Condition;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Condition
{
    /// <summary>
    /// Condition type controller tests
    /// </summary>
    public class ConditionTypeControllerTest
    {
        /// <summary>
        /// The condition type controller
        /// </summary>
        private readonly ConditionTypeController _conditionTypeController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionTypeControllerTest"/> class.
        /// </summary>
        public ConditionTypeControllerTest()
        {
            _conditionTypeController = new ConditionTypeController(MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetConditionTypes()).Returns(new[] {new ConditionType()});

            var result = _conditionTypeController.Get();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
