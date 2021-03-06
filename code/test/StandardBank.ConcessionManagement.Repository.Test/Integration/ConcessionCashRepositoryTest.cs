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
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                AdValorem = 2263,
                BaseRate = 1233,
                AccrualTypeId = DataHelper.GetAccrualTypeId(),
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
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
            model.ConcessionDetailId = DataHelper.GetAlternateConcessionDetailId(model.ConcessionDetailId);
            model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            model.AdValorem = model.AdValorem + 100;
            model.BaseRate = model.BaseRate + 100;
            model.AccrualTypeId = DataHelper.GetAlternateAccrualTypeId(model.AccrualTypeId);
            model.TableNumberId = DataHelper.GetAlternateTableNumberId(model.TableNumberId);
            model.LoadedTableNumberId = DataHelper.GetAlternateTableNumberId(model.LoadedTableNumberId);
            model.ApprovedTableNumberId = DataHelper.GetAlternateTableNumberId(model.ApprovedTableNumberId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);

            InstantiatedDependencies.ConcessionCashRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionCashRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ConcessionDetailId, model.ConcessionDetailId);
            Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            Assert.Equal(updatedModel.BaseRate, model.BaseRate);
            Assert.Equal(updatedModel.AccrualTypeId, model.AccrualTypeId);
            Assert.Equal(updatedModel.TableNumberId, model.TableNumberId);
            Assert.Equal(updatedModel.LoadedTableNumberId, model.LoadedTableNumberId);
            Assert.Equal(updatedModel.ApprovedTableNumberId, model.ApprovedTableNumberId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
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
                ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                AdValorem = 2263,
                BaseRate = 653,
                AccrualTypeId = DataHelper.GetAccrualTypeId(),
                TableNumberId = tableNumberId,
                ApprovedTableNumberId = tableNumberId,
                LoadedTableNumberId = tableNumberId,
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
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
