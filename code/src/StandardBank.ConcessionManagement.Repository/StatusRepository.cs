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
    /// Status repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IStatusRepository" />
    public class StatusRepository : IStatusRepository
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
        /// Initializes a new instance of the <see cref="StatusRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public StatusRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Status Create(Status model)
        {
            const string sql = @"INSERT [dbo].[rtblStatus] ([Description], [IsActive]) 
                                VALUES (@Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, IsActive = model.IsActive})
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.StatusRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Status ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Status> ReadAll()
        {
            Func<IEnumerable<Status>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<Status>(
                        "SELECT [pkStatusId] [Id], [Description], [IsActive] FROM [dbo].[rtblStatus]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.StatusRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Status model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblStatus]
                            SET [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkStatusId] = @Id",
                    new {Id = model.Id, Description = model.Description, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.StatusRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Status model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblStatus] WHERE [pkStatusId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.StatusRepository.ReadAll);
        }
    }
}