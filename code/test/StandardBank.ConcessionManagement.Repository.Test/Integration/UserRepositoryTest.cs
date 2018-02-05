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
                ANumber = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10),
                EmailAddress = "416dcb07b0",
                FirstName = "0ac3ff0687",
                Surname = "42f59d39d7",
                IsActive = false,
                ContactNumber = "011 555 1234",
                CanApprove = true
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
        /// Tests that ReadByANumber executes positive
        /// </summary>
        [Fact]
        public void ReadByANumber_Executes_Positive()
        {
            var results = InstantiatedDependencies.UserRepository.ReadAll();
            var aNumber = results.First().ANumber;
            var result = InstantiatedDependencies.UserRepository.ReadByANumber(aNumber);

            Assert.NotNull(result);
            Assert.Equal(result.ANumber, aNumber);
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

            model.ANumber = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            model.EmailAddress = "c477ac58db";
            model.FirstName = "3de2516a4c";
            model.Surname = "01e39c70bf";
            model.IsActive = !model.IsActive;
            model.ContactNumber = "012123123";
            model.CanApprove = !model.CanApprove;

            InstantiatedDependencies.UserRepository.Update(model);

            var updatedModel = InstantiatedDependencies.UserRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ANumber, model.ANumber);
            Assert.Equal(updatedModel.EmailAddress, model.EmailAddress);
            Assert.Equal(updatedModel.FirstName, model.FirstName);
            Assert.Equal(updatedModel.Surname, model.Surname);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
            Assert.Equal(updatedModel.ContactNumber, model.ContactNumber);
            Assert.Equal(updatedModel.CanApprove, model.CanApprove);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var aNumber = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);

            var model = new User
            {
                ANumber = aNumber,
                EmailAddress = "416dcb07b0",
                FirstName = "0ac3ff0687",
                Surname = "42f59d39d7",
                IsActive = false,
                ContactNumber = "011 555 1234",
                CanApprove = true
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
