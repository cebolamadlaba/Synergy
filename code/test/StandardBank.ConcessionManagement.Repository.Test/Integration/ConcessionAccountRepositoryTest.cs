using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionAccount repository tests
    /// </summary>
    public class ConcessionAccountRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionAccount
            {
                ConcessionId = DataHelper.GetConcessionId(),
                AccountNumber = "7d3f4e378a",
                IsActive = false
            };

            var result = InstantiatedDependencies.ConcessionAccountRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionAccountRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionAccountRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionAccountRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionAccountRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionAccountRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.AccountNumber = "f53f59a0dc";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConcessionAccountRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionAccountRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.AccountNumber, model.AccountNumber);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionAccount
            {
                ConcessionId = DataHelper.GetConcessionId(),
                AccountNumber = "7d3f4e378a",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionAccountRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionAccountRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionAccountRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
