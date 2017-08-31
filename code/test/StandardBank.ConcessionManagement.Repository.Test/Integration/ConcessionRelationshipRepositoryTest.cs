using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// ConcessionRelationship repository tests
    /// </summary>
    public class ConcessionRelationshipRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var concessionId = DataHelper.GetConcessionId();

            var model = new ConcessionRelationship
            {
                ParentConcessionId = concessionId,
                ChildConcessionId = DataHelper.GetAlternateConcessionId(concessionId),
                RelationshipId = DataHelper.GetRelationshipId()
            };

            var result = InstantiatedDependencies.ConcessionRelationshipRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.ConcessionRelationshipRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.ConcessionRelationshipRepository.ReadById(id);

            model.ParentConcessionId = DataHelper.GetAlternateConcessionId(model.ParentConcessionId);
            model.ChildConcessionId = DataHelper.GetAlternateConcessionId(model.ChildConcessionId);
            model.RelationshipId = DataHelper.GetAlternateRelationshipId(model.RelationshipId);

            InstantiatedDependencies.ConcessionRelationshipRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionRelationshipRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ParentConcessionId, model.ParentConcessionId);
            Assert.Equal(updatedModel.ChildConcessionId, model.ChildConcessionId);
            Assert.Equal(updatedModel.RelationshipId, model.RelationshipId);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var concessionId = DataHelper.GetConcessionId();

            var model = new ConcessionRelationship
            {
                ParentConcessionId = concessionId,
                ChildConcessionId = DataHelper.GetAlternateConcessionId(concessionId),
                RelationshipId = DataHelper.GetRelationshipId()
            };

            var temporaryEntity = InstantiatedDependencies.ConcessionRelationshipRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.ConcessionRelationshipRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.ConcessionRelationshipRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
