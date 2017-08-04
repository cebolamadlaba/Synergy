using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers.Condition;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Condition
{
    /// <summary>
    /// Period type controller tests
    /// </summary>
    public class PeriodTypeControllerTest
    {
        /// <summary>
        /// The period type controller
        /// </summary>
        private readonly PeriodTypeController _periodTypeController;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodTypeControllerTest"/> class.
        /// </summary>
        public PeriodTypeControllerTest()
        {
            _periodTypeController = new PeriodTypeController(MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetPeriodTypes()).Returns(new[] {new PeriodType()});

            var result = _periodTypeController.Get();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
