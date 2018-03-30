using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ProductCash repository tests
    /// </summary>
    public class ProductCashRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            //var model = new ProductCash
            //{
            //    RiskGroupId = DataHelper.GetRiskGroupId(),
            //    LegalEntityId = DataHelper.GetLegalEntityId(),
            //    LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
            //    TableNumberId = DataHelper.GetTableNumberId(),
            //    Channel = "7b05323752",
            //    BpId = 8,
            //    Volume = 3077,
            //    Value = 8268,
            //    LoadedPrice = 8604
            //};

            //var result = InstantiatedDependencies.ProductCashRepository.Create(model);

            //Assert.NotNull(result);
            //Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductCashRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProductCashRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupId executes positive.
        /// </summary>
        [Fact]
        public void ReadByRiskGroupId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductCashRepository.ReadAll();
            var riskGroupId = results.First().RiskGroupId;
            var result = InstantiatedDependencies.ProductCashRepository.ReadByRiskGroupId(riskGroupId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.RiskGroupId, riskGroupId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProductCashRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductCashRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProductCashRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.TableNumberId = DataHelper.GetAlternateTableNumberId(model.TableNumberId);
            model.Channel = "3165132a02";
            model.BpId = model.BpId + 1;
            model.Volume = model.Volume + 100;
            model.Value = model.Value + 100;
            model.LoadedPrice = model.LoadedPrice + 100;

            InstantiatedDependencies.ProductCashRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProductCashRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.TableNumberId, model.TableNumberId);
            Assert.Equal(updatedModel.Channel, model.Channel);
            Assert.Equal(updatedModel.BpId, model.BpId);
            Assert.Equal(updatedModel.Volume, model.Volume);
            Assert.Equal(updatedModel.Value, model.Value);
            Assert.Equal(updatedModel.LoadedPrice, model.LoadedPrice);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            //var model = new ProductCash
            //{
            //    RiskGroupId = DataHelper.GetRiskGroupId(),
            //    LegalEntityId = DataHelper.GetLegalEntityId(),
            //    LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
            //    TableNumberId = DataHelper.GetTableNumberId(),
            //    Channel = "7b05323752",
            //    BpId = 8,
            //    Volume = 3077,
            //    Value = 8268,
            //    LoadedPrice = 8604
            //};

            //var temporaryEntity = InstantiatedDependencies.ProductCashRepository.Create(model);

            //Assert.NotNull(temporaryEntity);
            //Assert.NotEqual(temporaryEntity.Id, 0);

            //InstantiatedDependencies.ProductCashRepository.Delete(temporaryEntity);

            //var result = InstantiatedDependencies.ProductCashRepository.ReadById(temporaryEntity.Id);

            //Assert.Null(result);
        }
    }
}
