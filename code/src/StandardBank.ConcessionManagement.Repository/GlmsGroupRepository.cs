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
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IGlmsGroupRepository" />
    public class GlmsGroupRepository : IGlmsGroupRepository
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
        /// Initializes a new instance of the <see cref="GlmsGroupRepositoryRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public GlmsGroupRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GlmsGroup ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GlmsGroup> ReadAll()
        {
            Func<IEnumerable<GlmsGroup>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<GlmsGroup>(
                        @"SELECT Distinct [pkGlmsGroupId] [Id], 
                                    [GroupNumber],
                                    [GroupName], 
                                    [IsActive] 
                            FROM [dbo].[tblGlmsGroup] gg
                            Inner Join tblProductGlms pg On pg.fkGroupId = gg.pkGlmsGroupId");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.GlmsGroupRepository.ReadAll);
        }

        public IEnumerable<GlmsGroup> ReadAllByRiskGroupAndOrSapBpId(int riskGroupNumber, int? sapBpId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<GlmsGroup>(
                    @"SELECT  Distinct [pkGlmsGroupId] [Id], 
                                    [GroupNumber],
                                    [GroupName], 
                                    gg.[IsActive] 
                            FROM [dbo].[tblGlmsGroup] gg
                            Inner Join tblProductGlms pg On pg.fkGroupId = gg.pkGlmsGroupId
                            Inner Join tblRiskGroup rg On rg.pkRiskGroupId = pg.fkRiskGroupId
                            Left Join tblLegalEntity le On le.pkLegalEntityId = pg.fkLegalEntityId
                            Where (@sapBpId = 0 And rg.RiskGroupNumber = @riskGroupNumber)
                            Or (@sapBpId <> 0 And le.CustomerNumber = @sapBpId)"
                    , new { riskGroupNumber, sapBpId });
            }
        }
    }
}