using StandardBank.ConcessionManagement.UI.Controllers;
using System.Threading.Tasks;
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
            
        }

        /// <summary>
        /// Tests that NewCash executes positive.
        /// </summary>
        [Fact]
        public async Task NewCash_Executes_Positive()
        {
            
        }

        /// <summary>
        /// Tests that CashConcessionData executes positive.
        /// </summary>
        [Fact]
        public void CashConcessionData_Executes_Positive()
        {
            
        }

        /// <summary>
        /// Tests that UpdateCash executes positive.
        /// </summary>
        [Fact]
        public async Task UpdateCash_Executes_Positive()
        {
            
        }
    }
}

