using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// FinancialInvestment repository tests
    /// </summary>
    public class FinancialInvestmentRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new FinancialInvestment
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                TotalLiabilityBalances = 7107,
                WeightedAverageMTP = 3635,
                WeightedAverageNetMargin = 1430,
                LatestCrsOrMrs = 75
            };

            var result = InstantiatedDependencies.FinancialInvestmentRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialInvestmentRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.FinancialInvestmentRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.FinancialInvestmentRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialInvestmentRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.FinancialInvestmentRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.TotalLiabilityBalances = model.TotalLiabilityBalances + 100;
            model.WeightedAverageMTP = model.WeightedAverageMTP + 100;
            model.WeightedAverageNetMargin = model.WeightedAverageNetMargin + 100;
            model.LatestCrsOrMrs = model.LatestCrsOrMrs + 100;

            InstantiatedDependencies.FinancialInvestmentRepository.Update(model);

            var updatedModel = InstantiatedDependencies.FinancialInvestmentRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.TotalLiabilityBalances, model.TotalLiabilityBalances);
            Assert.Equal(updatedModel.WeightedAverageMTP, model.WeightedAverageMTP);
            Assert.Equal(updatedModel.WeightedAverageNetMargin, model.WeightedAverageNetMargin);
            Assert.Equal(updatedModel.LatestCrsOrMrs, model.LatestCrsOrMrs);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new FinancialInvestment
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                TotalLiabilityBalances = 7107,
                WeightedAverageMTP = 3635,
                WeightedAverageNetMargin = 1430,
                LatestCrsOrMrs = 75
            };

            var temporaryEntity = InstantiatedDependencies.FinancialInvestmentRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.FinancialInvestmentRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.FinancialInvestmentRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
