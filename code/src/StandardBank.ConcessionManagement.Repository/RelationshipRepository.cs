using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Relationship repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IRelationshipRepository" />
    public class RelationshipRepository : IRelationshipRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelationshipRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public RelationshipRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Relationship Create(Relationship model)
        {
            const string sql = @"INSERT [dbo].[rtblRelationship] ([Description], [IsActive]) 
                                VALUES (@Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, IsActive = model.IsActive})
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.RelationshipRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Relationship ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Relationship> ReadAll()
        {
            Func<IEnumerable<Relationship>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<Relationship>(
                        @"SELECT [pkRelationshipId] [Id], 
                            [Description], 
                            [IsActive] 
                        FROM [dbo].[rtblRelationship]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.RelationshipRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Relationship model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[rtblRelationship]
                    SET [Description] = @Description, 
                        [IsActive] = @IsActive
                    WHERE [pkRelationshipId] = @Id",
                    new {Id = model.Id, Description = model.Description, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.RelationshipRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Relationship model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[rtblRelationship] 
                            WHERE [pkRelationshipId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.RelationshipRepository.ReadAll);
        }
    }
}
