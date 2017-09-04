using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Test.Helpers;
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
            _concessionController = new ConcessionController(MockConcessionManager.Object,
                MockLookupTableManager.Object, new FakeSiteHelper(), MockLetterGeneratorManager.Object, MockMediator.Object);
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

        /// <summary>
        /// Tests that AccrualTypes executes positive.
        /// </summary>
        [Fact]
        public void AccrualTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetAccrualTypes()).Returns(new[] { new AccrualType() });

            var result = _concessionController.AccrualTypes();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<AccrualType>);
        }

        /// <summary>
        /// Tests that ChannelTypes executes positive.
        /// </summary>
        [Fact]
        public void ChannelTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetChannelTypes()).Returns(new[] { new ChannelType() });

            var result = _concessionController.ChannelTypes();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ChannelType>);
        }

        /// <summary>
        /// Tests that ClientAccounts executes positive
        /// </summary>
        [Fact]
        public void ClientAccounts_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetClientAccounts(It.IsAny<int>())).Returns(new[] { new ClientAccount() });

            var result = _concessionController.ClientAccounts(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ClientAccount>);
        }

        /// <summary>
        /// Tests that UserApprovedConcessions executes positive.
        /// </summary>
        [Fact]
        public void UserApprovedConcessions_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetApprovedConcessionsForUser(It.IsAny<int>()))
                .Returns(new[] {new ApprovedConcession()});

            var result = _concessionController.UserApprovedConcessions();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<ApprovedConcession>);
        }

        /// <summary>
        /// Tests that PrintConcessionLetters executes positive.
        /// </summary>
        [Fact]
        public void PrintConcessionLetters_Executes_Positive()
        {
            MockLetterGeneratorManager.Setup(_ => _.GenerateLetters(It.IsAny<IEnumerable<int>>())).Returns(new byte[0]);

            var result = _concessionController.UserApprovedConcessions();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
        }

        /// <summary>
        /// Tests that TransactionTypes executes positive.
        /// </summary>
        [Fact]
        public void TransactionTypes_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetTransactionTypesForConcessionType(It.IsAny<string>()))
                .Returns(new[] {new TransactionType()});

            var result = _concessionController.TransactionTypes("Test");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<TransactionType>);
        }

        /// <summary>
        /// Tests that TableNumbers executes positive.
        /// </summary>
        [Fact]
        public void TableNumbers_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetTableNumbers())
                .Returns(new[] { new TableNumber() });

            var result = _concessionController.TableNumbers();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is IEnumerable<TableNumber>);
        }

        /// <summary>
        /// Tests that DeactivateConcession executes positive.
        /// </summary>
        [Fact]
        public async Task DeactivateConcession_Executes_Positive()
        {
            var result = await _concessionController.DeactivateConcession("D001");
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
            Assert.True(apiResult.Value is bool);
            Assert.True((bool)apiResult.Value);
        }
    }
}