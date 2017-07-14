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
                TransactionGroupId = DataHelper.GetTransactionGroupId(),
                BusinesOnlineTransactionTypeId = DataHelper.GetBusinesOnlineTransactionTypeId(),
                BolUseId = 6,
                TransactionVolume = 4,
                TransactionValue = 9692,
                Fee = 6453
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
            model.TransactionGroupId = DataHelper.GetAlternateTransactionGroupId(model.TransactionGroupId);
            model.BusinesOnlineTransactionTypeId = DataHelper.GetAlternateBusinesOnlineTransactionTypeId(model.BusinesOnlineTransactionTypeId);
            model.BolUseId = model.BolUseId + 1;
            model.TransactionVolume = model.TransactionVolume + 1;
            model.TransactionValue = model.TransactionValue + 100;
            model.Fee = model.Fee + 100;

            InstantiatedDependencies.ConcessionBolRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionBolRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.TransactionGroupId, model.TransactionGroupId);
            Assert.Equal(updatedModel.BusinesOnlineTransactionTypeId, model.BusinesOnlineTransactionTypeId);
            Assert.Equal(updatedModel.BolUseId, model.BolUseId);
            Assert.Equal(updatedModel.TransactionVolume, model.TransactionVolume);
            Assert.Equal(updatedModel.TransactionValue, model.TransactionValue);
            Assert.Equal(updatedModel.Fee, model.Fee);
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
                TransactionGroupId = DataHelper.GetTransactionGroupId(),
                BusinesOnlineTransactionTypeId = DataHelper.GetBusinesOnlineTransactionTypeId(),
                BolUseId = 6,
                TransactionVolume = 4,
                TransactionValue = 9692,
                Fee = 6453
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
