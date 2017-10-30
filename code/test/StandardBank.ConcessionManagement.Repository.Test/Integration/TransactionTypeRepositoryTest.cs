using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// TransactionType repository tests
    /// </summary>
    public class TransactionTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new TransactionType
            {
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                Description = "aed25144ff",
                IsActive = true,
                ImportFileProductId = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)
            };

            var result = InstantiatedDependencies.TransactionTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.TransactionTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByConcessionTypeIdIsActive executes positive.
        /// </summary>
        [Fact]
        public void ReadByConcessionTypeIdIsActive_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTypeRepository.ReadAll();
            var concessionTypeId = results.First(_ => _.IsActive && _.ConcessionTypeId.HasValue).ConcessionTypeId.Value;
            var result = InstantiatedDependencies.TransactionTypeRepository.ReadByConcessionTypeIdIsActive(concessionTypeId, true);

            Assert.NotNull(result);

            foreach (var record in result)
                Assert.Equal(record.ConcessionTypeId, concessionTypeId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.TransactionTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.TransactionTypeRepository.ReadById(id);

            model.ConcessionTypeId = DataHelper.GetAlternateConcessionTypeId(model.ConcessionTypeId);
            model.Description = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            model.IsActive = !model.IsActive;
            model.ImportFileProductId = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);

            InstantiatedDependencies.TransactionTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.TransactionTypeRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionTypeId, model.ConcessionTypeId);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
            Assert.Equal(updatedModel.ImportFileProductId, model.ImportFileProductId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new TransactionType
            {
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                Description = "aed25144ff",
                IsActive = false,
                ImportFileProductId = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10)
            };

            var temporaryEntity = InstantiatedDependencies.TransactionTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.TransactionTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.TransactionTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
