using System;
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
                RelationshipId = DataHelper.GetRelationshipId(),
                CreationDate = DateTime.Now,
                UserId = DataHelper.GetUserId()
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
        /// Tests that ReadByChildConcessionId executes positive.
        /// </summary>
        [Fact]
        public void ReadByChildConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();
            var childConcessionId = results.First().ChildConcessionId;
            var result = InstantiatedDependencies.ConcessionRelationshipRepository.ReadByChildConcessionId(childConcessionId);

            Assert.NotNull(result);

            foreach (var record in result)
                Assert.Equal(record.ChildConcessionId, childConcessionId);
        }

        /// <summary>
        /// Tests that ReadByParentConcessionId executes positive.
        /// </summary>
        [Fact]
        public void ReadByParentConcessionId_Executes_Positive()
        {
            var results = InstantiatedDependencies.ConcessionRelationshipRepository.ReadAll();
            var parentConcessionId = results.First().ParentConcessionId;
            var result = InstantiatedDependencies.ConcessionRelationshipRepository.ReadByParentConcessionId(parentConcessionId);

            Assert.NotNull(result);

            foreach (var record in result)
                Assert.Equal(record.ParentConcessionId, parentConcessionId);
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
        /// Tests that DoesChildHaveThreeParentRelationships returns true.
        /// </summary>
        [Fact]
        public void DoesChildHaveThreeParentRelationships_Returns_True()
        {
            var firstConcessionId = DataHelper.InsertConcession();
            var secondConcessionId = DataHelper.InsertConcession();
            var thirdConcessionId = DataHelper.InsertConcession();
            var fourthConcessionId = DataHelper.InsertConcession();

            var relationshipId = DataHelper.GetRelationshipId();
            var userId = DataHelper.GetUserId();

            var firstModel = new ConcessionRelationship
            {
                ParentConcessionId = fourthConcessionId,
                ChildConcessionId = thirdConcessionId,
                RelationshipId = relationshipId,
                CreationDate = DateTime.Now,
                UserId = userId
            };

            var firstResult = InstantiatedDependencies.ConcessionRelationshipRepository.Create(firstModel);

            Assert.NotNull(firstResult);

            var secondModel = new ConcessionRelationship
            {
                ParentConcessionId = thirdConcessionId,
                ChildConcessionId = secondConcessionId,
                RelationshipId = relationshipId,
                CreationDate = DateTime.Now,
                UserId = userId
            };

            var secondResult = InstantiatedDependencies.ConcessionRelationshipRepository.Create(secondModel);

            Assert.NotNull(secondResult);

            var thirdModel = new ConcessionRelationship
            {
                ParentConcessionId = secondConcessionId,
                ChildConcessionId = firstConcessionId,
                RelationshipId = relationshipId,
                CreationDate = DateTime.Now,
                UserId = userId
            };

            var thirdResult = InstantiatedDependencies.ConcessionRelationshipRepository.Create(thirdModel);

            Assert.NotNull(thirdResult);

            var result =
                InstantiatedDependencies.ConcessionRelationshipRepository.DoesChildHaveThreeParentRelationships(
                    firstConcessionId, relationshipId);

            Assert.True(result);
        }

        /// <summary>
        /// Tests that DoesChildHaveThreeParentRelationships returns false.
        /// </summary>
        [Fact]
        public void DoesChildHaveThreeParentRelationships_Returns_False()
        {
            var firstConcessionId = DataHelper.InsertConcession();
            var secondConcessionId = DataHelper.InsertConcession();
            var thirdConcessionId = DataHelper.InsertConcession();

            var relationshipId = DataHelper.GetRelationshipId();
            var userId = DataHelper.GetUserId();

            var firstModel = new ConcessionRelationship
            {
                ParentConcessionId = thirdConcessionId,
                ChildConcessionId = secondConcessionId,
                RelationshipId = relationshipId,
                CreationDate = DateTime.Now,
                UserId = userId
            };

            var firstResult = InstantiatedDependencies.ConcessionRelationshipRepository.Create(firstModel);

            Assert.NotNull(firstResult);

            var secondModel = new ConcessionRelationship
            {
                ParentConcessionId = secondConcessionId,
                ChildConcessionId = firstConcessionId,
                RelationshipId = relationshipId,
                CreationDate = DateTime.Now,
                UserId = userId
            };

            var secondResult = InstantiatedDependencies.ConcessionRelationshipRepository.Create(secondModel);

            Assert.NotNull(secondResult);

            var result =
                InstantiatedDependencies.ConcessionRelationshipRepository.DoesChildHaveThreeParentRelationships(
                    firstConcessionId, relationshipId);

            Assert.False(result);
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
            model.CreationDate = DataHelper.ChangeDate(model.CreationDate);
            model.UserId = DataHelper.GetAlternateUserId(model.UserId);

            InstantiatedDependencies.ConcessionRelationshipRepository.Update(model);

            var updatedModel = InstantiatedDependencies.ConcessionRelationshipRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.ParentConcessionId, model.ParentConcessionId);
            Assert.Equal(updatedModel.ChildConcessionId, model.ChildConcessionId);
            Assert.Equal(updatedModel.RelationshipId, model.RelationshipId);
            Assert.Equal(updatedModel.CreationDate, model.CreationDate);
            Assert.Equal(updatedModel.UserId, model.UserId);
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
                RelationshipId = DataHelper.GetRelationshipId(),
                CreationDate = DateTime.Now,
                UserId = DataHelper.GetUserId()
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
