using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ProductInvestment repository tests
    /// </summary>
    public class ProductInvestmentRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ProductInvestment
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                ProductId = DataHelper.GetProductId(),
                AverageBalance = 5172,
                LoadedCustomerRate = 8110
            };

            var result = InstantiatedDependencies.ProductInvestmentRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductInvestmentRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ProductInvestmentRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ProductInvestmentRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ProductInvestmentRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ProductInvestmentRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.ProductId = DataHelper.GetAlternateProductId(model.ProductId);
            model.AverageBalance = model.AverageBalance + 100;
            model.LoadedCustomerRate = model.LoadedCustomerRate + 100;

            InstantiatedDependencies.ProductInvestmentRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ProductInvestmentRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.ProductId, model.ProductId);
            Assert.Equal(updatedModel.AverageBalance, model.AverageBalance);
            Assert.Equal(updatedModel.LoadedCustomerRate, model.LoadedCustomerRate);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ProductInvestment
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                ProductId = DataHelper.GetProductId(),
                AverageBalance = 5172,
                LoadedCustomerRate = 8110
            };

            var temporaryEntity = InstantiatedDependencies.ProductInvestmentRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ProductInvestmentRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ProductInvestmentRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
