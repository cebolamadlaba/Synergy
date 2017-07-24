using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// CentreUser repository tests
    /// </summary>
    public class CentreUserRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new CentreUser
            {
                CentreId = DataHelper.GetCentreId(),
                UserId = DataHelper.GetUserId(),
                IsActive = false
            };

            var result = InstantiatedDependencies.CentreUserRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreUserRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.CentreUserRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByUserId executes positive
        /// </summary>
        [Fact]
        public void ReadByUserId_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreUserRepository.ReadAll();
            var userId = results.First().UserId;
            var result = InstantiatedDependencies.CentreUserRepository.ReadByUserId(userId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.UserId, userId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.CentreUserRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreUserRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.CentreUserRepository.ReadById(id);

            model.CentreId = DataHelper.GetAlternateCentreId(model.CentreId);
            model.UserId = DataHelper.GetAlternateUserId(model.UserId);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.CentreUserRepository.Update(model);

            var updatedModel = InstantiatedDependencies.CentreUserRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.CentreId, model.CentreId);
            Assert.Equal(updatedModel.UserId, model.UserId);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new CentreUser
            {
                CentreId = DataHelper.GetCentreId(),
                UserId = DataHelper.GetUserId(),
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.CentreUserRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.CentreUserRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.CentreUserRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
