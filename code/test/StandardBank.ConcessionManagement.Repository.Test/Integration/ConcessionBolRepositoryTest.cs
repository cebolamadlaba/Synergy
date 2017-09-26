using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionBol repository tests
    /// </summary>
    public class ConcessionBolRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionBol
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                TransactionGroupId = DataHelper.GetTransactionGroupId(),
                BusinesOnlineTransactionTypeId = DataHelper.GetBusinesOnlineTransactionTypeId(),
                BolUseId = DataHelper.GetBolUserId(),
                TransactionVolume = 4,
                TransactionValue = 9692,
                Fee = 6453,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var result = InstantiatedDependencies.ConcessionBolRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionBolRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionBolRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionBolRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionBolRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionBolRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ConcessionDetailId = DataHelper.GetAlternateConcessionDetailId(model.ConcessionDetailId);
            model.TransactionGroupId = DataHelper.GetAlternateTransactionGroupId(model.TransactionGroupId);
            model.BusinesOnlineTransactionTypeId = DataHelper.GetAlternateBusinesOnlineTransactionTypeId(model.BusinesOnlineTransactionTypeId);
            model.BolUseId = DataHelper.GetAlternateBolUserId(model.BolUseId);
            model.TransactionVolume = model.TransactionVolume + 1;
            model.TransactionValue = model.TransactionValue + 100;
            model.Fee = model.Fee + 100;
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);

            InstantiatedDependencies.ConcessionBolRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionBolRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ConcessionDetailId, model.ConcessionDetailId);
            Assert.Equal(updatedModel.TransactionGroupId, model.TransactionGroupId);
            Assert.Equal(updatedModel.BusinesOnlineTransactionTypeId, model.BusinesOnlineTransactionTypeId);
            Assert.Equal(updatedModel.BolUseId, model.BolUseId);
            Assert.Equal(updatedModel.TransactionVolume, model.TransactionVolume);
            Assert.Equal(updatedModel.TransactionValue, model.TransactionValue);
            Assert.Equal(updatedModel.Fee, model.Fee);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionBol
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                TransactionGroupId = DataHelper.GetTransactionGroupId(),
                BusinesOnlineTransactionTypeId = DataHelper.GetBusinesOnlineTransactionTypeId(),
                BolUseId = DataHelper.GetBolUserId(),
                TransactionVolume = 4,
                TransactionValue = 9692,
                Fee = 6453,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionBolRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionBolRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionBolRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
