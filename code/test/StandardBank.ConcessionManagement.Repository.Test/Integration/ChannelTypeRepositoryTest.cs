using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ChannelType repository tests
    /// </summary>
    public class ChannelTypeRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ChannelType
            {
                Description = "ba0956864b",
                IsActive = false
            };

            var result = InstantiatedDependencies.ChannelTypeRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ChannelTypeRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ChannelTypeRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ChannelTypeRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ChannelTypeRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ChannelTypeRepository.ReadById(id);

            model.Description = "bacb28e04c";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.ChannelTypeRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ChannelTypeRepository.ReadById(id);

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
            var model = new ChannelType
            {
                Description = "ba0956864b",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.ChannelTypeRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ChannelTypeRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ChannelTypeRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
