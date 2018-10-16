using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionTrade repository tests
    /// </summary>
    public class ConcessionTradeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionTrade
            {
            //    ConcessionId = DataHelper.GetConcessionId(),
            //    ConcessionDetailId = DataHelper.GetConcessionDetailId(),
            //    TransactionTypeId = DataHelper.GetTransactionTypeId(),
            //    ChannelTypeId = DataHelper.GetChannelTypeId(),
            //    TableNumber = 6,
            //    TransactionVolume = 9,
            //    TransactionValue = 5402,
            //    BaseRateId = DataHelper.GetBaseRateId(),
            //    AdValorem = 1376,
            //    LegalEntityId = DataHelper.GetLegalEntityId(),
            //    LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var result = InstantiatedDependencies.ConcessionTradeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionTradeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionTradeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionTradeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            //var results = InstantiatedDependencies.ConcessionTradeRepository.ReadAll();
            //var id = results.First().Id;
            //var model = InstantiatedDependencies.ConcessionTradeRepository.ReadById(id);

            //model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            //model.ConcessionDetailId = DataHelper.GetAlternateConcessionDetailId(model.ConcessionDetailId);
            //model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            //model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            //model.TableNumber = model.TableNumber + 1;
            //model.TransactionVolume = model.TransactionVolume + 1;
            //model.TransactionValue = model.TransactionValue + 100;
            //model.BaseRateId = DataHelper.GetAlternateBaseRateId(model.BaseRateId);
            //model.AdValorem = model.AdValorem + 100;
            //model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            //model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);

            //InstantiatedDependencies.ConcessionTradeRepository.Update(model);

            //var updatedModel = InstantiatedDependencies.ConcessionTradeRepository.ReadById(id);

            //Assert.NotNull(updatedModel);
            //Assert.Equal(updatedModel.Id, model.Id);
            //Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            //Assert.Equal(updatedModel.ConcessionDetailId, model.ConcessionDetailId);
            //Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            //Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            //Assert.Equal(updatedModel.TableNumber, model.TableNumber);
            //Assert.Equal(updatedModel.TransactionVolume, model.TransactionVolume);
            //Assert.Equal(updatedModel.TransactionValue, model.TransactionValue);
            //Assert.Equal(updatedModel.BaseRateId, model.BaseRateId);
            //Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            //Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            //Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionTrade
            {
                //ConcessionId = DataHelper.GetConcessionId(),
                //ConcessionDetailId = DataHelper.GetConcessionDetailId(),
                //TransactionTypeId = DataHelper.GetTransactionTypeId(),
                //ChannelTypeId = DataHelper.GetChannelTypeId(),
                //TableNumber = 6,
                //TransactionVolume = 9,
                //TransactionValue = 5402,
                //BaseRateId = DataHelper.GetBaseRateId(),
                //AdValorem = 1376,
                //LegalEntityId = DataHelper.GetLegalEntityId(),
                //LegalEntityAccountId = DataHelper.GetLegalEntityAccountId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionTradeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionTradeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionTradeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
