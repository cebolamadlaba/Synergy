using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// LegalEntityAccount repository tests
    /// </summary>
    public class LegalEntityAccountRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new LegalEntityAccount
            {
                LegalEntityId = DataHelper.GetLegalEntityId(),
                AccountNumber = "2dec4034da",
                IsActive = false
            };

            var result = InstantiatedDependencies.LegalEntityAccountRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityAccountRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.LegalEntityAccountRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.LegalEntityAccountRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.LegalEntityAccountRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.LegalEntityAccountRepository.ReadById(id);

            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.AccountNumber = "31dc11bf2e";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.LegalEntityAccountRepository.Update(model);

            var updatedModel = InstantiatedDependencies.LegalEntityAccountRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.AccountNumber, model.AccountNumber);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new LegalEntityAccount
            {
                LegalEntityId = DataHelper.GetLegalEntityId(),
                AccountNumber = "2dec4034da",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.LegalEntityAccountRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.LegalEntityAccountRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.LegalEntityAccountRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
