using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Relationship repository tests
    /// </summary>
    public class RelationshipRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Relationship
            {
                Description = "bd51146e3e",
                IsActive = false
            };

            var result = InstantiatedDependencies.RelationshipRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.RelationshipRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.RelationshipRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.RelationshipRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.RelationshipRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.RelationshipRepository.ReadById(id);

            model.Description = "07386636e3";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.RelationshipRepository.Update(model);

            var updatedModel = InstantiatedDependencies.RelationshipRepository.ReadById(id);

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
            var model = new Relationship
            {
                Description = "bd51146e3e",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.RelationshipRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.RelationshipRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.RelationshipRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
