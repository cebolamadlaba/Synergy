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
    /// PeriodType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IPeriodTypeRepository" />
    public class PeriodTypeRepository : IPeriodTypeRepository
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
        /// Initializes a new instance of the <see cref="PeriodTypeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public PeriodTypeRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public PeriodType Create(PeriodType model)
        {
            const string sql = @"INSERT [dbo].[rtblPeriodType] ([Description], [IsActive]) 
                                VALUES (@Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, IsActive = model.IsActive})
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.PeriodTypeRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PeriodType ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PeriodType> ReadAll()
        {
            Func<IEnumerable<PeriodType>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<PeriodType>(
                        @"SELECT [pkPeriodTypeId] [Id], 
                            [Description], 
                            [IsActive] 
                        FROM [dbo].[rtblPeriodType]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.PeriodTypeRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(PeriodType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[rtblPeriodType]
                    SET [Description] = @Description, 
                        [IsActive] = @IsActive
                    WHERE [pkPeriodTypeId] = @Id",
                    new {Id = model.Id, Description = model.Description, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.PeriodTypeRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(PeriodType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[rtblPeriodType] 
                            WHERE [pkPeriodTypeId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.PeriodTypeRepository.ReadAll);
        }
    }
}