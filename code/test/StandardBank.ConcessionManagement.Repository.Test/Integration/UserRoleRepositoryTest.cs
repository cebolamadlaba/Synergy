using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// UserRole repository tests
    /// </summary>
    public class UserRoleRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new UserRole
            {
                UserId = DataHelper.GetUserId(),
                RoleId = DataHelper.GetRoleId(),
                IsActive = false
            };

            var result = InstantiatedDependencies.UserRoleRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRoleRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.UserRoleRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.UserRoleRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRoleRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.UserRoleRepository.ReadById(id);

            model.UserId = DataHelper.GetAlternateUserId(model.UserId);
            model.RoleId = DataHelper.GetAlternateRoleId(model.RoleId);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.UserRoleRepository.Update(model);

            var updatedModel = InstantiatedDependencies.UserRoleRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.UserId, model.UserId);
            Assert.Equal(updatedModel.RoleId, model.RoleId);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new UserRole
            {
                UserId = DataHelper.GetUserId(),
                RoleId = DataHelper.GetRoleId(),
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.UserRoleRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.UserRoleRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.UserRoleRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
