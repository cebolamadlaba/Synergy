using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionRelationship repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionRelationshipRepository" />
    public class ConcessionRelationshipRepository : IConcessionRelationshipRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionRelationshipRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionRelationshipRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionRelationship Create(ConcessionRelationship model)
        {
            const string sql =
                @"INSERT [dbo].[tblConcessionRelationship] ([fkParentConcessionId], [fkChildConcessionId], [fkRelationshipId], [CreationDate], [fkUserId]) 
                                VALUES (@ParentConcessionId, @ChildConcessionId, @RelationshipId, @CreationDate, @UserId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ParentConcessionId = model.ParentConcessionId,
                        ChildConcessionId = model.ChildConcessionId,
                        RelationshipId = model.RelationshipId,
                        CreationDate = model.CreationDate,
                        UserId = model.UserId
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionRelationship ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationship>(
                    "SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship] WHERE [pkConcessionRelationshipId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by child concession identifier.
        /// </summary>
        /// <param name="childConcessionId">The child concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionRelationship> ReadByChildConcessionId(int childConcessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationship>(
                    @"SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] 
                    FROM [dbo].[tblConcessionRelationship] 
                    WHERE [fkChildConcessionId] = @childConcessionId",
                    new { childConcessionId });
            }
        }

        /// <summary>
        /// Reads the by parent concession identifier.
        /// </summary>
        /// <param name="parentConcessionId">The parent concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionRelationship> ReadByParentConcessionId(int parentConcessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationship>(
                    @"SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] 
                    FROM [dbo].[tblConcessionRelationship] 
                    WHERE [fkParentConcessionId] = @parentConcessionId",
                    new { parentConcessionId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionRelationship> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationship>(
                    "SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship]");
            }
        }

        /// <summary>
        /// Doeses the child have three parent relationships.
        /// </summary>
        /// <param name="childConcessionId">The child concession identifier.</param>
        /// <param name="relationshipId">The relationship identifier.</param>
        /// <returns></returns>
        public bool DoesChildHaveThreeParentRelationships(int childConcessionId, int relationshipId)
        {
            const string sql = @"SELECT [fkParentConcessionId] FROM [dbo].[tblConcessionRelationship]
                                WHERE [fkChildConcessionId] = (SELECT [fkParentConcessionId] FROM [dbo].[tblConcessionRelationship]
                                WHERE [fkChildConcessionId] = (SELECT [fkParentConcessionId] FROM [dbo].[tblConcessionRelationship]
                                WHERE [fkChildConcessionId] = @childConcessionId
                                AND [fkRelationshipId] = @relationshipId)
                                AND [fkRelationshipId] = @relationshipId)
                                AND [fkRelationshipId] = @relationshipId";

            using (var db = _dbConnectionFactory.Connection())
            {
                var result = db.ExecuteScalar<int?>(sql, new {childConcessionId, relationshipId});

                return result.HasValue;
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionRelationship model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionRelationship]
                            SET [fkParentConcessionId] = @ParentConcessionId, [fkChildConcessionId] = @ChildConcessionId, [fkRelationshipId] = @RelationshipId, [CreationDate] = @CreationDate, [fkUserId] = @UserId
                            WHERE [pkConcessionRelationshipId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ParentConcessionId = model.ParentConcessionId,
                        ChildConcessionId = model.ChildConcessionId,
                        RelationshipId = model.RelationshipId,
                        CreationDate = model.CreationDate,
                        UserId = model.UserId
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionRelationship model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionRelationship] WHERE [pkConcessionRelationshipId] = @Id",
                    new {model.Id});
            }
        }
    }
}
