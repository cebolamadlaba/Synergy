using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionApproval repository tests
    /// </summary>
    public class ConcessionApprovalRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionApproval
            {
                ConcessionId = DataHelper.GetConcessionId(),
                OldSubStatusId = DataHelper.GetOldSubStatusId(),
                NewSubStatusId = DataHelper.GetNewSubStatusId(),
                UserId = DataHelper.GetUserId(),
                SystemDate = DateTime.Now,
                IsActive = false
            };

            var result = InstantiatedDependencies.ConcessionApprovalRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionApprovalRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionApprovalRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionApprovalRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionApprovalRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionApprovalRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.OldSubStatusId = DataHelper.GetAlternateOldSubStatusId(model.OldSubStatusId);
            model.NewSubStatusId = DataHelper.GetAlternateNewSubStatusId(model.NewSubStatusId);
            model.UserId = DataHelper.GetAlternateUserId(model.UserId);
            model.SystemDate = DataHelper.ChangeDate(model.SystemDate);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConcessionApprovalRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionApprovalRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.OldSubStatusId, model.OldSubStatusId);
            Assert.Equal(updatedModel.NewSubStatusId, model.NewSubStatusId);
            Assert.Equal(updatedModel.UserId, model.UserId);
            Assert.Equal(updatedModel.SystemDate, model.SystemDate);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionApproval
            {
                ConcessionId = DataHelper.GetConcessionId(),
                OldSubStatusId = DataHelper.GetOldSubStatusId(),
                NewSubStatusId = DataHelper.GetNewSubStatusId(),
                UserId = DataHelper.GetUserId(),
                SystemDate = DateTime.Now,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionApprovalRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionApprovalRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionApprovalRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
