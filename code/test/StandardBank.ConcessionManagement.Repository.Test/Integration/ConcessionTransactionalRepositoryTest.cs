using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionTransactional repository tests
    /// </summary>
    public class ConcessionTransactionalRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var TransactionTableNumberId = DataHelper.GetTransactionTableNumberId();

            var model = new ConcessionTransactional
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                AdValorem = 9485,
                Fee = 70,
                TransactionTableNumberId = TransactionTableNumberId,
                ApprovedTransactionTableNumberId = TransactionTableNumberId,
                LoadedTransactionTableNumberId = TransactionTableNumberId,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var result = InstantiatedDependencies.ConcessionTransactionalRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionTransactionalRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Reads by concession id executes positive
        /// </summary>
        [Fact]
        public void ReadByConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionTransactionalRepository.ReadAll();
            var concessionId = results.First().ConcessionId;
            var result = InstantiatedDependencies.ConcessionTransactionalRepository.ReadByConcessionId(concessionId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.ConcessionId, concessionId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionTransactionalRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionTransactionalRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ConcessionDetailId = DataHelper.GetAlternateConcessionDetailId(model.ConcessionDetailId);
            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.AdValorem = model.AdValorem + 100;
            model.Fee = model.Fee + 100;
            model.TransactionTableNumberId = DataHelper.GetAlternateTransactionTableNumberId(model.TransactionTableNumberId);
            model.ApprovedTransactionTableNumberId = DataHelper.GetAlternateTransactionTableNumberId(model.ApprovedTransactionTableNumberId);
            model.LoadedTransactionTableNumberId = DataHelper.GetAlternateTransactionTableNumberId(model.LoadedTransactionTableNumberId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);

            InstantiatedDependencies.ConcessionTransactionalRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionTransactionalRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ConcessionDetailId, model.ConcessionDetailId);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            Assert.Equal(updatedModel.Fee, model.Fee);
            Assert.Equal(updatedModel.TransactionTableNumberId, model.TransactionTableNumberId);
            Assert.Equal(updatedModel.ApprovedTransactionTableNumberId, model.ApprovedTransactionTableNumberId);
            Assert.Equal(updatedModel.LoadedTransactionTableNumberId, model.LoadedTransactionTableNumberId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var TransactionTableNumberId = DataHelper.GetTransactionTableNumberId();

            var model = new ConcessionTransactional
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                AdValorem = 9485,
                Fee = 70,
                TransactionTableNumberId = TransactionTableNumberId,
                ApprovedTransactionTableNumberId = TransactionTableNumberId,
                LoadedTransactionTableNumberId = TransactionTableNumberId,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionTransactionalRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionTransactionalRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionTransactionalRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
