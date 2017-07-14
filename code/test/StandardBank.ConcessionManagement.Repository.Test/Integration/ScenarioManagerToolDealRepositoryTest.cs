using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ScenarioManagerToolDeal repository tests
    /// </summary>
    public class ScenarioManagerToolDealRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ScenarioManagerToolDeal
            {
                DealNumber = "11c385aa6f",
                IsActive = false
            };

            var result = InstantiatedDependencies.ScenarioManagerToolDealRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadById(id);

            model.DealNumber = "1c712059fd";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ScenarioManagerToolDealRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.DealNumber, model.DealNumber);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ScenarioManagerToolDeal
            {
                DealNumber = "11c385aa6f",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ScenarioManagerToolDealRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ScenarioManagerToolDealRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ScenarioManagerToolDealRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
