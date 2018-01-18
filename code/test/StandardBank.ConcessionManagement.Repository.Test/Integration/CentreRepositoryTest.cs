using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Centre repository tests
    /// </summary>
    public class CentreRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Centre
            {
                RegionId = DataHelper.GetRegionId(),
                CentreName = "7827c21c2e",
                IsActive = false
            };

            var result = InstantiatedDependencies.CentreRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.CentreRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.CentreRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.CentreRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.CentreRepository.ReadById(id);

            model.RegionId = DataHelper.GetAlternateRegionId(model.RegionId);
            model.CentreName = "19a72a641f";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.CentreRepository.Update(model);

            var updatedModel = InstantiatedDependencies.CentreRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RegionId, model.RegionId);
            Assert.Equal(updatedModel.CentreName, model.CentreName);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new Centre
            {
                RegionId = DataHelper.GetRegionId(),
                CentreName = "7827c21c2e",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.CentreRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.CentreRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.CentreRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
