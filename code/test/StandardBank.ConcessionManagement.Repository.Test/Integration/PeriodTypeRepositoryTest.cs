using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// PeriodType repository tests
    /// </summary>
    public class PeriodTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new PeriodType
            {
                Description = "a6158ab6d1",
                IsActive = false
            };

            var result = InstantiatedDependencies.PeriodTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.PeriodTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.PeriodTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.PeriodTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.PeriodTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.PeriodTypeRepository.ReadById(id);

            model.Description = "384df42a2a";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.PeriodTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.PeriodTypeRepository.ReadById(id);

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
            var model = new PeriodType
            {
                Description = "a6158ab6d1",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.PeriodTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.PeriodTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.PeriodTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
