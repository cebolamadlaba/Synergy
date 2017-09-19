using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionInvestment repository tests
    /// </summary>
    public class ConcessionInvestmentRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        //[Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionInvestment
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ProductTypeId = DataHelper.GetProductId(),
                Balance = 2501,
                Term = 6,
                InterestToCustomer = 5484
            };

            var result = InstantiatedDependencies.ConcessionInvestmentRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionInvestmentRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionInvestmentRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionInvestmentRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionInvestmentRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionInvestmentRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ProductTypeId = DataHelper.GetAlternateProductId(model.ProductTypeId);
            model.Balance = model.Balance + 100;
            model.Term = model.Term + 1;
            model.InterestToCustomer = model.InterestToCustomer + 100;

            InstantiatedDependencies.ConcessionInvestmentRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionInvestmentRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ProductTypeId, model.ProductTypeId);
            Assert.Equal(updatedModel.Balance, model.Balance);
            Assert.Equal(updatedModel.Term, model.Term);
            Assert.Equal(updatedModel.InterestToCustomer, model.InterestToCustomer);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        //[Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionInvestment
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ProductTypeId = DataHelper.GetProductId(),
                Balance = 2501,
                Term = 6,
                InterestToCustomer = 5484
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionInvestmentRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionInvestmentRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionInvestmentRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
