using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Concession controller tests
    /// </summary>
    public class ConcessionControllerTest
    {
        /// <summary>
        /// The concession controller
        /// </summary>
        private readonly ConcessionController _concessionController;

        /// <summary>
        /// Initializes the class
        /// </summary>
        public ConcessionControllerTest()
        {
            _concessionController = new ConcessionController(MockConcessionManager.Object, MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that ConcessionConditions executes positive
        /// </summary>
        [Fact]
        public void ConcessionConditions_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetConcessionConditions(It.IsAny<int>()))
                .Returns(new[] { new ConcessionCondition() });

            var result = _concessionController.ConcessionConditions(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ConcessionCondition>);
        }

        /// <summary>
        /// Tests that ProductTypes executes positive
        /// </summary>
        [Fact]
        public void ProductTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetProductTypesForConcessionType(It.IsAny<string>()))
                .Returns(new[] { new ProductType() });

            var result = _concessionController.ProductTypes("Test");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ProductType>);
        }

        /// <summary>
        /// Tests that ReviewFeeTypes executes positive
        /// </summary>
        [Fact]
        public void ReviewFeeTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetReviewFeeTypes()).Returns(new[] { new ReviewFeeType() });

            var result = _concessionController.ReviewFeeTypes();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ReviewFeeType>);
        }
    }
}