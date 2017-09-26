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
        /// Tests that Create an active record executes positive.
        /// </summary>
        [Fact]
        public void Create_Active_Executes_Positive()
        {
            var model = new Concession
            {
                TypeId = DataHelper.GetReferenceTypeId(),
                ConcessionRef = "47c038861f",
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                SMTDealNumber = "777f9e1b48",
                StatusId = DataHelper.GetStatusId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                ConcessionDate = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                Motivation = "455d28a04b",
                DateApproved = DateTime.Now,
                RequestorId = DataHelper.GetUserId(),
                BCMUserId = DataHelper.GetUserId(),
                DateActionedByBCM = DateTime.Now,
                PCMUserId = DataHelper.GetUserId(),
                DateActionedByPCM = DateTime.Now,
                HOUserId = DataHelper.GetUserId(),
                DateActionedByHO = DateTime.Now,
                CentreId = DataHelper.GetCentreId(),
                IsCurrent = true,
                IsActive = true,
                MRS_CRS = 1232,
                RiskGroupId = DataHelper.GetRiskGroupId(),
                RegionId = DataHelper.GetRegionId()
            };

            var result = InstantiatedDependencies.ConcessionRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that Create an inactive record executes positive.
        /// </summary>
        [Fact]
        public void Create_InActive_Executes_Positive()
        {
            var model = new Concession
            {
                TypeId = DataHelper.GetReferenceTypeId(),
                ConcessionRef = "47c038861f",
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                SMTDealNumber = "777f9e1b48",
                StatusId = DataHelper.GetStatusId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                ConcessionDate = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                Motivation = "455d28a04b",
                DateApproved = DateTime.Now,
                RequestorId = DataHelper.GetUserId(),
                BCMUserId = DataHelper.GetUserId(),
                DateActionedByBCM = DateTime.Now,
                PCMUserId = DataHelper.GetUserId(),
                DateActionedByPCM = DateTime.Now,
                HOUserId = DataHelper.GetUserId(),
                DateActionedByHO = DateTime.Now,
                CentreId = DataHelper.GetCentreId(),
                IsCurrent = true,
                IsActive = false,
                MRS_CRS = 1233,
                RiskGroupId = DataHelper.GetRiskGroupId(),
                RegionId = DataHelper.GetRegionId()
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

            model.TypeId = DataHelper.GetAlternateReferenceTypeId(model.TypeId);
            model.ConcessionRef = "325e0ffb67";
            model.ConcessionTypeId = DataHelper.GetAlternateConcessionTypeId(model.ConcessionTypeId);
            model.SMTDealNumber = "8d2fbe86c2";
            model.StatusId = DataHelper.GetAlternateStatusId(model.StatusId);
            model.SubStatusId = DataHelper.GetAlternateSubStatusId(model.SubStatusId);
            model.ConcessionDate = DataHelper.ChangeDate(model.ConcessionDate);
            model.DatesentForApproval = DataHelper.ChangeDate(model.DatesentForApproval);
            model.Motivation = "902f5e8b15";
            model.DateApproved = DataHelper.ChangeDate(model.DateApproved);
            model.RequestorId = DataHelper.GetAlternateUserId(model.RequestorId);
            model.BCMUserId = DataHelper.GetAlternateUserId(model.BCMUserId);
            model.DateActionedByBCM = DataHelper.ChangeDate(model.DateActionedByBCM);
            model.PCMUserId = DataHelper.GetAlternateUserId(model.PCMUserId);
            model.DateActionedByPCM = DataHelper.ChangeDate(model.DateActionedByPCM);
            model.HOUserId = DataHelper.GetAlternateUserId(model.HOUserId);
            model.DateActionedByHO = DataHelper.ChangeDate(model.DateActionedByHO);
            model.CentreId = DataHelper.GetAlternateCentreId(model.CentreId);
            model.IsCurrent = !model.IsCurrent;
            model.IsActive = !model.IsActive;
            model.MRS_CRS = model.MRS_CRS + 123;
            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.RegionId = DataHelper.GetAlternateRegionId(model.RegionId);

            InstantiatedDependencies.ConcessionRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TypeId, model.TypeId);
            Assert.Equal(updatedModel.ConcessionRef, model.ConcessionRef);
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
            Assert.Equal(updatedModel.CentreId, model.CentreId);
            Assert.Equal(updatedModel.IsCurrent, model.IsCurrent);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
            Assert.Equal(updatedModel.MRS_CRS, model.MRS_CRS);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.RegionId, model.RegionId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new Concession
            {
                TypeId = DataHelper.GetReferenceTypeId(),
                ConcessionRef = "47c038861f",
                ConcessionTypeId = DataHelper.GetConcessionTypeId(),
                SMTDealNumber = "777f9e1b48",
                StatusId = DataHelper.GetStatusId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                ConcessionDate = DateTime.Now,
                DatesentForApproval = DateTime.Now,
                Motivation = "455d28a04b",
                DateApproved = DateTime.Now,
                RequestorId = DataHelper.GetUserId(),
                BCMUserId = DataHelper.GetUserId(),
                DateActionedByBCM = DateTime.Now,
                PCMUserId = DataHelper.GetUserId(),
                DateActionedByPCM = DateTime.Now,
                HOUserId = DataHelper.GetUserId(),
                DateActionedByHO = DateTime.Now,
                CentreId = DataHelper.GetCentreId(),
                IsCurrent = false,
                IsActive = false,
                MRS_CRS = 6533,
                RiskGroupId = DataHelper.GetRiskGroupId(),
                RegionId = DataHelper.GetRegionId()
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
