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
                    new { id }).SingleOrDefault();
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
        /// Reads the details by concession identifier.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionRelationshipDetail> ReadDetailsByConcessionId(int concessionId)
        {
            var concessionRelationshipDetails = new List<ConcessionRelationshipDetail>();

            concessionRelationshipDetails.AddRange(GetParentDetails(concessionId));
            concessionRelationshipDetails.AddRange(GetChildrenDetails(concessionId));

            return concessionRelationshipDetails.OrderBy(_ => _.ParentConcessionId);
        }

        /// <summary>
        /// Gets the parent details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionRelationshipDetail> GetParentDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationshipDetail>(
                    @"WITH children AS (
                    SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship]
                    WHERE [fkChildConcessionId] = @concessionId
                    UNION ALL
                    SELECT t.[pkConcessionRelationshipId] [Id], t.[fkParentConcessionId] [ParentConcessionId], t.[fkChildConcessionId] [ChildConcessionId], t.[fkRelationshipId] [RelationshipId], t.[CreationDate], t.[fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship] t
                    INNER JOIN children c ON c.[ParentConcessionId] = t.[fkChildConcessionId])
                    SELECT
	                    'Parent' [RelationshipType],
                        pc.[pkConcessionId] [ParentConcessionId],
	                    pc.[ConcessionRef] [ParentConcessionReference],
	                    pc.[ConcessionRef] + ' (' + spc.[Description] + ' - ' + sspc.[Description] + ')' [ParentConcession], 
	                    pc.[IsActive] [ParentIsActive],
	                    r.[Description] [Relationship],
                        cc.[pkConcessionId] [ChildConcessionId],
	                    cc.[ConcessionRef] [ChildConcessionReference],
	                    cc.[ConcessionRef] + ' (' + scc.[Description] + ' - ' + sscc.[Description] + ')'  [ChildConcession],
	                    cc.[IsActive] [ChildIsActive],
	                    c.[CreationDate] [Date],
	                    u.[FirstName] + ' ' + u.[Surname] [User]
                    FROM children c
                    JOIN [dbo].[rtblRelationship] r ON r.[pkRelationshipId] = c.[RelationshipId]
                    JOIN [dbo].[tblConcession] pc ON pc.[pkConcessionId] = c.[ParentConcessionId]
                    JOIN [dbo].[tblConcession] cc ON cc.[pkConcessionId] = c.[ChildConcessionId]
                    JOIN [dbo].[rtblStatus] spc ON spc.[pkStatusId] = pc.[fkStatusId]
                    JOIN [dbo].[rtblStatus] scc ON scc.[pkStatusId] = cc.[fkStatusId]
                    JOIN [dbo].[rtblSubStatus] sspc ON sspc.[pkSubStatusId] = pc.[fkSubStatusId]
                    JOIN [dbo].[rtblSubStatus] sscc ON sscc.[pkSubStatusId] = cc.[fkSubStatusId]
                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = c.[UserId]
                    ORDER BY c.[ParentConcessionId]",
                    new { concessionId });
            }
        }

        /// <summary>
        /// Gets the children details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        private IEnumerable<ConcessionRelationshipDetail> GetChildrenDetails(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationshipDetail>(
                    @"WITH parents AS (
                    SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship]
                    WHERE [fkParentConcessionId] = @concessionId
                    UNION ALL
                    SELECT t.[pkConcessionRelationshipId] [Id], t.[fkParentConcessionId] [ParentConcessionId], t.[fkChildConcessionId] [ChildConcessionId], t.[fkRelationshipId] [RelationshipId], t.[CreationDate], t.[fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship] t
                    INNER JOIN parents p ON p.[ChildConcessionId] = t.[fkParentConcessionId])
                    SELECT
	                    'Children' [RelationshipType],
                        pc.[pkConcessionId] [ParentConcessionId],
	                    pc.[ConcessionRef] [ParentConcessionReference],
	                    pc.[ConcessionRef] + ' (' + spc.[Description] + ' - ' + sspc.[Description] + ')' [ParentConcession], 
	                    pc.[IsActive] [ParentIsActive],
	                    r.[Description] [Relationship],
                        cc.[pkConcessionId] [ChildConcessionId],
	                    cc.[ConcessionRef] [ChildConcessionReference],
	                    cc.[ConcessionRef] + ' (' + scc.[Description] + ' - ' + sscc.[Description] + ')'  [ChildConcession],
	                    cc.[IsActive] [ChildIsActive],
	                    p.[CreationDate] [Date],
	                    u.[FirstName] + ' ' + u.[Surname] [User]
                    FROM parents p
                    JOIN [dbo].[rtblRelationship] r ON r.[pkRelationshipId] = p.[RelationshipId]
                    JOIN [dbo].[tblConcession] pc ON pc.[pkConcessionId] = p.[ParentConcessionId]
                    JOIN [dbo].[tblConcession] cc ON cc.[pkConcessionId] = p.[ChildConcessionId]
                    JOIN [dbo].[rtblStatus] spc ON spc.[pkStatusId] = pc.[fkStatusId]
                    JOIN [dbo].[rtblStatus] scc ON scc.[pkStatusId] = cc.[fkStatusId]
                    JOIN [dbo].[rtblSubStatus] sspc ON sspc.[pkSubStatusId] = pc.[fkSubStatusId]
                    JOIN [dbo].[rtblSubStatus] sscc ON sscc.[pkSubStatusId] = cc.[fkSubStatusId]
                    JOIN [dbo].[tblUser] u ON u.[pkUserId] = p.[UserId]
                    ORDER BY p.[ParentConcessionId]",
                    new {concessionId});
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
        /// Reads the by child concession identifier relationship identifier relationships.
        /// </summary>
        /// <param name="childConcessionId">The child concession identifier.</param>
        /// <param name="relationshipId">The relationship identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionRelationship> ReadByChildConcessionIdRelationshipIdRelationships(int childConcessionId, int relationshipId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionRelationship>(@"WITH children AS (
                    SELECT [pkConcessionRelationshipId] [Id], [fkParentConcessionId] [ParentConcessionId], [fkChildConcessionId] [ChildConcessionId], [fkRelationshipId] [RelationshipId], [CreationDate], [fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship]
                    WHERE [fkChildConcessionId] = @childConcessionId
                    UNION ALL
                    SELECT t.[pkConcessionRelationshipId] [Id], t.[fkParentConcessionId] [ParentConcessionId], t.[fkChildConcessionId] [ChildConcessionId], t.[fkRelationshipId] [RelationshipId], t.[CreationDate], t.[fkUserId] [UserId] FROM [dbo].[tblConcessionRelationship] t
                    INNER JOIN children c ON c.[ParentConcessionId] = t.[fkChildConcessionId])
                    SELECT * FROM children
                    WHERE [RelationshipId] = @relationshipId", new { childConcessionId, relationshipId });
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
                var result = db.ExecuteScalar<int?>(sql, new { childConcessionId, relationshipId });

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
                    new { model.Id });
            }
        }
    }
}
