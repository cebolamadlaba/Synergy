using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// FinancialCash repository tests
    /// </summary>
    public class FinancialCashRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new FinancialCash
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                WeightedAverageBranchPrice = 3422,
                TotalCashCentrCashTurnover = 1269,
                TotalCashCentrCashVolume = 2703,
                TotalBranchCashTurnover = 6402,
                TotalBranchCashVolume = 6304,
                TotalAutosafeCashTurnover = 4895,
                TotalAutosafeCashVolume = 8788,
                WeightedAverageCCPrice = 2238,
                WeightedAverageAFPrice = 8684,
                LatestCrsOrMrs = 5427,
                LoadedPrice = 123.54m
            };

            var result = InstantiatedDependencies.FinancialCashRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialCashRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.FinancialCashRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupId executes positive.
        /// </summary>
        [Fact]
        public void ReadByRiskGroupId_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialCashRepository.ReadAll();
            var riskGroupId = results.First().RiskGroupId;
            var result = InstantiatedDependencies.FinancialCashRepository.ReadByRiskGroupId(riskGroupId);

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
            var result = InstantiatedDependencies.FinancialCashRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialCashRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.FinancialCashRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.WeightedAverageBranchPrice = model.WeightedAverageBranchPrice + 100;
            model.TotalCashCentrCashTurnover = model.TotalCashCentrCashTurnover + 100;
            model.TotalCashCentrCashVolume = model.TotalCashCentrCashVolume + 100;
            model.TotalBranchCashTurnover = model.TotalBranchCashTurnover + 100;
            model.TotalBranchCashVolume = model.TotalBranchCashVolume + 100;
            model.TotalAutosafeCashTurnover = model.TotalAutosafeCashTurnover + 100;
            model.TotalAutosafeCashVolume = model.TotalAutosafeCashVolume + 100;
            model.WeightedAverageCCPrice = model.WeightedAverageCCPrice + 100;
            model.WeightedAverageAFPrice = model.WeightedAverageAFPrice + 100;
            model.LatestCrsOrMrs = model.LatestCrsOrMrs + 100;
            model.LoadedPrice = model.LoadedPrice + 100;

            InstantiatedDependencies.FinancialCashRepository.Update(model);

            var updatedModel = InstantiatedDependencies.FinancialCashRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.WeightedAverageBranchPrice, model.WeightedAverageBranchPrice);
            Assert.Equal(updatedModel.TotalCashCentrCashTurnover, model.TotalCashCentrCashTurnover);
            Assert.Equal(updatedModel.TotalCashCentrCashVolume, model.TotalCashCentrCashVolume);
            Assert.Equal(updatedModel.TotalBranchCashTurnover, model.TotalBranchCashTurnover);
            Assert.Equal(updatedModel.TotalBranchCashVolume, model.TotalBranchCashVolume);
            Assert.Equal(updatedModel.TotalAutosafeCashTurnover, model.TotalAutosafeCashTurnover);
            Assert.Equal(updatedModel.TotalAutosafeCashVolume, model.TotalAutosafeCashVolume);
            Assert.Equal(updatedModel.WeightedAverageCCPrice, model.WeightedAverageCCPrice);
            Assert.Equal(updatedModel.WeightedAverageAFPrice, model.WeightedAverageAFPrice);
            Assert.Equal(updatedModel.LatestCrsOrMrs, model.LatestCrsOrMrs);
            Assert.Equal(updatedModel.LoadedPrice, model.LoadedPrice);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new FinancialCash
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                WeightedAverageBranchPrice = 3422,
                TotalCashCentrCashTurnover = 1269,
                TotalCashCentrCashVolume = 2703,
                TotalBranchCashTurnover = 6402,
                TotalBranchCashVolume = 6304,
                TotalAutosafeCashTurnover = 4895,
                TotalAutosafeCashVolume = 8788,
                WeightedAverageCCPrice = 2238,
                WeightedAverageAFPrice = 8684,
                LatestCrsOrMrs = 5427,
                LoadedPrice = 5432.67m
            };

            var temporaryEntity = InstantiatedDependencies.FinancialCashRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.FinancialCashRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.FinancialCashRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
