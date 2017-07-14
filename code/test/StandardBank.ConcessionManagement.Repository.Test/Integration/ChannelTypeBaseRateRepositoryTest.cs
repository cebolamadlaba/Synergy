using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ChannelTypeBaseRate repository tests
    /// </summary>
    public class ChannelTypeBaseRateRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ChannelTypeBaseRate
            {
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                BaseRateId = DataHelper.GetBaseRateId()
            };

            var result = InstantiatedDependencies.ChannelTypeBaseRateRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadById(id);

            model.ChannelTypeId = DataHelper.GetAlternateChannelTypeId(model.ChannelTypeId);
            model.BaseRateId = DataHelper.GetAlternateBaseRateId(model.BaseRateId);

            InstantiatedDependencies.ChannelTypeBaseRateRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ChannelTypeId, model.ChannelTypeId);
            Assert.Equal(updatedModel.BaseRateId, model.BaseRateId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ChannelTypeBaseRate
            {
                ChannelTypeId = DataHelper.GetChannelTypeId(),
                BaseRateId = DataHelper.GetBaseRateId()
            };

            var temporaryEntity = InstantiatedDependencies.ChannelTypeBaseRateRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ChannelTypeBaseRateRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ChannelTypeBaseRateRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
