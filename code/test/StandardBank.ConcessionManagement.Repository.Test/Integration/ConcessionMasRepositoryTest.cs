using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionMas repository tests
    /// </summary>
    public class ConcessionMasRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionMas
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                MerchantNumber = "96bc9a6a9f",
                Turnover = 7518,
                CommissionRate = 8728,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var result = InstantiatedDependencies.ConcessionMasRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionMasRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionMasRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionMasRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionMasRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionMasRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ConcessionDetailId = DataHelper.GetAlternateConcessionDetailId(model.ConcessionDetailId);
            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.MerchantNumber = "e048e1db42";
            model.Turnover = model.Turnover + 100;
            model.CommissionRate = model.CommissionRate + 100;
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);

            InstantiatedDependencies.ConcessionMasRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionMasRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ConcessionDetailId, model.ConcessionDetailId);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            Assert.Equal(updatedModel.MerchantNumber, model.MerchantNumber);
            Assert.Equal(updatedModel.Turnover, model.Turnover);
            Assert.Equal(updatedModel.CommissionRate, model.CommissionRate);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionMas
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                MerchantNumber = "96bc9a6a9f",
                Turnover = 7518,
                CommissionRate = 8728,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionMasRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionMasRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionMasRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
