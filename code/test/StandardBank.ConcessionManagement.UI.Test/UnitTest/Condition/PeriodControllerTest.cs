using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers.Condition;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Condition
{
    /// <summary>
    /// Period controller tests
    /// </summary>
    public class PeriodControllerTest
    {
        /// <summary>
        /// The period controller
        /// </summary>
        private readonly PeriodController _periodController;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodControllerTest"/> class.
        /// </summary>
        public PeriodControllerTest()
        {
            _periodController = new PeriodController(MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetPeriods()).Returns(new[] {new Period()});

            var result = _periodController.Get();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
