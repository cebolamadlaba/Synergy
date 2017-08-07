using System.Collections.Generic;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.UI.Controllers.Lending;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Lending
{
    /// <summary>
    /// New lending controller tests
    /// </summary>
    public class NewLendingControllerTest
    {
        /// <summary>
        /// The new lending controller
        /// </summary>
        private readonly NewLendingController _newLendingController;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewLendingControllerTest"/> class.
        /// </summary>
        public NewLendingControllerTest()
        {
            _newLendingController = new NewLendingController(MockSiteHelper.Object, MockMediator.Object);
        }

        /// <summary>
        /// Tests that post executes positive.
        /// </summary>
        [Fact]
        public async Task Post_Executes_Positive()
        {
            var result = await _newLendingController.Post(new LendingConcession
            {
                Concession = new Model.UserInterface.Concession(),
                LendingConcessionDetails = new List<LendingConcessionDetail>(),
                ConcessionConditions = new List<ConcessionCondition>()
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
