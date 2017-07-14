using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// SubStatus repository tests
    /// </summary>
    public class SubStatusRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new SubStatus
            {
                Description = "040fbcd7d5",
                IsActive = false
            };

            var result = InstantiatedDependencies.SubStatusRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.SubStatusRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.SubStatusRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.SubStatusRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.SubStatusRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.SubStatusRepository.ReadById(id);

            model.Description = "deb052d47c";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.SubStatusRepository.Update(model);

            var updatedModel = InstantiatedDependencies.SubStatusRepository.ReadById(id);

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
            var model = new SubStatus
            {
                Description = "040fbcd7d5",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.SubStatusRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.SubStatusRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.SubStatusRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
