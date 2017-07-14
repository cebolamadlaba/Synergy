using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Province repository tests
    /// </summary>
    public class ProvinceRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Province
            {
                Description = "9c81a3ad64",
                IsActive = false
            };

            var result = InstantiatedDependencies.ProvinceRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProvinceRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProvinceRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProvinceRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProvinceRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProvinceRepository.ReadById(id);

            model.Description = "2dde3c4b01";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ProvinceRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProvinceRepository.ReadById(id);

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
            var model = new Province
            {
                Description = "9c81a3ad64",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ProvinceRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ProvinceRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ProvinceRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
