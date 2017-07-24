using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// UserRegion repository tests
    /// </summary>
    public class UserRegionRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new UserRegion
            {
                UserId = DataHelper.GetUserId(),
                RegionId = DataHelper.GetRegionId(),
                IsActive = false
            };

            var result = InstantiatedDependencies.UserRegionRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRegionRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.UserRegionRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByUserId executes positive
        /// </summary>
        [Fact]
        public void ReadByUserId_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRegionRepository.ReadAll();
            var userId = results.First().UserId;
            var resultsForUserId = InstantiatedDependencies.UserRegionRepository.ReadByUserId(userId);

            Assert.NotNull(resultsForUserId);
            Assert.NotEmpty(resultsForUserId);

            foreach (var resultForUserId in resultsForUserId)
                Assert.Equal(resultForUserId.UserId, userId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.UserRegionRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRegionRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.UserRegionRepository.ReadById(id);

            model.UserId = DataHelper.GetAlternateUserId(model.UserId);
            model.RegionId = DataHelper.GetAlternateRegionId(model.RegionId);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.UserRegionRepository.Update(model);

            var updatedModel = InstantiatedDependencies.UserRegionRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.UserId, model.UserId);
            Assert.Equal(updatedModel.RegionId, model.RegionId);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new UserRegion
            {
                UserId = DataHelper.GetUserId(),
                RegionId = DataHelper.GetRegionId(),
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.UserRegionRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.UserRegionRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.UserRegionRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
