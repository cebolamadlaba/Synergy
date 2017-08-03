using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers.Concession;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Concession
{
    /// <summary>
    /// Concession conditions controller tests
    /// </summary>
    public class ConcessionConditionsControllerTest
    {
        /// <summary>
        /// The concession conditions controller
        /// </summary>
        private readonly ConcessionConditionsController _concessionConditionsController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionConditionsControllerTest"/> class.
        /// </summary>
        public ConcessionConditionsControllerTest()
        {
            _concessionConditionsController = new ConcessionConditionsController(MockConcessionManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetConcessionConditions(It.IsAny<int>()))
                .Returns(new[] {new ConcessionCondition()});

            var result = _concessionConditionsController.Get(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
