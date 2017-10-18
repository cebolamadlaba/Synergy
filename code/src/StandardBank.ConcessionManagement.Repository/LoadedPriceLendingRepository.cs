using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// LoadedPriceLending repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ILoadedPriceLendingRepository" />
    public class LoadedPriceLendingRepository : ILoadedPriceLendingRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedPriceLendingRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public LoadedPriceLendingRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public LoadedPriceLending Create(LoadedPriceLending model)
        {
            const string sql =
                @"INSERT [dbo].[tblLoadedPriceLending] ([fkProductTypeId], [fkLegalEntityAccountId], [MarginToPrime]) 
                                VALUES (@ProductTypeId, @LegalEntityAccountId, @MarginToPrime) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ProductTypeId = model.ProductTypeId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        MarginToPrime = model.MarginToPrime
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LoadedPriceLending ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceLending>(
                    "SELECT [pkLoadedPriceLendingId] [Id], [fkProductTypeId] [ProductTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [MarginToPrime] FROM [dbo].[tblLoadedPriceLending] WHERE [pkLoadedPriceLendingId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the product type id and the legal entity account id
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="legalEntityAccountId"></param>
        /// <returns></returns>
        public LoadedPriceLending ReadByProductTypeIdLegalEntityAccountId(int productTypeId, int legalEntityAccountId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceLending>(
                    @"SELECT [pkLoadedPriceLendingId] [Id], [fkProductTypeId] [ProductTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [MarginToPrime] 
                    FROM [dbo].[tblLoadedPriceLending] 
                    WHERE [fkProductTypeId] = @productTypeId
                    AND [fkLegalEntityAccountId] = @legalEntityAccountId",
                    new { productTypeId, legalEntityAccountId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LoadedPriceLending> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceLending>(
                    "SELECT [pkLoadedPriceLendingId] [Id], [fkProductTypeId] [ProductTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [MarginToPrime] FROM [dbo].[tblLoadedPriceLending]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(LoadedPriceLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblLoadedPriceLending]
                            SET [fkProductTypeId] = @ProductTypeId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [MarginToPrime] = @MarginToPrime
                            WHERE [pkLoadedPriceLendingId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ProductTypeId = model.ProductTypeId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        MarginToPrime = model.MarginToPrime
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(LoadedPriceLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblLoadedPriceLending] WHERE [pkLoadedPriceLendingId] = @Id",
                    new {model.Id});
            }
        }
    }
}
