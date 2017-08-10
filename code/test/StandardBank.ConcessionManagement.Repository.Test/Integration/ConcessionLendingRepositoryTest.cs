using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionLending repository tests
    /// </summary>
    public class ConcessionLendingRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionLending
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ProductTypeId = DataHelper.GetProductId(),
                Limit = 1681,
                Term = 9,
                MarginToPrime = 3015,
                ApprovedMarginToPrime = 2343,
                InitiationFee = 4019,
                ReviewFee = 939,
                UFFFee = 8999,
                ReviewFeeTypeId = DataHelper.GetReviewFeeTypeId(),
                LegalEntityId = DataHelper.GetLegalEntityId()
            };

            var result = InstantiatedDependencies.ConcessionLendingRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionLendingRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionLendingRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Reads by concession id executes positive
        /// </summary>
        [Fact]
        public void ReadByConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionLendingRepository.ReadAll();
            var concessionId = results.First().ConcessionId;
            var result = InstantiatedDependencies.ConcessionLendingRepository.ReadByConcessionId(concessionId);

            Assert.NotNull(result);
            Assert.Equal(result.ConcessionId, concessionId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionLendingRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionLendingRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionLendingRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ProductTypeId = DataHelper.GetAlternateProductId(model.ProductTypeId);
            model.Limit = model.Limit + 100;
            model.Term = model.Term + 1;
            model.MarginToPrime = model.MarginToPrime + 100;
            model.ApprovedMarginToPrime = model.ApprovedMarginToPrime + 123;
            model.InitiationFee = model.InitiationFee + 100;
            model.ReviewFee = model.ReviewFee + 100;
            model.UFFFee = model.UFFFee + 100;
            model.ReviewFeeTypeId = DataHelper.GetAlternateReviewFeeTypeId(model.ReviewFeeTypeId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);

            InstantiatedDependencies.ConcessionLendingRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionLendingRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ProductTypeId, model.ProductTypeId);
            Assert.Equal(updatedModel.Limit, model.Limit);
            Assert.Equal(updatedModel.Term, model.Term);
            Assert.Equal(updatedModel.MarginToPrime, model.MarginToPrime);
            Assert.Equal(updatedModel.ApprovedMarginToPrime, model.ApprovedMarginToPrime);
            Assert.Equal(updatedModel.InitiationFee, model.InitiationFee);
            Assert.Equal(updatedModel.ReviewFee, model.ReviewFee);
            Assert.Equal(updatedModel.UFFFee, model.UFFFee);
            Assert.Equal(updatedModel.ReviewFeeTypeId, model.ReviewFeeTypeId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionLending
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ProductTypeId = DataHelper.GetProductId(),
                Limit = 1681,
                Term = 9,
                MarginToPrime = 3015,
                ApprovedMarginToPrime = 2234,
                InitiationFee = 4019,
                ReviewFee = 939,
                UFFFee = 8999,
                ReviewFeeTypeId = DataHelper.GetReviewFeeTypeId(),
                LegalEntityId = DataHelper.GetLegalEntityId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionLendingRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionLendingRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionLendingRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
