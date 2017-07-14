using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionType repository tests
    /// </summary>
    public class ConcessionTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionType
            {
                Description = "aa32f3374f",
                Code = "2af5560d97",
                IsActive = false
            };

            var result = InstantiatedDependencies.ConcessionTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionTypeRepository.ReadById(id);

            model.Description = "73a42000e4";
            model.Code = "a335e40073";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConcessionTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionTypeRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.Code, model.Code);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionType
            {
                Description = "aa32f3374f",
                Code = "2af5560d97",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
