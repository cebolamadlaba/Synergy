using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Region repository tests
    /// </summary>
    public class RegionRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Region
            {
                Description = "fc95c1a610",
                IsActive = false
            };

            var result = InstantiatedDependencies.RegionRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.RegionRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.RegionRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.RegionRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.RegionRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.RegionRepository.ReadById(id);

            model.Description = "17a6caacee";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.RegionRepository.Update(model);

            var updatedModel = InstantiatedDependencies.RegionRepository.ReadById(id);

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
            var model = new Region
            {
                Description = "fc95c1a610",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.RegionRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.RegionRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.RegionRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
