using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionComment repository tests
    /// </summary>
    public class ConcessionCommentRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionComment
            {
                ConcessionId = DataHelper.GetConcessionId(),
                UserId = DataHelper.GetUserId(),
                ConcessionSubStatusId = DataHelper.GetConcessionSubStatusId(),
                Comment = "65a2826633",
                SystemDate = DateTime.Now,
                IsActive = false
            };

            var result = InstantiatedDependencies.ConcessionCommentRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionCommentRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionCommentRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionCommentRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionCommentRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionCommentRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.UserId = DataHelper.GetAlternateUserId(model.UserId);
            model.ConcessionSubStatusId = DataHelper.GetAlternateConcessionSubStatusId(model.ConcessionSubStatusId);
            model.Comment = "5e4d9db259";
            model.SystemDate = DataHelper.ChangeDate(model.SystemDate);
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ConcessionCommentRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionCommentRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.UserId, model.UserId);
            Assert.Equal(updatedModel.ConcessionSubStatusId, model.ConcessionSubStatusId);
            Assert.Equal(updatedModel.Comment, model.Comment);
            Assert.Equal(updatedModel.SystemDate, model.SystemDate);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionComment
            {
                ConcessionId = DataHelper.GetConcessionId(),
                UserId = DataHelper.GetUserId(),
                ConcessionSubStatusId = DataHelper.GetConcessionSubStatusId(),
                Comment = "65a2826633",
                SystemDate = DateTime.Now,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionCommentRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionCommentRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionCommentRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
