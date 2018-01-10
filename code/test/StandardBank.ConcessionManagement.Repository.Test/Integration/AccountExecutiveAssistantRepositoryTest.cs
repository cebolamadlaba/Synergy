using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// AccountExecutiveAssistant repository tests
    /// </summary>
    public class AccountExecutiveAssistantRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var aaUserId = DataHelper.GetUserId();

            var model = new AccountExecutiveAssistant
            {
                AccountAssistantUserId = aaUserId,
                AccountExecutiveUserId = DataHelper.GetAlternateUserId(aaUserId),
                IsActive = true
            };

            var result = InstantiatedDependencies.AccountExecutiveAssistantRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByAccountAssistantUserId executes positive.
        /// </summary>
        [Fact]
        public void ReadByAccountAssistantUserId_Executes_Positive()
        {
            var results = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadAll();
            var accountAssistantUserId = results.First(_ => _.IsActive).AccountAssistantUserId;
            var result =
                InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadByAccountAssistantUserId(
                    accountAssistantUserId);

            Assert.NotNull(result);
            Assert.Equal(result.AccountAssistantUserId, accountAssistantUserId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadById(id);

            model.AccountAssistantUserId = DataHelper.GetAlternateUserId(model.AccountAssistantUserId);
            model.AccountExecutiveUserId = DataHelper.GetAlternateUserId(model.AccountExecutiveUserId);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.AccountExecutiveAssistantRepository.Update(model);

            var updatedModel = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.AccountAssistantUserId, model.AccountAssistantUserId);
            Assert.Equal(updatedModel.AccountExecutiveUserId, model.AccountExecutiveUserId);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var aaUserId = DataHelper.GetUserId();

            var model = new AccountExecutiveAssistant
            {
                AccountAssistantUserId = aaUserId,
                AccountExecutiveUserId = DataHelper.GetAlternateUserId(aaUserId),
                IsActive = true
            };

            var temporaryEntity = InstantiatedDependencies.AccountExecutiveAssistantRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.AccountExecutiveAssistantRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.AccountExecutiveAssistantRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
