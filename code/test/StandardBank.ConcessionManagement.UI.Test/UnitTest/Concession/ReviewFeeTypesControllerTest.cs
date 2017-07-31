using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers.Concession;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Concession
{
    /// <summary>
    /// Review fee types controller tests
    /// </summary>
    public class ReviewFeeTypesControllerTest
    {
        /// <summary>
        /// The review fee types controller
        /// </summary>
        private readonly ReviewFeeTypesController _reviewFeeTypesController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewFeeTypesControllerTest"/> class.
        /// </summary>
        public ReviewFeeTypesControllerTest()
        {
            _reviewFeeTypesController = new ReviewFeeTypesController(MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetReviewFeeTypes()).Returns(new[] {new ReviewFeeType()});

            var result = _reviewFeeTypesController.Get();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
