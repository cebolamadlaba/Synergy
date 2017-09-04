using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ProductLending repository tests
    /// </summary>
    public class ProductLendingRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ProductLending
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                ProductId = DataHelper.GetProductId(),
                Limit = 2432,
                AverageBalance = 7454,
                LoadedMap = 4725
            };

            var result = InstantiatedDependencies.ProductLendingRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductLendingRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProductLendingRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProductLendingRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductLendingRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProductLendingRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.ProductId = DataHelper.GetAlternateProductId(model.ProductId);
            model.Limit = model.Limit + 100;
            model.AverageBalance = model.AverageBalance + 100;
            model.LoadedMap = model.LoadedMap + 100;

            InstantiatedDependencies.ProductLendingRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProductLendingRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.ProductId, model.ProductId);
            Assert.Equal(updatedModel.Limit, model.Limit);
            Assert.Equal(updatedModel.AverageBalance, model.AverageBalance);
            Assert.Equal(updatedModel.LoadedMap, model.LoadedMap);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ProductLending
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                ProductId = DataHelper.GetProductId(),
                Limit = 2432,
                AverageBalance = 7454,
                LoadedMap = 4725
            };

            var temporaryEntity = InstantiatedDependencies.ProductLendingRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ProductLendingRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ProductLendingRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
