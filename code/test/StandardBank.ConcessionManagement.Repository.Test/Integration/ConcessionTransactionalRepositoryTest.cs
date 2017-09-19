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
            var tableNumberId = DataHelper.GetTableNumberId();

            var model = new ConcessionTransactional
            {
                ConcessionId = DataHelper.GetConcessionId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                TransactionVolume = 9,
                TransactionValue = 557,
                BaseRateId = DataHelper.GetBaseRateId(),
                AdValorem = 9485,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                BaseRate = 70,
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId
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
            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            model.TransactionVolume = model.TransactionVolume + 1;
            model.TransactionValue = model.TransactionValue + 100;
            model.BaseRateId = DataHelper.GetAlternateBaseRateId(model.BaseRateId);
            model.AdValorem = model.AdValorem + 100;
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.BaseRate = model.BaseRate + 100;
            model.TableNumberId = DataHelper.GetAlternateTableNumberId(model.TableNumberId);
            model.ApprovedTableNumberId = DataHelper.GetAlternateTableNumberId(model.ApprovedTableNumberId);
            model.LoadedTableNumberId = DataHelper.GetAlternateTableNumberId(model.LoadedTableNumberId);

            InstantiatedDependencies.ConcessionTransactionalRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionTransactionalRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            Assert.Equal(updatedModel.TransactionVolume, model.TransactionVolume);
            Assert.Equal(updatedModel.TransactionValue, model.TransactionValue);
            Assert.Equal(updatedModel.BaseRateId, model.BaseRateId);
            Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.BaseRate, model.BaseRate);
            Assert.Equal(updatedModel.TableNumberId, model.TableNumberId);
            Assert.Equal(updatedModel.ApprovedTableNumberId, model.ApprovedTableNumberId);
            Assert.Equal(updatedModel.LoadedTableNumberId, model.LoadedTableNumberId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var tableNumberId = DataHelper.GetTableNumberId();

            var model = new ConcessionTransactional
            {
                ConcessionId = DataHelper.GetConcessionId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                TransactionVolume = 9,
                TransactionValue = 557,
                BaseRateId = DataHelper.GetBaseRateId(),
                AdValorem = 9485,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                BaseRate = 70,
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId
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
