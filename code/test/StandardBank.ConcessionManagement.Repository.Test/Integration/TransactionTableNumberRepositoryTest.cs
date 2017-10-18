using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// TransactionTableNumber repository tests
    /// </summary>
    public class TransactionTableNumberRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new TransactionTableNumber
            {
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                TariffTable = 4,
                Fee = 7040,
                AdValorem = 8189,
                IsActive = false
            };

            var result = InstantiatedDependencies.TransactionTableNumberRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTableNumberRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.TransactionTableNumberRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.TransactionTableNumberRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.TransactionTableNumberRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.TransactionTableNumberRepository.ReadById(id);

            model.TransactionTypeId = DataHelper.GetAlternateTransactionTypeId(model.TransactionTypeId);
            model.TariffTable = model.TariffTable + 1;
            model.Fee = model.Fee + 100;
            model.AdValorem = model.AdValorem + 100;
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.TransactionTableNumberRepository.Update(model);

            var updatedModel = InstantiatedDependencies.TransactionTableNumberRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TransactionTypeId, model.TransactionTypeId);
            Assert.Equal(updatedModel.TariffTable, model.TariffTable);
            Assert.Equal(updatedModel.Fee, model.Fee);
            Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new TransactionTableNumber
            {
                TransactionTypeId = DataHelper.GetTransactionTypeId(),
                TariffTable = 4,
                Fee = 7040,
                AdValorem = 8189,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.TransactionTableNumberRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.TransactionTableNumberRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.TransactionTableNumberRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
