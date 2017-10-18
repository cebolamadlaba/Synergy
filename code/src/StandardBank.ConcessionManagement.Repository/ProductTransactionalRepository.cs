using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ProductTransactional repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IProductTransactionalRepository" />
    public class ProductTransactionalRepository : IProductTransactionalRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTransactionalRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ProductTransactionalRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ProductTransactional Create(ProductTransactional model)
        {
            const string sql =
                @"INSERT [dbo].[tblProductTransactional] ([fkRiskGroupId], [fkLegalEntityId], [fkLegalEntityAccountId], [fkTransactionTableNumberId], [fkTransactionTypeId], [Volume], [Value], [LoadedPrice]) 
                VALUES (@RiskGroupId, @LegalEntityId, @LegalEntityAccountId, @TransactionTableNumberId, @TransactionTypeId, @Volume, @Value, @LoadedPrice) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TransactionTableNumberId = model.TransactionTableNumberId,
                        TransactionTypeId = model.TransactionTypeId,
                        Volume = model.Volume,
                        Value = model.Value,
                        LoadedPrice = model.LoadedPrice
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ProductTransactional ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductTransactional>(
                    "SELECT [pkProductTransactionalId] [Id], [fkRiskGroupId] [RiskGroupId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTransactionTableNumberId] [TransactionTableNumberId], [fkTransactionTypeId] [TransactionTypeId], [Volume], [Value], [LoadedPrice] FROM [dbo].[tblProductTransactional] WHERE [pkProductTransactionalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the risk group id
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        public IEnumerable<ProductTransactional> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductTransactional>(
                    @"SELECT [pkProductTransactionalId] [Id], [fkRiskGroupId] [RiskGroupId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTransactionTableNumberId] [TransactionTableNumberId], [fkTransactionTypeId] [TransactionTypeId], [Volume], [Value], [LoadedPrice] 
                    FROM [dbo].[tblProductTransactional] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductTransactional> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductTransactional>(
                    "SELECT [pkProductTransactionalId] [Id], [fkRiskGroupId] [RiskGroupId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTransactionTableNumberId] [TransactionTableNumberId], [fkTransactionTypeId] [TransactionTypeId], [Volume], [Value], [LoadedPrice] FROM [dbo].[tblProductTransactional]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ProductTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblProductTransactional]
                            SET [fkRiskGroupId] = @RiskGroupId, [fkLegalEntityId] = @LegalEntityId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [fkTransactionTableNumberId] = @TransactionTableNumberId, [fkTransactionTypeId] = @TransactionTypeId, [Volume] = @Volume, [Value] = @Value, [LoadedPrice] = @LoadedPrice
                            WHERE [pkProductTransactionalId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TransactionTableNumberId = model.TransactionTableNumberId,
                        TransactionTypeId = model.TransactionTypeId,
                        Volume = model.Volume,
                        Value = model.Value,
                        LoadedPrice = model.LoadedPrice
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ProductTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblProductTransactional] WHERE [pkProductTransactionalId] = @Id",
                    new {model.Id});
            }
        }
    }
}
