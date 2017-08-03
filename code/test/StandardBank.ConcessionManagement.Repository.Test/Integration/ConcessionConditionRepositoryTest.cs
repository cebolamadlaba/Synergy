using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionCondition repository tests
    /// </summary>
    public class ConcessionConditionRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new ConcessionCondition
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConditionTypeId = DataHelper.GetConditionTypeId(),
                ConditionProductId = DataHelper.GetConditionProductId(),
                InterestRate = 6563,
                Volume = 3,
                Value = 3092,
                IsActive = false,
                PeriodTypeId = DataHelper.GetPeriodTypeId(),
                PeriodId =  DataHelper.GetPeriodId()
            };

            var result = InstantiatedDependencies.ConcessionConditionRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionConditionRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionConditionRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByConcessionId executes positive
        /// </summary>
        [Fact]
        public void ReadByConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionConditionRepository.ReadAll();
            var concessionId = results.First().ConcessionId;
            var result = InstantiatedDependencies.ConcessionConditionRepository.ReadByConcessionId(concessionId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.ConcessionId, concessionId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionConditionRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionConditionRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionConditionRepository.ReadById(id);

            model.ConcessionId = DataHelper.GetAlternateConcessionId(model.ConcessionId);
            model.ConditionTypeId = DataHelper.GetAlternateConditionTypeId(model.ConditionTypeId);
            model.ConditionProductId = DataHelper.GetAlternateConditionProductId(model.ConditionProductId);
            model.InterestRate = model.InterestRate + 100;
            model.Volume = model.Volume + 1;
            model.Value = model.Value + 100;
            model.IsActive = !model.IsActive;
            model.PeriodTypeId = DataHelper.GetAlternatePeriodTypeId(model.PeriodTypeId);
            model.PeriodId = DataHelper.GetAlternatePeriodId(model.PeriodId);

            InstantiatedDependencies.ConcessionConditionRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionConditionRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ConcessionId, model.ConcessionId);
            Assert.Equal(updatedModel.ConditionTypeId, model.ConditionTypeId);
            Assert.Equal(updatedModel.ConditionProductId, model.ConditionProductId);
            Assert.Equal(updatedModel.InterestRate, model.InterestRate);
            Assert.Equal(updatedModel.Volume, model.Volume);
            Assert.Equal(updatedModel.Value, model.Value);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
            Assert.Equal(updatedModel.PeriodTypeId, model.PeriodTypeId);
            Assert.Equal(updatedModel.PeriodId, model.PeriodId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new ConcessionCondition
            {
                ConcessionId = DataHelper.GetConcessionId(),
                ConditionTypeId = DataHelper.GetConditionTypeId(),
                ConditionProductId = DataHelper.GetConditionProductId(),
                InterestRate = 6563,
                Volume = 3,
                Value = 3092,
                IsActive = false,
                PeriodTypeId = DataHelper.GetPeriodTypeId(),
                PeriodId = DataHelper.GetPeriodId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionConditionRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionConditionRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionConditionRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
