using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// RiskGroup repository tests
    /// </summary>
    public class RiskGroupRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new RiskGroup
            {
                RiskGroupNumber = 9,
                RiskGroupName = "3b84541fd8",
                IsActive = true,
                MarketSegmentId = DataHelper.GetMarketSegmentId(),
                RegionId = DataHelper.GetRegionId()
            };

            var result = InstantiatedDependencies.RiskGroupRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.RiskGroupRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.RiskGroupRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupNumber for an active record executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupNumberIsActive_ActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.RiskGroupRepository.ReadAll();
            var riskGroupNumber = results.First(_ => _.IsActive).RiskGroupNumber;
            var result = InstantiatedDependencies.RiskGroupRepository.ReadByRiskGroupNumberIsActive(riskGroupNumber, true);

            Assert.NotNull(result);
            Assert.Equal(result.RiskGroupNumber, riskGroupNumber);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupNumber for an in-active record executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupNumberIsActive_InActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.RiskGroupRepository.ReadAll();
            var riskGroupNumber = results.First(_ => !_.IsActive).RiskGroupNumber;
            var result = InstantiatedDependencies.RiskGroupRepository.ReadByRiskGroupNumberIsActive(riskGroupNumber, false);

            Assert.NotNull(result);
            Assert.Equal(result.RiskGroupNumber, riskGroupNumber);
        }

        /// <summary>
        /// Tests that ReadByIdIsActive for an active record executes positive
        /// </summary>
        [Fact]
        public void ReadByIdIsActive_ActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.RiskGroupRepository.ReadAll();
            var id = results.First(_ => _.IsActive).Id;
            var result = InstantiatedDependencies.RiskGroupRepository.ReadByIdIsActive(id, true);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByIdIsActive for an in-active record executes positive
        /// </summary>
        [Fact]
        public void ReadByIdIsActive_InActiveRecord_Executes_Positive()
        {
            var model = new RiskGroup
            {
                RiskGroupNumber = 9,
                RiskGroupName = "3b84541fd8",
                IsActive = false,
                MarketSegmentId = DataHelper.GetMarketSegmentId(),
                RegionId = DataHelper.GetRegionId()
            };

            InstantiatedDependencies.RiskGroupRepository.Create(model);

            var results = InstantiatedDependencies.RiskGroupRepository.ReadAll();
            var id = results.First(_ => !_.IsActive).Id;
            var result = InstantiatedDependencies.RiskGroupRepository.ReadByIdIsActive(id, false);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.RiskGroupRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.RiskGroupRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.RiskGroupRepository.ReadById(id);

            model.RiskGroupNumber = model.RiskGroupNumber + 1;
            model.RiskGroupName = "1cb39b8591";
            model.IsActive = !model.IsActive;
            model.MarketSegmentId = DataHelper.GetAlternateMarketSegmentId(model.MarketSegmentId);
            model.RegionId = DataHelper.GetAlternateRegionId(model.RegionId);

            InstantiatedDependencies.RiskGroupRepository.Update(model);

            var updatedModel = InstantiatedDependencies.RiskGroupRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupNumber, model.RiskGroupNumber);
            Assert.Equal(updatedModel.RiskGroupName, model.RiskGroupName);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
            Assert.Equal(updatedModel.MarketSegmentId, model.MarketSegmentId);
            Assert.Equal(updatedModel.RegionId, model.RegionId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new RiskGroup
            {
                RiskGroupNumber = 9,
                RiskGroupName = "3b84541fd8",
                IsActive = false,
                MarketSegmentId = DataHelper.GetMarketSegmentId(),
                RegionId = DataHelper.GetRegionId()
            };

            var temporaryEntity = InstantiatedDependencies.RiskGroupRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.RiskGroupRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.RiskGroupRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
