using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// AdValorem repository tests
    /// </summary>
    public class AdValoremRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new AdValorem
            {
                Amount = 3744,
                IsActive = false
            };

            var result = InstantiatedDependencies.AdValoremRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.AdValoremRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.AdValoremRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.AdValoremRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.AdValoremRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.AdValoremRepository.ReadById(id);

            model.Amount = model.Amount + 100;
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.AdValoremRepository.Update(model);

            var updatedModel = InstantiatedDependencies.AdValoremRepository.ReadById(id);

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
            var model = new AdValorem
            {
                Amount = 3744,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.AdValoremRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.AdValoremRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.AdValoremRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
