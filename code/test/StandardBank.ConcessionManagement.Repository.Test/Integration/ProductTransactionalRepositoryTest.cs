using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ProductTransactional repository tests
    /// </summary>
    public class ProductTransactionalRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ProductTransactional
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                TableNumberId = DataHelper.GetTableNumberId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                Volume = 6070,
                Value = 5104,
                LoadedPrice = "b046b4e111"
            };

            var result = InstantiatedDependencies.ProductTransactionalRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProductTransactionalRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProductTransactionalRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProductTransactionalRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.TableNumberId = DataHelper.GetAlternateTableNumberId(model.TableNumberId);
            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.Volume = model.Volume + 100;
            model.Value = model.Value + 100;
            model.LoadedPrice = "7eef89e49c";

            InstantiatedDependencies.ProductTransactionalRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProductTransactionalRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.TableNumberId, model.TableNumberId);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
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
            var model = new ProductTransactional
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                TableNumberId = DataHelper.GetTableNumberId(),
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                Volume = 6070,
                Value = 5104,
                LoadedPrice = "b046b4e111"
            };

            var temporaryEntity = InstantiatedDependencies.ProductTransactionalRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ProductTransactionalRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ProductTransactionalRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
