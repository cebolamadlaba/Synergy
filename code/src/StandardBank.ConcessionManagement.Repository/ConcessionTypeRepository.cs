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
    /// ConcessionType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionTypeRepository" />
    public class ConcessionTypeRepository : IConcessionTypeRepository
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
        /// Initializes a new instance of the <see cref="ConcessionTypeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ConcessionTypeRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionType Create(ConcessionType model)
        {
            const string sql = @"INSERT [dbo].[rtblConcessionType] ([Description], [Code], [IsActive]) 
                                VALUES (@Description, @Code, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new {Description = model.Description, Code = model.Code, IsActive = model.IsActive}).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ConcessionTypeRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionType ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<ConcessionType> ReadAll(bool isActive)
        {
            return ReadAll().Where(_ => _.IsActive == isActive);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionType> ReadAll()
        {
            Func<IEnumerable<ConcessionType>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<ConcessionType>(
                        "SELECT [pkConcessionTypeId] [Id], [Description], [Code], [IsActive] FROM [dbo].[rtblConcessionType]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.ConcessionTypeRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblConcessionType]
                            SET [Description] = @Description, [Code] = @Code, [IsActive] = @IsActive
                            WHERE [pkConcessionTypeId] = @Id",
                    new {Id = model.Id, Description = model.Description, Code = model.Code, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ConcessionTypeRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblConcessionType] WHERE [pkConcessionTypeId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ConcessionTypeRepository.ReadAll);
        }
    }
}