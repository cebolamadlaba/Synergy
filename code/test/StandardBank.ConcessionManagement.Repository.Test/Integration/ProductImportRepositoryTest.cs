using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ProductImport repository tests
    /// </summary>
    public class ProductImportRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ProductImport
            {
                ProductId = DataHelper.GetProductId(),
                ImportFileChannel = "1f44737159"
            };

            var result = InstantiatedDependencies.ProductImportRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductImportRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProductImportRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProductImportRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductImportRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProductImportRepository.ReadById(id);

            model.ProductId = DataHelper.GetAlternateProductId(model.ProductId);
            model.ImportFileChannel = "7308b0228a";

            InstantiatedDependencies.ProductImportRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProductImportRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ProductId, model.ProductId);
            Assert.Equal(updatedModel.ImportFileChannel, model.ImportFileChannel);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ProductImport
            {
                ProductId = DataHelper.GetProductId(),
                ImportFileChannel = "1f44737159"
            };

            var temporaryEntity = InstantiatedDependencies.ProductImportRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ProductImportRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ProductImportRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
