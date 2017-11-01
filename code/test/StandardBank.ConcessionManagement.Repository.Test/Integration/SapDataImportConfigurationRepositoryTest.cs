using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// SapDataImportConfiguration repository tests
    /// </summary>
    public class SapDataImportConfigurationRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new SapDataImportConfiguration
            {
                FileImportLocation = "c6d4ee2b93",
                FileExportLocation = "33e45310d6",
                SupportEmailAddress = "7462f5630f"
            };

            var result = InstantiatedDependencies.SapDataImportConfigurationRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadById(id);

            model.FileImportLocation = "029675cf3f";
            model.FileExportLocation = "79afac0ec2";
            model.SupportEmailAddress = "af246ccad8";

            InstantiatedDependencies.SapDataImportConfigurationRepository.Update(model);

            var updatedModel = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.FileImportLocation, model.FileImportLocation);
            Assert.Equal(updatedModel.FileExportLocation, model.FileExportLocation);
            Assert.Equal(updatedModel.SupportEmailAddress, model.SupportEmailAddress);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new SapDataImportConfiguration
            {
                FileImportLocation = "c6d4ee2b93",
                FileExportLocation = "33e45310d6",
                SupportEmailAddress = "7462f5630f"
            };

            var temporaryEntity = InstantiatedDependencies.SapDataImportConfigurationRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.SapDataImportConfigurationRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.SapDataImportConfigurationRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
