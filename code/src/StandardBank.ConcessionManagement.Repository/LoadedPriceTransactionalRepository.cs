using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// LoadedPriceTransactional repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ILoadedPriceTransactionalRepository" />
    public class LoadedPriceTransactionalRepository : ILoadedPriceTransactionalRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedPriceTransactionalRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public LoadedPriceTransactionalRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public LoadedPriceTransactional Create(LoadedPriceTransactional model)
        {
            const string sql =
                @"INSERT [dbo].[tblLoadedPriceTransactional] ([fkTransactionTypeId], [fkLegalEntityAccountId], [fkTransactionTableNumberId]) 
                                VALUES (@TransactionTypeId, @LegalEntityAccountId, @TransactionTableNumberId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        TransactionTypeId = model.TransactionTypeId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TransactionTableNumberId = model.TransactionTableNumberId
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LoadedPriceTransactional ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceTransactional>(
                    "SELECT [pkLoadedPriceTransactionalId] [Id], [fkTransactionTypeId] [TransactionTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTransactionTableNumberId] [TransactionTableNumberId] FROM [dbo].[tblLoadedPriceTransactional] WHERE [pkLoadedPriceTransactionalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the transaction type id and the legal entity account id
        /// </summary>
        /// <param name="transactionTypeId"></param>
        /// <param name="legalEntityAccountId"></param>
        /// <returns></returns>
        public LoadedPriceTransactional ReadByTransactionTypeIdLegalEntityAccountId(int transactionTypeId, int legalEntityAccountId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceTransactional>(
                    @"SELECT [pkLoadedPriceTransactionalId] [Id], [fkTransactionTypeId] [TransactionTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTransactionTableNumberId] [TransactionTableNumberId] 
                    FROM [dbo].[tblLoadedPriceTransactional] 
                    WHERE [fkTransactionTypeId] = @transactionTypeId
                    AND [fkLegalEntityAccountId] = @legalEntityAccountId",
                    new { transactionTypeId, legalEntityAccountId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LoadedPriceTransactional> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceTransactional>(
                    "SELECT [pkLoadedPriceTransactionalId] [Id], [fkTransactionTypeId] [TransactionTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTransactionTableNumberId] [TransactionTableNumberId] FROM [dbo].[tblLoadedPriceTransactional]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(LoadedPriceTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblLoadedPriceTransactional]
                            SET [fkTransactionTypeId] = @TransactionTypeId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [fkTransactionTableNumberId] = @TransactionTableNumberId
                            WHERE [pkLoadedPriceTransactionalId] = @Id",
                    new
                    {
                        Id = model.Id,
                        TransactionTypeId = model.TransactionTypeId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TransactionTableNumberId = model.TransactionTableNumberId
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(LoadedPriceTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblLoadedPriceTransactional] WHERE [pkLoadedPriceTransactionalId] = @Id",
                    new {model.Id});
            }
        }
    }
}
