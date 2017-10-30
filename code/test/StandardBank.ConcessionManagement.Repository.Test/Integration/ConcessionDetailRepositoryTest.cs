using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionDetail repository tests
    /// </summary>
    public class ConcessionDetailRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionDetail
            {
                ConcessionId = DataHelper.GetConcessionId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                ExpiryDate = DateTime.Now,
                DateApproved = DateTime.Now,
                IsMismatched = false,
                PriceExported = true,
                PriceExportedDate = DateTime.Now
            };

            var result = InstantiatedDependencies.ConcessionDetailRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.ConcessionDetailId, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionDetailRepository.ReadAll();
            var concessionDetailId = results.First().ConcessionDetailId;
            var result = InstantiatedDependencies.ConcessionDetailRepository.ReadById(concessionDetailId);

            Assert.NotNull(result);
            Assert.Equal(result.ConcessionDetailId, concessionDetailId);
        }

        /// <summary>
        /// Tests that ReadByConcessionId executes positive.
        /// </summary>
        [Fact]
        public void ReadByConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionDetailRepository.ReadAll();
            var concessionId = results.First().ConcessionId;
            var result = InstantiatedDependencies.ConcessionDetailRepository.ReadByConcessionId(concessionId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.ConcessionId, concessionId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionDetailRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionDetailRepository.ReadAll();
            var concessionDetailId = results.First().ConcessionDetailId;
            var model = InstantiatedDependencies.ConcessionDetailRepository.ReadById(concessionDetailId);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.LegalEntityAccountId = DataHelper.GetAlternateLegalEntityAccountId(model.LegalEntityAccountId);
            model.ExpiryDate = DataHelper.ChangeDate(model.ExpiryDate);
            model.DateApproved = DataHelper.ChangeDate(model.DateApproved);
            model.IsMismatched = !model.IsMismatched;
            model.PriceExported = !model.PriceExported;
            model.PriceExportedDate = DataHelper.ChangeDate(model.PriceExportedDate);

            InstantiatedDependencies.ConcessionDetailRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionDetailRepository.ReadById(concessionDetailId);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.ConcessionDetailId, model.ConcessionDetailId);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.LegalEntityAccountId, model.LegalEntityAccountId);
            Assert.Equal(updatedModel.ExpiryDate.Value.Date, model.ExpiryDate.Value.Date);
            Assert.Equal(updatedModel.DateApproved.Value.Date, model.DateApproved.Value.Date);
            Assert.Equal(updatedModel.IsMismatched, model.IsMismatched);
            Assert.Equal(updatedModel.PriceExported, model.PriceExported);
            Assert.Equal(updatedModel.PriceExportedDate.Value.Date, model.PriceExportedDate.Value.Date);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionDetail
            {
                ConcessionId = DataHelper.GetConcessionId(),
                LegalEntityId = DataHelper.GetLegalEntityId(),
                LegalEntityAccountId = DataHelper.GetLegalEntityAccountId(),
                ExpiryDate = DateTime.Now,
                DateApproved = DateTime.Now,
                IsMismatched = false,
                PriceExported = true,
                PriceExportedDate = DateTime.Now
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionDetailRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.ConcessionDetailId, 0);

            InstantiatedDependencies.ConcessionDetailRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionDetailRepository.ReadById(temporaryEntity.ConcessionDetailId);

            Assert.Null(result);
        }
    }
}
