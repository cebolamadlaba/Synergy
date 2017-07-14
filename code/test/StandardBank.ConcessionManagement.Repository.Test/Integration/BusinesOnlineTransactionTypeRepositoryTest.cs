using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// BusinesOnlineTransactionType repository tests
    /// </summary>
    public class BusinesOnlineTransactionTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new BusinesOnlineTransactionType
            {
                TransactionGroupId = DataHelper.GetTransactionGroupId(),
                Description = "c0a59567d2",
                IsActive = false
            };

            var result = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadById(id);

            model.TransactionGroupId = DataHelper.GetAlternateTransactionGroupId(model.TransactionGroupId);
            model.Description = "2306c17019";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TransactionGroupId, model.TransactionGroupId);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new BusinesOnlineTransactionType
            {
                TransactionGroupId = DataHelper.GetTransactionGroupId(),
                Description = "c0a59567d2",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.BusinesOnlineTransactionTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
