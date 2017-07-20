using StandardBank.ConcessionManagement.UI.Controllers.Inbox;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Inbox
{
    /// <summary>
    /// Concessions summary controller tests
    /// </summary>
    public class ConcessionsSummaryControllerTest
    {
        /// <summary>
        /// The concessions summary controller
        /// </summary>
        private readonly ConcessionsSummaryController _concessionsSummaryController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionsSummaryControllerTest"/> class.
        /// </summary>
        public ConcessionsSummaryControllerTest()
        {
            _concessionsSummaryController = new ConcessionsSummaryController();
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            var result = _concessionsSummaryController.Get();

            Assert.NotNull(result);
        }
    }
}
