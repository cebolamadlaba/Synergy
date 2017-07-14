using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// TransactionGroup repository tests
    /// </summary>
    public class TransactionGroupRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new TransactionGroup
            {
                Description = "49b17e6319",
                IsActive = false
            };

            var result = InstantiatedDependencies.TransactionGroupRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionGroupRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.TransactionGroupRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.TransactionGroupRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionGroupRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.TransactionGroupRepository.ReadById(id);

            model.Description = "4f177dac6f";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.TransactionGroupRepository.Update(model);

            var updatedModel = InstantiatedDependencies.TransactionGroupRepository.ReadById(id);

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
            var model = new TransactionGroup
            {
                Description = "49b17e6319",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.TransactionGroupRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.TransactionGroupRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.TransactionGroupRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
