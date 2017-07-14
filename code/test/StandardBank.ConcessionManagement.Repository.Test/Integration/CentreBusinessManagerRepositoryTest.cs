using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// CentreBusinessManager repository tests
    /// </summary>
    public class CentreBusinessManagerRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new CentreBusinessManager
            {
                CentreId = DataHelper.GetCentreId(),
                UserId = DataHelper.GetUserId(),
                IsActive = false
            };

            var result = InstantiatedDependencies.CentreBusinessManagerRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreBusinessManagerRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.CentreBusinessManagerRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.CentreBusinessManagerRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreBusinessManagerRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.CentreBusinessManagerRepository.ReadById(id);

            model.CentreId = DataHelper.GetAlternateCentreId(model.CentreId);
            model.UserId = DataHelper.GetAlternateUserId(model.UserId);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.CentreBusinessManagerRepository.Update(model);

            var updatedModel = InstantiatedDependencies.CentreBusinessManagerRepository.ReadById(id);

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
            var model = new CentreBusinessManager
            {
                CentreId = DataHelper.GetCentreId(),
                UserId = DataHelper.GetUserId(),
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.CentreBusinessManagerRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.CentreBusinessManagerRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.CentreBusinessManagerRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
