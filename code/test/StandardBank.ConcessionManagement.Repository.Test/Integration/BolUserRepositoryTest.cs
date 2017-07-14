using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// BolUser repository tests
    /// </summary>
    public class BolUserRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new BolUser
            {
                UserName = "75ba15ab3a",
                IsActive = false
            };

            var result = InstantiatedDependencies.BolUserRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.BolUserRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.BolUserRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.BolUserRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.BolUserRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.BolUserRepository.ReadById(id);

            model.UserName = "cc2bc1d1a4";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.BolUserRepository.Update(model);

            var updatedModel = InstantiatedDependencies.BolUserRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.UserName, model.UserName);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new BolUser
            {
                UserName = "75ba15ab3a",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.BolUserRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.BolUserRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.BolUserRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
