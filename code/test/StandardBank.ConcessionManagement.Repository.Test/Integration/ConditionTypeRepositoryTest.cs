using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConditionType repository tests
    /// </summary>
    public class ConditionTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConditionType
            {
                Description = "80b4de48dd",
                IsActive = false
            };

            var result = InstantiatedDependencies.ConditionTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConditionTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConditionTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConditionTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConditionTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConditionTypeRepository.ReadById(id);

            model.Description = "e2a9a23373";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConditionTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConditionTypeRepository.ReadById(id);

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
            var model = new ConditionType
            {
                Description = "80b4de48dd",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConditionTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConditionTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConditionTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
