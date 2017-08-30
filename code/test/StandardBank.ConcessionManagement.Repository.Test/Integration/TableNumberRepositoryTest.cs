using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// TableNumber repository tests
    /// </summary>
    public class TableNumberRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new TableNumber
            {
                TariffTable = 8,
                AdValorem = 7914,
                BaseRate = 1397,
                IsActive = true
            };

            var result = InstantiatedDependencies.TableNumberRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.TableNumberRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.TableNumberRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.TableNumberRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.TableNumberRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.TableNumberRepository.ReadById(id);

            model.TariffTable = model.TariffTable + 1;
            model.AdValorem = model.AdValorem + 100;
            model.BaseRate = model.BaseRate + 100;
            model.IsActive = !model.IsActive;

            InstantiatedDependencies.TableNumberRepository.Update(model);

            var updatedModel = InstantiatedDependencies.TableNumberRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.TariffTable, model.TariffTable);
            Assert.Equal(updatedModel.AdValorem, model.AdValorem);
            Assert.Equal(updatedModel.BaseRate, model.BaseRate);
            Assert.Equal(updatedModel.IsActive, model.IsActive);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new TableNumber
            {
                TariffTable = 8,
                AdValorem = 7914,
                BaseRate = 1397,
                IsActive = false
            };

            var temporaryEntity = InstantiatedDependencies.TableNumberRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.TableNumberRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.TableNumberRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
