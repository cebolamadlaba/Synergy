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
    /// ReferenceType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IReferenceTypeRepository" />
    public class ReferenceTypeRepository : IReferenceTypeRepository
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
        /// Initializes a new instance of the <see cref="ReferenceTypeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ReferenceTypeRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ReferenceType Create(ReferenceType model)
        {
            const string sql = @"INSERT [dbo].[rtblType] ([Description], [IsActive]) 
                                VALUES (@Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, IsActive = model.IsActive})
                    .Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ReferenceTypeRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ReferenceType ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReferenceType> ReadAll()
        {
            Func<IEnumerable<ReferenceType>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<ReferenceType>(
                        "SELECT [pkTypeId] [Id], [Description], [IsActive] FROM [dbo].[rtblType]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.ReferenceTypeRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ReferenceType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblType]
                            SET [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkTypeId] = @Id",
                    new {Id = model.Id, Description = model.Description, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ReferenceTypeRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ReferenceType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblType] WHERE [pkTypeId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ReferenceTypeRepository.ReadAll);
        }
    }
}