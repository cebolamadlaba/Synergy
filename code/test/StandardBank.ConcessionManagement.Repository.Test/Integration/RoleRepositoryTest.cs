using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Role repository tests
    /// </summary>
    public class RoleRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Role
            {
                RoleName = "bcf1f77d08",
                RoleDescription = "43d0fad165",
                IsActive = false
            };

            var result = InstantiatedDependencies.RoleRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.RoleRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.RoleRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.RoleRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.RoleRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.RoleRepository.ReadById(id);

            model.RoleName = "36da1150a7";
            model.RoleDescription = "ee4e876626";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.RoleRepository.Update(model);

            var updatedModel = InstantiatedDependencies.RoleRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RoleName, model.RoleName);
            Assert.Equal(updatedModel.RoleDescription, model.RoleDescription);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new Role
            {
                RoleName = "bcf1f77d08",
                RoleDescription = "43d0fad165",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.RoleRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.RoleRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.RoleRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
