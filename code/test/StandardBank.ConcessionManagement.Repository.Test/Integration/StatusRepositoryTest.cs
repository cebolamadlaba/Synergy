using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Status repository tests
    /// </summary>
    public class StatusRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Status
            {
                Description = "2818e7bd68",
                IsActive = false
            };

            var result = InstantiatedDependencies.StatusRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.StatusRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.StatusRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.StatusRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.StatusRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.StatusRepository.ReadById(id);

            model.Description = "3cc7b972ed";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.StatusRepository.Update(model);

            var updatedModel = InstantiatedDependencies.StatusRepository.ReadById(id);

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
            var model = new Status
            {
                Description = "2818e7bd68",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.StatusRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.StatusRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.StatusRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
