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
    /// Period repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IPeriodRepository" />
    public class PeriodRepository : IPeriodRepository
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
        /// Initializes a new instance of the <see cref="PeriodRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public PeriodRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Period Create(Period model)
        {
            const string sql = @"INSERT [dbo].[rtblPeriod] ([Description], [IsActive]) 
                                VALUES (@Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, IsActive = model.IsActive})
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.PeriodRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Period ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Period> ReadAll()
        {
            Func<IEnumerable<Period>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<Period>(
                        @"SELECT [pkPeriodId] [Id], 
                            [Description], 
                            [IsActive] 
                        FROM [dbo].[rtblPeriod]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.PeriodRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Period model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[rtblPeriod]
                    SET [Description] = @Description, 
                        [IsActive] = @IsActive
                    WHERE [pkPeriodId] = @Id",
                    new {Id = model.Id, Description = model.Description, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.PeriodRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Period model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[rtblPeriod] 
                            WHERE [pkPeriodId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.PeriodRepository.ReadAll);
        }
    }
}