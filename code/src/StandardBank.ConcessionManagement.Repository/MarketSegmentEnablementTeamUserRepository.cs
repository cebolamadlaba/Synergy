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
    /// MarketSegmentEnablementTeamUser repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IMarketSegmentEnablementTeamUserRepository" />
    public class MarketSegmentEnablementTeamUserRepository : IMarketSegmentEnablementTeamUserRepository
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
        public MarketSegmentEnablementTeamUserRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        public IEnumerable<MarketSegmentEnablementTeamUser> ReadAll()
        {
            Func<IEnumerable<MarketSegmentEnablementTeamUser>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<MarketSegmentEnablementTeamUser>(
                        @"Select et.pkMarketSegmentEnablementTeamId [Id], et.fkMarketSegmentId [MarketSegmentId], et.fkUserId [UserId], et.IsActive,
		                        u.FirstName + ' ' + u.Surname [Fullname], u.EmailAddress
                        From [dbo].[tblMarketSegmentEnablementTeamUser] et
                        Inner Join [dbo].[tblUser] u On u.pkUserId = et.fkUserId");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.MarketSegmentEnablementRepository.ReadAll);
        }

        public IEnumerable<ConcessionTypeMismatchEscalation> GetConcessionTypeMismatchEscalation()
        {
            Func<IEnumerable<ConcessionTypeMismatchEscalation>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<ConcessionTypeMismatchEscalation>(
                        @"SELECT cme.[pkConcessionTypeMismatchEscalationId] [ConcessionTypeMismatchEscalationId], cme.[fkConcessionTypeId] [ConcessionTypeId], cme.[LastEscalationSentDateTime], 
		                        ct.[Description] [ConcessionType]
                        FROM [dbo].[tblConcessionTypeMismatchEscalation] cme
                        Inner Join rtblConcessionType ct On ct.pkConcessionTypeId = cme.[fkConcessionTypeId]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.ConcessionTypeMismatchEscalation.ReadAll);
        }
    }
}