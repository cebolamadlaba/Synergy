using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Product repository tests
    /// </summary>
    public class ProductRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Product
            {
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                Description = "c5a6c49731",
                IsActive = false
            };

            var result = InstantiatedDependencies.ProductRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProductRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProductRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProductRepository.ReadById(id);

            model.ConcessionTypeId = DataHelper.GetAlternateConcessionTypeId(model.ConcessionTypeId);
            model.Description = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ProductRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProductRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionTypeId, model.ConcessionTypeId);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new Product
            {
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                Description = "c5a6c49731",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ProductRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ProductRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ProductRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
