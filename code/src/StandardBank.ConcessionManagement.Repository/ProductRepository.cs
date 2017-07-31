using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Product repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IProductRepository" />
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ProductRepository(IConfigurationData configurationData, ICacheManager cacheManager)
        {
            _configurationData = configurationData;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Product Create(Product model)
        {
            const string sql = @"INSERT [dbo].[rtblProduct] ([fkConcessionTypeId], [Description], [IsActive]) 
                                VALUES (@fkConcessionTypeId, @Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionTypeId = model.ConcessionTypeId, Description = model.Description, IsActive = model.IsActive}).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ProductRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Product ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads by the concession type id and the is active flag
        /// </summary>
        /// <param name="concessionTypeId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Product> ReadByConcessionTypeIdIsActive(int concessionTypeId, bool isActive)
        {
            return ReadAll().Where(_ => _.ConcessionTypeId == concessionTypeId && _.IsActive == isActive);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> ReadAll()
        {
            Func<IEnumerable<Product>> function = () =>
            {
                using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            	{
                	return db.Query<Product>("SELECT [pkProductId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [Description], [IsActive] FROM [dbo].[rtblProduct]");
            	}
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.ProductRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Product model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblProduct]
                            SET [fkConcessionTypeId] = @fkConcessionTypeId, [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkProductId] = @Id",
                    new {Id = model.Id, fkConcessionTypeId = model.ConcessionTypeId, Description = model.Description, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ProductRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Product model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblProduct] WHERE [pkProductId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ProductRepository.ReadAll);
        }
    }
}
