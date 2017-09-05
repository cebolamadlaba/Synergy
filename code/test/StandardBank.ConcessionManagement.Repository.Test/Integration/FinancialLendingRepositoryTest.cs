using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// FinancialLending repository tests
    /// </summary>
    public class FinancialLendingRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new FinancialLending
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                TotalExposure = 4211,
                WeightedAverageMap = 8186,
                WeightedCrsOrMrs = 1680,
                LatestCrsOrMrs = 123.45m
            };

            var result = InstantiatedDependencies.FinancialLendingRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialLendingRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.FinancialLendingRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupId executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupId_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialLendingRepository.ReadAll();
            var riskGroupId = results.First().RiskGroupId;
            var result = InstantiatedDependencies.FinancialLendingRepository.ReadByRiskGroupId(riskGroupId);

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
            var result = InstantiatedDependencies.FinancialLendingRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialLendingRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.FinancialLendingRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.TotalExposure = model.TotalExposure + 100;
            model.WeightedAverageMap = model.WeightedAverageMap + 100;
            model.WeightedCrsOrMrs = model.WeightedCrsOrMrs + 100;
            model.LatestCrsOrMrs = model.LatestCrsOrMrs + 100;

            InstantiatedDependencies.FinancialLendingRepository.Update(model);

            var updatedModel = InstantiatedDependencies.FinancialLendingRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.TotalExposure, model.TotalExposure);
            Assert.Equal(updatedModel.WeightedAverageMap, model.WeightedAverageMap);
            Assert.Equal(updatedModel.WeightedCrsOrMrs, model.WeightedCrsOrMrs);
            Assert.Equal(updatedModel.LatestCrsOrMrs, model.LatestCrsOrMrs);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new FinancialLending
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                TotalExposure = 4211,
                WeightedAverageMap = 8186,
                WeightedCrsOrMrs = 1680,
                LatestCrsOrMrs = 6523.35m
            };

            var temporaryEntity = InstantiatedDependencies.FinancialLendingRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.FinancialLendingRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.FinancialLendingRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
