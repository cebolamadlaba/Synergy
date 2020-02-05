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
    /// TransactionTypeImport repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ITransactionTypeImportRepository" />
    public class TransactionTypeImportRepository : ITransactionTypeImportRepository
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
        /// Initializes a new instance of the <see cref="TransactionTypeImportRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public TransactionTypeImportRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionTypeImport Create(TransactionTypeImport model)
        {
            const string sql = @"INSERT [dbo].[rtblTransactionTypeImport] ([fkTransactionTypeId], [ImportFileChannel]) 
                                VALUES (@TransactionTypeId, @ImportFileChannel) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {TransactionTypeId = model.TransactionTypeId, ImportFileChannel = model.ImportFileChannel}).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeImportRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TransactionTypeImport ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TransactionTypeImport> ReadAll()
        {
            Func<IEnumerable<TransactionTypeImport>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
            	{
                	return db.Query<TransactionTypeImport>(
                        @"SELECT [pkTransactionTypeImportId] [Id], 
                            [fkTransactionTypeId] [TransactionTypeId], 
                            [ImportFileChannel] 
                        FROM [dbo].[rtblTransactionTypeImport]");
            	}
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.TransactionTypeImportRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(TransactionTypeImport model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[rtblTransactionTypeImport]
                    SET [fkTransactionTypeId] = @TransactionTypeId, 
                        [ImportFileChannel] = @ImportFileChannel
                    WHERE [pkTransactionTypeImportId] = @Id",
                    new {Id = model.Id, TransactionTypeId = model.TransactionTypeId, ImportFileChannel = model.ImportFileChannel});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeImportRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(TransactionTypeImport model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[rtblTransactionTypeImport] 
                            WHERE [pkTransactionTypeImportId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeImportRepository.ReadAll);
        }
    }
}
