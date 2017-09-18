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
                CustomerName = "311273f3c2",
                CustomerNumber = "c46e397eeb",
                IsActive = true,
                City = "Joburg",
                ContactPerson = "Bob",
                PostalAddress = "123 Somewhere Street",
                PostalCode = "2001"
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
        /// Tests that ReadByIdIsActive for an active record executes positive
        /// </summary>
        [Fact]
        public void ReadByIdIsActive_ActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var id = results.First(_ => _.IsActive).Id;
            var result = InstantiatedDependencies.LegalEntityRepository.ReadByIdIsActive(id, true);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByIdIsActive for an in-active record executes positive
        /// </summary>
        [Fact]
        public void ReadByIdIsActive_InActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var id = results.First(_ => !_.IsActive).Id;
            var result = InstantiatedDependencies.LegalEntityRepository.ReadByIdIsActive(id, false);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupIdIsActive for an active record executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupIdIsActive_ActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var riskGroupId = results.First(_ => _.IsActive).RiskGroupId;
            var result = InstantiatedDependencies.LegalEntityRepository.ReadByRiskGroupIdIsActive(riskGroupId, true);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.RiskGroupId, riskGroupId);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupIdIsActive for an in-active record executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupIdIsActive_InActiveRecord_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityRepository.ReadAll();
            var riskGroupId = results.First(_ => !_.IsActive).RiskGroupId;
            var result = InstantiatedDependencies.LegalEntityRepository.ReadByRiskGroupIdIsActive(riskGroupId, false);

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
            model.CustomerName = "e300b22636";
            model.CustomerNumber = "6d77d219be";
            model.IsActive = !model.IsActive;
            model.City = "Cape Town";
            model.ContactPerson = "John";
            model.PostalAddress = "999 Here Street";
            model.PostalCode = "5000";

            InstantiatedDependencies.LegalEntityRepository.Update(model);

            var updatedModel = InstantiatedDependencies.LegalEntityRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.MarketSegmentId, model.MarketSegmentId);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.CustomerName, model.CustomerName);
            Assert.Equal(updatedModel.CustomerNumber, model.CustomerNumber);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
            Assert.Equal(updatedModel.City, model.City);
            Assert.Equal(updatedModel.ContactPerson, model.ContactPerson);
            Assert.Equal(updatedModel.PostalAddress, model.PostalAddress);
            Assert.Equal(updatedModel.PostalCode, model.PostalCode);
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
                CustomerName = "311273f3c2",
                CustomerNumber = "c46e397eeb",
                IsActive = false,
                City = "Joburg",
                ContactPerson = "Bob",
                PostalAddress = "123 Somewhere Street",
                PostalCode = "2001"
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
