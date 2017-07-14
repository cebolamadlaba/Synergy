using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionRemovalRequest repository tests
    /// </summary>
    public class ConcessionRemovalRequestRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionRemovalRequest
            {
                ConcessionId = DataHelper.GetConcessionId(),
                RequestorId = 5,
                BCMUserId = DataHelper.GetBCMUserId(),
                PCMUserId = DataHelper.GetPCMUserId(),
                HOUserId = DataHelper.GetHOUserId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                SystemDate = DateTime.Now,
                DateApproved = DateTime.Now
            };

            var result = InstantiatedDependencies.ConcessionRemovalRequestRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.RequestorId = model.RequestorId + 1;
            model.BCMUserId = DataHelper.GetAlternateBCMUserId(model.BCMUserId);
            model.PCMUserId = DataHelper.GetAlternatePCMUserId(model.PCMUserId);
            model.HOUserId = DataHelper.GetAlternateHOUserId(model.HOUserId);
            model.SubStatusId = DataHelper.GetAlternateSubStatusId(model.SubStatusId);
            model.SystemDate = DataHelper.ChangeDate(model.SystemDate);
            model.DateApproved = DataHelper.ChangeDate(model.DateApproved);

            InstantiatedDependencies.ConcessionRemovalRequestRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.RequestorId, model.RequestorId);
            Assert.Equal(updatedModel.BCMUserId, model.BCMUserId);
            Assert.Equal(updatedModel.PCMUserId, model.PCMUserId);
            Assert.Equal(updatedModel.HOUserId, model.HOUserId);
            Assert.Equal(updatedModel.SubStatusId, model.SubStatusId);
            Assert.Equal(updatedModel.SystemDate, model.SystemDate);
            Assert.Equal(updatedModel.DateApproved, model.DateApproved);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionRemovalRequest
            {
                ConcessionId = DataHelper.GetConcessionId(),
                RequestorId = 5,
                BCMUserId = DataHelper.GetBCMUserId(),
                PCMUserId = DataHelper.GetPCMUserId(),
                HOUserId = DataHelper.GetHOUserId(),
                SubStatusId = DataHelper.GetSubStatusId(),
                SystemDate = DateTime.Now,
                DateApproved = DateTime.Now
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionRemovalRequestRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionRemovalRequestRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionRemovalRequestRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
