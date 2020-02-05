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
    /// ChannelType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IInterestPricingCategoryRepository" />
    public class InterestPricingCategoryRepository : IInterestPricingCategoryRepository
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
        /// Initializes a new instance of the <see cref="RateTypeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public InterestPricingCategoryRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public InterestPricingCategory ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InterestPricingCategory> ReadAll()
        {
            Func<IEnumerable<InterestPricingCategory>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<InterestPricingCategory>(
                        @"SELECT [pkInterestPricingCategoryId] [Id], 
                                [Description], 
                                [IsActive] 
                        FROM [dbo].[tblInterestPricingCategory]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.InterestPricingCategory.ReadAll);
        }

    }
}