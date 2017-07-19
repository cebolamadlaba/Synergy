using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ExceptionLog repository tests
    /// </summary>
    public class ExceptionLogRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ExceptionLog
            {
                ExceptionMessage = "4abbac3cc4",
                ExceptionType = "6fc67d6417",
                ExceptionSource = "324c527d42",
                ExceptionData = "25da5616a1",
                Logdate = DateTime.Now
            };

            var result = InstantiatedDependencies.ExceptionLogRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.ExceptionLogId, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ExceptionLogRepository.ReadAll();
            var id = results.First().ExceptionLogId;
            var result = InstantiatedDependencies.ExceptionLogRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.ExceptionLogId, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ExceptionLogRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ExceptionLogRepository.ReadAll();
            var id = results.First().ExceptionLogId;
            var model = InstantiatedDependencies.ExceptionLogRepository.ReadById(id);

            model.ExceptionMessage = "c9cdb89d24";
            model.ExceptionType = "acb222ee70";
            model.ExceptionSource = "bac1c2665c";
            model.ExceptionData = "37ce5e4ccb";
            model.Logdate = DataHelper.ChangeDate(model.Logdate);

            InstantiatedDependencies.ExceptionLogRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ExceptionLogRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.ExceptionLogId, model.ExceptionLogId);
            Assert.Equal(updatedModel.ExceptionMessage, model.ExceptionMessage);
            Assert.Equal(updatedModel.ExceptionType, model.ExceptionType);
            Assert.Equal(updatedModel.ExceptionSource, model.ExceptionSource);
            Assert.Equal(updatedModel.ExceptionData, model.ExceptionData);
            Assert.Equal(updatedModel.Logdate, model.Logdate);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ExceptionLog
            {
                ExceptionMessage = "4abbac3cc4",
                ExceptionType = "6fc67d6417",
                ExceptionSource = "324c527d42",
                ExceptionData = "25da5616a1",
                Logdate = DateTime.Now
            };

            var temporaryEntity = InstantiatedDependencies.ExceptionLogRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.ExceptionLogId, 0);

            InstantiatedDependencies.ExceptionLogRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ExceptionLogRepository.ReadById(temporaryEntity.ExceptionLogId);

            Assert.Null(result);
        }
    }
}
