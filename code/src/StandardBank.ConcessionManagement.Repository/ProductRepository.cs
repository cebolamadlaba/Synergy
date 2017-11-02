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
    /// Product repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IProductRepository" />
    public class ProductRepository : IProductRepository
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
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ProductRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Product Create(Product model)
        {
            const string sql =
                @"INSERT [dbo].[rtblProduct] ([fkConcessionTypeId], [Description], [IsActive]) 
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
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<Product>(
                        "SELECT [pkProductId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [Description], [IsActive] FROM [dbo].[rtblProduct]");
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
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblProduct]
                            SET [fkConcessionTypeId] = @ConcessionTypeId, [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkProductId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionTypeId = model.ConcessionTypeId,
                        Description = model.Description,
                        IsActive = model.IsActive
                    });
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
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblProduct] WHERE [pkProductId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ProductRepository.ReadAll);
        }
    }
}
