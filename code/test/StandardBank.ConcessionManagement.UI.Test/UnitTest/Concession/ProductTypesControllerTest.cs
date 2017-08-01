using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers.Concession;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Concession
{
    /// <summary>
    /// Product types controller test
    /// </summary>
    public class ProductTypesControllerTest
    {
        /// <summary>
        /// The product types controller
        /// </summary>
        private readonly ProductTypesController _productTypesController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypesControllerTest"/> class.
        /// </summary>
        public ProductTypesControllerTest()
        {
            _productTypesController = new ProductTypesController(MockLookupTableManager.Object);
        }

        /// <summary>
        /// Gets the executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetProductTypesForConcessionType(It.IsAny<string>()))
                .Returns(new[] { new ProductType() });

            var result = _productTypesController.Get("Test");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
