using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// LoadedPriceTransactional repository tests
    /// </summary>
    public class LoadedPriceTransactionalRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new LoadedPriceTransactional
            {
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                TransactionTableNumberId = DataHelper.GetTransactionTableNumberId()
            };

            var result = InstantiatedDependencies.LoadedPriceTransactionalRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByTransactionTypeIdLegalEntityAccountId executes positive
        /// </summary>
        [Fact]
        public void ReadByTransactionTypeIdLegalEntityAccountId_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadAll();
            var transactionTypeId = results.First().TransactionTypeId;
            var legalEntityAccountId = results.First().LegalEntityAccountId;
            var result =
                InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadByTransactionTypeIdLegalEntityAccountId(
                    transactionTypeId, legalEntityAccountId);

            Assert.NotNull(result);
            Assert.Equal(result.TransactionTypeId, transactionTypeId);
            Assert.Equal(result.LegalEntityAccountId, legalEntityAccountId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadById(id);

            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.TransactionTableNumberId = DataHelper.GetAlternateTransactionTableNumberId(model.TransactionTableNumberId);

            InstantiatedDependencies.LoadedPriceTransactionalRepository.Update(model);

            var updatedModel = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.TransactionTableNumberId, model.TransactionTableNumberId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new LoadedPriceTransactional
            {
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                TransactionTableNumberId = DataHelper.GetTransactionTableNumberId()
            };

            var temporaryEntity = InstantiatedDependencies.LoadedPriceTransactionalRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.LoadedPriceTransactionalRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.LoadedPriceTransactionalRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
