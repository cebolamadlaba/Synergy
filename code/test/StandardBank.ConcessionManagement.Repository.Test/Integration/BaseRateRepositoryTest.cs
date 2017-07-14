using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// BaseRate repository tests
    /// </summary>
    public class BaseRateRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new BaseRate
            {
                Amount = 7103,
                IsActive = false
            };

            var result = InstantiatedDependencies.BaseRateRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.BaseRateRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.BaseRateRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.BaseRateRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.BaseRateRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.BaseRateRepository.ReadById(id);

            model.Amount = model.Amount + 100;
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.BaseRateRepository.Update(model);

            var updatedModel = InstantiatedDependencies.BaseRateRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.Amount, model.Amount);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new BaseRate
            {
                Amount = 7103,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.BaseRateRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.BaseRateRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.BaseRateRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
