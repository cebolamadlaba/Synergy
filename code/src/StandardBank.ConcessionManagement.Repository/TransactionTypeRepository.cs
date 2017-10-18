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
    /// TransactionType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ITransactionTypeRepository" />
    public class TransactionTypeRepository : ITransactionTypeRepository
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
        /// Initializes a new instance of the <see cref="TransactionTypeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public TransactionTypeRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionType Create(TransactionType model)
        {
            const string sql = @"INSERT [dbo].[rtblTransactionType] ([fkConcessionTypeId], [Description], [IsActive]) 
                                VALUES (@ConcessionTypeId, @Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionTypeId = model.ConcessionTypeId,
                        Description = model.Description,
                        IsActive = model.IsActive
                    }).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TransactionType ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads the by concession type identifier is active.
        /// </summary>
        /// <param name="concessionTypeId">The concession type identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<TransactionType> ReadByConcessionTypeIdIsActive(int concessionTypeId, bool isActive)
        {
            return ReadAll().Where(_ => _.ConcessionTypeId.HasValue && _.ConcessionTypeId.Value == concessionTypeId &&
                                        _.IsActive == isActive);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TransactionType> ReadAll()
        {
            Func<IEnumerable<TransactionType>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<TransactionType>(
                        "SELECT [pkTransactionTypeId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [Description], [IsActive] FROM [dbo].[rtblTransactionType]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.TransactionTypeRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(TransactionType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblTransactionType]
                            SET [fkConcessionTypeId] = @ConcessionTypeId, [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkTransactionTypeId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionTypeId = model.ConcessionTypeId,
                        Description = model.Description,
                        IsActive = model.IsActive
                    });
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(TransactionType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblTransactionType] WHERE [pkTransactionTypeId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeRepository.ReadAll);
        }
    }
}