using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionCash repository tests
    /// </summary>
    public class ConcessionCashRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var tableNumberId = DataHelper.GetTableNumberId();

            var model = new ConcessionCash
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                CashVolume = 5,
                CashValue = 4110,
                BaseRateId = DataHelper.GetBaseRateId(),
                AdValorem = 2263,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                BaseRate = 1233,
                AccrualTypeId = DataHelper.GetAccrualTypeId(),
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId
            };

            var result = InstantiatedDependencies.ConcessionCashRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionCashRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionCashRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByConcessionId executes positive.
        /// </summary>
        [Fact]
        public void ReadByConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionCashRepository.ReadAll();
            var concessionId = results.First().ConcessionId;
            var result = InstantiatedDependencies.ConcessionCashRepository.ReadByConcessionId(concessionId);

            Assert.NotNull(result);

            foreach (var record in result)
                Assert.Equal(record.ConcessionId, concessionId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionCashRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionCashRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionCashRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            model.CashVolume = model.CashVolume + 1;
            model.CashValue = model.CashValue + 100;
            model.BaseRateId = DataHelper.GetAlternateBaseRateId(model.BaseRateId);
            model.AdValorem = model.AdValorem + 100;
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.BaseRate = model.BaseRate + 100;
            model.AccrualTypeId = DataHelper.GetAlternateAccrualTypeId(model.AccrualTypeId);
            model.TableNumberId = DataHelper.GetAlternateTableNumberId(model.TableNumberId);
            model.LoadedTableNumberId = DataHelper.GetAlternateTableNumberId(model.LoadedTableNumberId);
            model.ApprovedTableNumberId = DataHelper.GetAlternateTableNumberId(model.ApprovedTableNumberId);

            InstantiatedDependencies.ConcessionCashRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionCashRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            Assert.Equal(updatedModel.CashVolume, model.CashVolume);
            Assert.Equal(updatedModel.CashValue, model.CashValue);
            Assert.Equal(updatedModel.BaseRateId, model.BaseRateId);
            Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.BaseRate, model.BaseRate);
            Assert.Equal(updatedModel.AccrualTypeId, model.AccrualTypeId);
            Assert.Equal(updatedModel.TableNumberId, model.TableNumberId);
            Assert.Equal(updatedModel.LoadedTableNumberId, model.LoadedTableNumberId);
            Assert.Equal(updatedModel.ApprovedTableNumberId, model.ApprovedTableNumberId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var tableNumberId = DataHelper.GetTableNumberId();

            var model = new ConcessionCash
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                CashVolume = 5,
                CashValue = 4110,
                BaseRateId = DataHelper.GetBaseRateId(),
                AdValorem = 2263,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                BaseRate = 653,
                AccrualTypeId = DataHelper.GetAccrualTypeId(),
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionCashRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionCashRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionCashRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
