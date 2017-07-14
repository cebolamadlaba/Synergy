using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ReferenceType repository tests
    /// </summary>
    public class ReferenceTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ReferenceType
            {
                Description = "4baf50aa23",
                IsActive = false
            };

            var result = InstantiatedDependencies.ReferenceTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ReferenceTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ReferenceTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ReferenceTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ReferenceTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ReferenceTypeRepository.ReadById(id);

            model.Description = "25c289d0b1";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ReferenceTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ReferenceTypeRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.Description, model.Description);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ReferenceType
            {
                Description = "4baf50aa23",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ReferenceTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ReferenceTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ReferenceTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
