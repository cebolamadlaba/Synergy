using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ApprovalType repository tests
    /// </summary>
    public class ApprovalTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ApprovalType
            {
                Description = "9e6a69e36b",
                IsActive = false
            };

            var result = InstantiatedDependencies.ApprovalTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ApprovalTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ApprovalTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ApprovalTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ApprovalTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ApprovalTypeRepository.ReadById(id);

            model.Description = "3773ff6769";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ApprovalTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ApprovalTypeRepository.ReadById(id);

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
            var model = new ApprovalType
            {
                Description = "9e6a69e36b",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ApprovalTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ApprovalTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ApprovalTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
