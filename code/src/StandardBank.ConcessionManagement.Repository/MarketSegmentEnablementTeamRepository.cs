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
    /// MarketSegment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IMarketSegmentEnablementTeamRepository" />
    public class MarketSegmentEnablementTeamRepository : IMarketSegmentEnablementTeamRepository
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
        /// Initializes a new instance of the <see cref="MarketSegmentRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public MarketSegmentEnablementTeamRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        public IEnumerable<MarketSegmentEnablementTeam> ReadAll()
        {
            Func<IEnumerable<MarketSegmentEnablementTeam>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<MarketSegmentEnablementTeam>(
                        @"Select pkMarketSegmentEnablementTeamId [Id], fkMarketSegmentId [MarketSegmentId], EnablementTeamUserEmail, IsActive
                        From [dbo].[tblMarketSegmentEnablementTeam]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.MarketSegmentEnablementRepository.ReadAll);
        }

    }
}