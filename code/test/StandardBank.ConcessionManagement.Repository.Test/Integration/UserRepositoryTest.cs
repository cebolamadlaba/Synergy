using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// User repository tests
    /// </summary>
    public class UserRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new User
            {
                ANumber = "ef6cbdf438",
                EmailAddress = "416dcb07b0",
                FirstName = "0ac3ff0687",
                Surname = "42f59d39d7",
                IsActive = false
            };

            var result = InstantiatedDependencies.UserRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.UserRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.UserRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.UserRepository.ReadById(id);

            model.ANumber = "100d36692c";
            model.EmailAddress = "c477ac58db";
            model.FirstName = "3de2516a4c";
            model.Surname = "01e39c70bf";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.UserRepository.Update(model);

            var updatedModel = InstantiatedDependencies.UserRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ANumber, model.ANumber);
            Assert.Equal(updatedModel.EmailAddress, model.EmailAddress);
            Assert.Equal(updatedModel.FirstName, model.FirstName);
            Assert.Equal(updatedModel.Surname, model.Surname);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new User
            {
                ANumber = "ef6cbdf438",
                EmailAddress = "416dcb07b0",
                FirstName = "0ac3ff0687",
                Surname = "42f59d39d7",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.UserRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.UserRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.UserRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
