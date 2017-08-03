using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// AdValorem repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IAdValoremRepository" />
    public class AdValoremRepository : IAdValoremRepository
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
        /// Initializes a new instance of the <see cref="AdValoremRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public AdValoremRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AdValorem Create(AdValorem model)
        {
            const string sql = @"INSERT [dbo].[rtblAdValorem] ([AdValorem], [IsActive]) 
                                VALUES (@AdValorem, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {AdValorem = model.Amount, IsActive = model.IsActive}).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.AdValoremRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AdValorem ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AdValorem> ReadAll()
        {
            Func<IEnumerable<AdValorem>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
            	{
                	return db.Query<AdValorem>("SELECT [pkAdValoremId] [Id], [AdValorem] [Amount], [IsActive] FROM [dbo].[rtblAdValorem]");
            	}
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.AdValoremRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(AdValorem model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblAdValorem]
                            SET [AdValorem] = @AdValorem, [IsActive] = @IsActive
                            WHERE [pkAdValoremId] = @Id",
                    new {Id = model.Id, AdValorem = model.Amount, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.AdValoremRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(AdValorem model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblAdValorem] WHERE [pkAdValoremId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.AdValoremRepository.ReadAll);
        }
    }
}
