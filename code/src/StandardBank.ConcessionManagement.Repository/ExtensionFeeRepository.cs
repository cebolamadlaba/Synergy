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
    public class ExtensionFeeRepository : IExtensionFeeRepository
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
        /// Initializes a new instance of the <see cref="RelationshipRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ExtensionFeeRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets active fee value.
        /// </summary>
        /// <returns></returns>
        public decimal GetActiveFee()
        {
            Func<IEnumerable<ExtensionFee>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<ExtensionFee>(
                        @"select [pkExtensionFeeId] [Id],
	                        [ExtensionFee],
	                        [IsActive]
                        from [rtblExtensionFee]
                        where [IsActive] = 1");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.ExtensionFeeRepository.ReadByIdIsActive).FirstOrDefault().extensionFee;
        }
    }
}
