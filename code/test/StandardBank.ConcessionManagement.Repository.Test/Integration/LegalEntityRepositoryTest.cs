using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// LegalEntity repository tests
    /// </summary>
    public class LegalEntityRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new LegalEntity
            {
                MarketSegmentId = DataHelper.GetMarketSegmentId(),
                RiskGroupId = DataHelper.GetRiskGroupId(),
                RiskGroupName = "29bf11668d",
                CustomerName = "311273f3c2",
                CustomerNumber = "c46e397eeb",
                IsActive = false
            };

            var result = InstantiatedDependencies.LegalEntityRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.LegalEntityRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupId executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupId_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var riskGroupId = results.First().RiskGroupId;
            var result = InstantiatedDependencies.LegalEntityRepository.ReadByRiskGroupId(riskGroupId);

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
            var result = InstantiatedDependencies.LegalEntityRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.LegalEntityRepository.ReadById(id);

            model.MarketSegmentId = DataHelper.GetAlternateMarketSegmentId(model.MarketSegmentId);
            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.RiskGroupName = "9d9679a6b0";
            model.CustomerName = "e300b22636";
            model.CustomerNumber = "6d77d219be";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.LegalEntityRepository.Update(model);

            var updatedModel = InstantiatedDependencies.LegalEntityRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.MarketSegmentId, model.MarketSegmentId);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.RiskGroupName, model.RiskGroupName);
            Assert.Equal(updatedModel.CustomerName, model.CustomerName);
            Assert.Equal(updatedModel.CustomerNumber, model.CustomerNumber);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new LegalEntity
            {
                MarketSegmentId = DataHelper.GetMarketSegmentId(),
                RiskGroupId = DataHelper.GetRiskGroupId(),
                RiskGroupName = "29bf11668d",
                CustomerName = "311273f3c2",
                CustomerNumber = "c46e397eeb",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.LegalEntityRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.LegalEntityRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.LegalEntityRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
