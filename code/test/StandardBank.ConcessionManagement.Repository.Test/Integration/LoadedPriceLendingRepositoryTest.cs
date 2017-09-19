using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// LoadedPriceLending repository tests
    /// </summary>
    public class LoadedPriceLendingRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new LoadedPriceLending
            {
                ProductTypeId = DataHelper.GetProductId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                MarginToPrime = 8866
            };

            var result = InstantiatedDependencies.LoadedPriceLendingRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceLendingRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.LoadedPriceLendingRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByProductTypeIdLegalEntityAccountId executes positive
        /// </summary>
        [Fact]
        public void ReadByProductTypeIdLegalEntityAccountId_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceLendingRepository.ReadAll();
            var productTypeId = results.First().ProductTypeId;
            var legalEntityAccountId = results.First().LegalEntityAccountId;
            var result =
                InstantiatedDependencies.LoadedPriceLendingRepository.ReadByProductTypeIdLegalEntityAccountId(
                    productTypeId, legalEntityAccountId);

            Assert.NotNull(result);
            Assert.Equal(result.ProductTypeId, productTypeId);
            Assert.Equal(result.LegalEntityAccountId, legalEntityAccountId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.LoadedPriceLendingRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.LoadedPriceLendingRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.LoadedPriceLendingRepository.ReadById(id);

            model.ProductTypeId = DataHelper.GetAlternateProductId(model.ProductTypeId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.MarginToPrime = model.MarginToPrime + 100;

            InstantiatedDependencies.LoadedPriceLendingRepository.Update(model);

            var updatedModel = InstantiatedDependencies.LoadedPriceLendingRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ProductTypeId, model.ProductTypeId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.MarginToPrime, model.MarginToPrime);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new LoadedPriceLending
            {
                ProductTypeId = DataHelper.GetProductId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                MarginToPrime = 8866
            };

            var temporaryEntity = InstantiatedDependencies.LoadedPriceLendingRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.LoadedPriceLendingRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.LoadedPriceLendingRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
