using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ProductLending repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IProductLendingRepository" />
    public class ProductLendingRepository : IProductLendingRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductLendingRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ProductLendingRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ProductLending Create(ProductLending model)
        {
            const string sql =
                @"INSERT [dbo].[tblProductLending] ([fkRiskGroupId], [fkLegalEntityId], [fkLegalEntityAccountId], [fkProductId], [Limit], [AverageBalance], [LoadedMap]) 
                                VALUES (@RiskGroupId, @LegalEntityId, @LegalEntityAccountId, @ProductId, @Limit, @AverageBalance, @LoadedMap) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        ProductId = model.ProductId,
                        Limit = model.Limit,
                        AverageBalance = model.AverageBalance,
                        LoadedMap = model.LoadedMap
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ProductLending ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductLending>(
                    "SELECT [pkProductLendingId] [Id], [fkRiskGroupId] [RiskGroupId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkProductId] [ProductId], [Limit], [AverageBalance], [LoadedMap] FROM [dbo].[tblProductLending] WHERE [pkProductLendingId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by risk group identifier.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <returns></returns>
        public IEnumerable<ProductLending> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductLending>(
                    @"SELECT [pkProductLendingId] [Id], [fkRiskGroupId] [RiskGroupId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkProductId] [ProductId], [Limit], [AverageBalance], [LoadedMap] 
                    FROM [dbo].[tblProductLending] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductLending> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductLending>(
                    "SELECT [pkProductLendingId] [Id], [fkRiskGroupId] [RiskGroupId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkProductId] [ProductId], [Limit], [AverageBalance], [LoadedMap] FROM [dbo].[tblProductLending]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ProductLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblProductLending]
                            SET [fkRiskGroupId] = @RiskGroupId, [fkLegalEntityId] = @LegalEntityId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [fkProductId] = @ProductId, [Limit] = @Limit, [AverageBalance] = @AverageBalance, [LoadedMap] = @LoadedMap
                            WHERE [pkProductLendingId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        ProductId = model.ProductId,
                        Limit = model.Limit,
                        AverageBalance = model.AverageBalance,
                        LoadedMap = model.LoadedMap
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ProductLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblProductLending] WHERE [pkProductLendingId] = @Id",
                    new {model.Id});
            }
        }
    }
}