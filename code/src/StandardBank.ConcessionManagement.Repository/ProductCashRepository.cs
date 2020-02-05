using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ProductCash repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IProductCashRepository" />
    public class ProductCashRepository : IProductCashRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCashRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ProductCashRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ProductCash Create(ProductCash model)
        {
            const string sql =
                @"INSERT [dbo].[tblProductCash] ([fkRiskGroupId], [fkLegalEntityId], [fkLegalEntityAccountId], [fkTableNumberId], [Channel], [BpId], [Volume], [Value], [LoadedPrice]) 
                VALUES (@RiskGroupId, @LegalEntityId, @LegalEntityAccountId, @TableNumberId, @Channel, @BpId, @Volume, @Value, @LoadedPrice) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TableNumberId = model.TableNumberId,
                        Channel = model.Channel,
                        BpId = model.BpId,
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
        public ProductCash ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductCash>(
                    @"SELECT [pkProductCashId] [Id],
                        [fkRiskGroupId] [RiskGroupId],
                        [fkLegalEntityId] [LegalEntityId],
                        [fkLegalEntityAccountId] [LegalEntityAccountId],
                        [fkTableNumberId] [TableNumberId],
                        [Channel],
                        [BpId],
                        [Volume],
                        [Value],
                        [LoadedPrice] 
                    FROM [dbo].[tblProductCash] 
                    WHERE [pkProductCashId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by risk group identifier.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <returns></returns>
        public IEnumerable<ProductCash> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductCash>(
                    @"SELECT [pkProductCashId] [Id],
                        [fkRiskGroupId] [RiskGroupId],
                        [fkLegalEntityId] [LegalEntityId],
                        [fkLegalEntityAccountId] [LegalEntityAccountId],
                        [fkTableNumberId] [TableNumberId],
                        [Channel],
                        [BpId],
                        [Volume],
                        [Value],
                        [LoadedPrice] 
                    FROM [dbo].[tblProductCash] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductCash> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ProductCash>(
                    @"SELECT [pkProductCashId] [Id],
	                    [fkRiskGroupId] [RiskGroupId],
	                    [fkLegalEntityId] [LegalEntityId],
	                    [fkLegalEntityAccountId] [LegalEntityAccountId],
	                    [fkTableNumberId] [TableNumberId],
	                    [Channel],
	                    [BpId],
	                    [Volume],
	                    [Value],
	                    [LoadedPrice] 
                    FROM [dbo].[tblProductCash]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ProductCash model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblProductCash]
                    SET [fkRiskGroupId] = @RiskGroupId,
	                    [fkLegalEntityId] = @LegalEntityId,
	                    [fkLegalEntityAccountId] = @LegalEntityAccountId,
	                    [fkTableNumberId] = @TableNumberId,
	                    [Channel] = @Channel,
	                    [BpId] = @BpId,
	                    [Volume] = @Volume,
	                    [Value] = @Value,
	                    [LoadedPrice] = @LoadedPrice
                    WHERE [pkProductCashId] = @Id",
                    new
                    {
                        Id = model.Id,
                        RiskGroupId = model.RiskGroupId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TableNumberId = model.TableNumberId,
                        Channel = model.Channel,
                        BpId = model.BpId,
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
        public void Delete(ProductCash model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblProductCash] 
                            WHERE [pkProductCashId] = @Id",
                    new {model.Id});
            }
        }
    }
}