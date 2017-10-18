using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// LoadedPriceCash repository tests
    /// </summary>
    public class LoadedPriceCashRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new LoadedPriceCash
            {
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                TableNumberId = DataHelper.GetTableNumberId()
            };

            var result = InstantiatedDependencies.LoadedPriceCashRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceCashRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.LoadedPriceCashRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByChannelTypeIdLegalEntityAccountId executes positive
        /// </summary>
        [Fact]
        public void ReadByChannelTypeIdLegalEntityAccountId_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceCashRepository.ReadAll();
            var channelTypeId = results.First().ChannelTypeId;
            var legalEntityAccountId = results.First().LegalEntityAccountId;
            var result =
                InstantiatedDependencies.LoadedPriceCashRepository.ReadByChannelTypeIdLegalEntityAccountId(
                    channelTypeId, legalEntityAccountId);

            Assert.NotNull(result);
            Assert.Equal(result.ChannelTypeId, channelTypeId);
            Assert.Equal(result.LegalEntityAccountId, legalEntityAccountId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.LoadedPriceCashRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceCashRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.LoadedPriceCashRepository.ReadById(id);

            model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.TableNumberId = DataHelper.GetAlternateTableNumberId(model.TableNumberId);

            InstantiatedDependencies.LoadedPriceCashRepository.Update(model);

            var updatedModel = InstantiatedDependencies.LoadedPriceCashRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.TableNumberId, model.TableNumberId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new LoadedPriceCash
            {
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                TableNumberId = DataHelper.GetTableNumberId()
            };

            var temporaryEntity = InstantiatedDependencies.LoadedPriceCashRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.LoadedPriceCashRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.LoadedPriceCashRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
