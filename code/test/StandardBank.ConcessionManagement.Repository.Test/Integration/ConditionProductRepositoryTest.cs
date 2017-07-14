using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConditionProduct repository tests
    /// </summary>
    public class ConditionProductRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConditionProduct
            {
                Description = "a410be0cc1",
                IsActive = false
            };

            var result = InstantiatedDependencies.ConditionProductRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConditionProductRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConditionProductRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConditionProductRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConditionProductRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConditionProductRepository.ReadById(id);

            model.Description = "05d626d0ac";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConditionProductRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConditionProductRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConditionProduct
            {
                Description = "a410be0cc1",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConditionProductRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConditionProductRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConditionProductRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
