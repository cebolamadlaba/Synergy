using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// Concession repository tests
    /// </summary>
    public class ConcessionRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new Concession
            {
                TypeId = DataHelper.GetTypeId(),
                ConcessionRef = "47c038861f",
                LegalEntityId = DataHelper.GetLegalEntityId(),
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                SMTDealNumber = "777f9e1b48",
                StatusId = DataHelper.GetStatusId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                ConcessionDate = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                Motivation = "455d28a04b",
                DateApproved = DateTime.Now,
                RequestorId = DataHelper.GetRequestorId(),
                BCMUserId = DataHelper.GetBCMUserId(),
                DateActionedByBCM = DateTime.Now,
                PCMUserId = DataHelper.GetPCMUserId(),
                DateActionedByPCM = DateTime.Now,
                HOUserId = DataHelper.GetHOUserId(),
                DateActionedByHO = DateTime.Now,
                ExpiryDate = DateTime.Now,
                CentreId = 3,
                IsCurrent = false,
                IsActive = false
            };

            var result = InstantiatedDependencies.ConcessionRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionRepository.ReadById(id);

            model.TypeId = DataHelper.GetAlternateTypeId(model.TypeId);
            model.ConcessionRef = "325e0ffb67";
            model.LegalEntityId = DataHelper.GetAlternateLegalEntityId(model.LegalEntityId);
            model.ConcessionTypeId = DataHelper.GetAlternateConcessionTypeId(model.ConcessionTypeId);
            model.SMTDealNumber = "8d2fbe86c2";
            model.StatusId = DataHelper.GetAlternateStatusId(model.StatusId);
            model.SubStatusId = DataHelper.GetAlternateSubStatusId(model.SubStatusId);
            model.ConcessionDate = DataHelper.ChangeDate(model.ConcessionDate);
            model.DatesentForApproval = DataHelper.ChangeDate(model.DatesentForApproval);
            model.Motivation = "902f5e8b15";
            model.DateApproved = DataHelper.ChangeDate(model.DateApproved);
            model.RequestorId = DataHelper.GetAlternateRequestorId(model.RequestorId);
            model.BCMUserId = DataHelper.GetAlternateBCMUserId(model.BCMUserId);
            model.DateActionedByBCM = DataHelper.ChangeDate(model.DateActionedByBCM);
            model.PCMUserId = DataHelper.GetAlternatePCMUserId(model.PCMUserId);
            model.DateActionedByPCM = DataHelper.ChangeDate(model.DateActionedByPCM);
            model.HOUserId = DataHelper.GetAlternateHOUserId(model.HOUserId);
            model.DateActionedByHO = DataHelper.ChangeDate(model.DateActionedByHO);
            model.ExpiryDate = DataHelper.ChangeDate(model.ExpiryDate);
            model.CentreId = model.CentreId + 1;
            model.IsCurrent = !model.IsCurrent;
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConcessionRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TypeId, model.TypeId);
            Assert.Equal(updatedModel.ConcessionRef, model.ConcessionRef);
            Assert.Equal(updatedModel.LegalEntityId, model.LegalEntityId);
            Assert.Equal(updatedModel.ConcessionTypeId, model.ConcessionTypeId);
            Assert.Equal(updatedModel.SMTDealNumber, model.SMTDealNumber);
            Assert.Equal(updatedModel.StatusId, model.StatusId);
            Assert.Equal(updatedModel.SubStatusId, model.SubStatusId);
            Assert.Equal(updatedModel.ConcessionDate, model.ConcessionDate);
            Assert.Equal(updatedModel.DatesentForApproval, model.DatesentForApproval);
            Assert.Equal(updatedModel.Motivation, model.Motivation);
            Assert.Equal(updatedModel.DateApproved, model.DateApproved);
            Assert.Equal(updatedModel.RequestorId, model.RequestorId);
            Assert.Equal(updatedModel.BCMUserId, model.BCMUserId);
            Assert.Equal(updatedModel.DateActionedByBCM, model.DateActionedByBCM);
            Assert.Equal(updatedModel.PCMUserId, model.PCMUserId);
            Assert.Equal(updatedModel.DateActionedByPCM, model.DateActionedByPCM);
            Assert.Equal(updatedModel.HOUserId, model.HOUserId);
            Assert.Equal(updatedModel.DateActionedByHO, model.DateActionedByHO);
            Assert.Equal(updatedModel.ExpiryDate, model.ExpiryDate);
            Assert.Equal(updatedModel.CentreId, model.CentreId);
            Assert.Equal(updatedModel.IsCurrent, model.IsCurrent);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new Concession
            {
                TypeId = DataHelper.GetTypeId(),
                ConcessionRef = "47c038861f",
                LegalEntityId = DataHelper.GetLegalEntityId(),
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                SMTDealNumber = "777f9e1b48",
                StatusId = DataHelper.GetStatusId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                ConcessionDate = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                Motivation = "455d28a04b",
                DateApproved = DateTime.Now,
                RequestorId = DataHelper.GetRequestorId(),
                BCMUserId = DataHelper.GetBCMUserId(),
                DateActionedByBCM = DateTime.Now,
                PCMUserId = DataHelper.GetPCMUserId(),
                DateActionedByPCM = DateTime.Now,
                HOUserId = DataHelper.GetHOUserId(),
                DateActionedByHO = DateTime.Now,
                ExpiryDate = DateTime.Now,
                CentreId = 3,
                IsCurrent = false,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
