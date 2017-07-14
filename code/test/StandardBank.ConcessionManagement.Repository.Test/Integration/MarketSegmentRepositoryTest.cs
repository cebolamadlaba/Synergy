using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// MarketSegment repository tests
    /// </summary>
    public class MarketSegmentRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new MarketSegment
            {
                Description = "30836d1c63",
                IsActive = false
            };

            var result = InstantiatedDependencies.MarketSegmentRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.MarketSegmentRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.MarketSegmentRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.MarketSegmentRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.MarketSegmentRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.MarketSegmentRepository.ReadById(id);

            model.Description = "38fe36a13d";
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.MarketSegmentRepository.Update(model);

            var updatedModel = InstantiatedDependencies.MarketSegmentRepository.ReadById(id);

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
            var model = new MarketSegment
            {
                Description = "30836d1c63",
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.MarketSegmentRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.MarketSegmentRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.MarketSegmentRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
