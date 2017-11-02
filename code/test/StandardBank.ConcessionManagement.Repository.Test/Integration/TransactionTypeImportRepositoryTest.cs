using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// TransactionTypeImport repository tests
    /// </summary>
    public class TransactionTypeImportRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new TransactionTypeImport
            {
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                ImportFileChannel = "a5ee15a57f"
            };

            var result = InstantiatedDependencies.TransactionTypeImportRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTypeImportRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.TransactionTypeImportRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.TransactionTypeImportRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTypeImportRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.TransactionTypeImportRepository.ReadById(id);

            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.ImportFileChannel = "9e160e6319";

            InstantiatedDependencies.TransactionTypeImportRepository.Update(model);

            var updatedModel = InstantiatedDependencies.TransactionTypeImportRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            Assert.Equal(updatedModel.ImportFileChannel, model.ImportFileChannel);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new TransactionTypeImport
            {
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                ImportFileChannel = "a5ee15a57f"
            };

            var temporaryEntity = InstantiatedDependencies.TransactionTypeImportRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.TransactionTypeImportRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.TransactionTypeImportRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
