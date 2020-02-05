using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ProductInvestment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IProductInvestmentRepository" />
    public class ProductInvestmentRepository : IProductInvestmentRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInvestmentRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ProductInvestmentRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ProductInvestment Create(ProductInvestment model)
        {
            const string sql =
                @"INSERT [dbo].[tblProductInvestment] ([fkRiskGroupId], [fkLegalEntityId], [fkLegalEntityAccountId], [fkProductId], [AverageBalance], [LoadedCustomerRate]) 
                VALUES (@RiskGroupId, @LegalEntityId, @LegalEntityAccountId, @ProductId, @AverageBalance, @LoadedCustomerRate) 
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
                        AverageBalance = model.AverageBalance,
                        LoadedCustomerRate = model.LoadedCustomerRate
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ProductInvestment ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductInvestment>(
                    @"SELECT [pkProductInvestmentId] [Id],
	                    [fkRiskGroupId] [RiskGroupId],
	                    [fkLegalEntityId] [LegalEntityId],
	                    [fkLegalEntityAccountId] [LegalEntityAccountId],
	                    [fkProductId] [ProductId],
	                    [AverageBalance],
	                    [LoadedCustomerRate] 
                    FROM [dbo].[tblProductInvestment] 
                    WHERE [pkProductInvestmentId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductInvestment> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductInvestment>(
                    @"SELECT [pkProductInvestmentId] [Id],
	                    [fkRiskGroupId] [RiskGroupId],
	                    [fkLegalEntityId] [LegalEntityId],
	                    [fkLegalEntityAccountId] [LegalEntityAccountId],
	                    [fkProductId] [ProductId],
	                    [AverageBalance],
	                    [LoadedCustomerRate] 
                    FROM [dbo].[tblProductInvestment]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ProductInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblProductInvestment]
                    SET [fkRiskGroupId] = @RiskGroupId,
                        [fkLegalEntityId] = @LegalEntityId,
                        [fkLegalEntityAccountId] = @LegalEntityAccountId,
                        [fkProductId] = @ProductId,
                        [AverageBalance] = @AverageBalance,
                        [LoadedCustomerRate] = @LoadedCustomerRate
                    WHERE [pkProductInvestmentId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        ProductId = model.ProductId,
                        AverageBalance = model.AverageBalance,
                        LoadedCustomerRate = model.LoadedCustomerRate
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ProductInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblProductInvestment] 
                            WHERE [pkProductInvestmentId] = @Id",
                    new {model.Id});
            }
        }
    }
}