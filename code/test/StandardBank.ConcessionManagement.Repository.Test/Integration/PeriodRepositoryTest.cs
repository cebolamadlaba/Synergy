using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Period repository tests
    /// </summary>
    public class PeriodRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Period
            {
                Description = "ebe53e285b",
                IsActive = false
            };

            var result = InstantiatedDependencies.PeriodRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.PeriodRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.PeriodRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.PeriodRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.PeriodRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.PeriodRepository.ReadById(id);

            model.Description = "595d3b1c9d";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.PeriodRepository.Update(model);

            var updatedModel = InstantiatedDependencies.PeriodRepository.ReadById(id);

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
            var model = new Period
            {
                Description = "ebe53e285b",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.PeriodRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.PeriodRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.PeriodRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
