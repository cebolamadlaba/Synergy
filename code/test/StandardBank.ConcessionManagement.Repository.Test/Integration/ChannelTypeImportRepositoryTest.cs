using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ChannelTypeImport repository tests
    /// </summary>
    public class ChannelTypeImportRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ChannelTypeImport
            {
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                ImportFileChannel = "5fc6cca923"
            };

            var result = InstantiatedDependencies.ChannelTypeImportRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ChannelTypeImportRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ChannelTypeImportRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ChannelTypeImportRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ChannelTypeImportRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ChannelTypeImportRepository.ReadById(id);

            model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            model.ImportFileChannel = "c424ba4a50";

            InstantiatedDependencies.ChannelTypeImportRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ChannelTypeImportRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            Assert.Equal(updatedModel.ImportFileChannel, model.ImportFileChannel);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ChannelTypeImport
            {
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                ImportFileChannel = "5fc6cca923"
            };

            var temporaryEntity = InstantiatedDependencies.ChannelTypeImportRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ChannelTypeImportRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ChannelTypeImportRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
