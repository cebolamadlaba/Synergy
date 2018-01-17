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
    /// RiskGroup repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IRiskGroupRepository" />
    public class RiskGroupRepository : IRiskGroupRepository
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
        /// Initializes a new instance of the <see cref="RiskGroupRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public RiskGroupRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public RiskGroup Create(RiskGroup model)
        {
            const string sql = @"INSERT [dbo].[tblRiskGroup] ([fkMarketSegmentId], [RiskGroupNumber], [RiskGroupName], [IsActive]) 
                                VALUES (@fkMarketSegmentId, @RiskGroupNumber, @RiskGroupName, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        fkMarketSegmentId = model.MarketSegmentId,
                        RiskGroupNumber = model.RiskGroupNumber,
                        RiskGroupName = model.RiskGroupName,
                        IsActive = model.IsActive
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public RiskGroup ReadById(int id)
        {
            RiskGroup Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<RiskGroup>(
                            @"SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [RiskGroupNumber], [RiskGroupName], [IsActive] 
                            FROM [dbo].[tblRiskGroup]
                            WHERE [pkRiskGroupId] = @id", new {id})
                        .FirstOrDefault();
                }
            }

            return _cacheManager.ReturnFromCache(Function, 30, CacheKey.Repository.RiskGroupRepository.ReadById,
                new CacheKeyParameter(nameof(id), id));
        }

        /// <summary>
        /// Reads by the id and is active flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public RiskGroup ReadByIdIsActive(int id, bool isActive)
        {
            RiskGroup Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<RiskGroup>(
                        @"SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [RiskGroupNumber], [RiskGroupName], [IsActive] 
                        FROM [dbo].[tblRiskGroup]
                        WHERE [pkRiskGroupId] = @id
                        AND [IsActive] = @isActive", new { id, isActive }).Single();
                }
            }

            return _cacheManager.ReturnFromCache(Function, 30, CacheKey.Repository.RiskGroupRepository.ReadByIdIsActive,
                new CacheKeyParameter(nameof(id), id), new CacheKeyParameter(nameof(isActive), isActive));
        }

        /// <summary>
        /// Reads by the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public RiskGroup ReadByRiskGroupNumberIsActive(int riskGroupNumber, bool isActive)
        {
            RiskGroup Function()
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<RiskGroup>(
                        @"SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [RiskGroupNumber], [RiskGroupName], [IsActive] 
                        FROM [dbo].[tblRiskGroup]
                        WHERE [RiskGroupNumber] = @riskGroupNumber
                        AND [IsActive] = @isActive", new { riskGroupNumber, isActive }).FirstOrDefault();
                }
            }

            return _cacheManager.ReturnFromCache(Function, 30,
                CacheKey.Repository.RiskGroupRepository.ReadByRiskGroupNumberIsActive,
                new CacheKeyParameter(nameof(riskGroupNumber), riskGroupNumber),
                new CacheKeyParameter(nameof(isActive), isActive));
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RiskGroup> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RiskGroup>(
                    "SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [RiskGroupNumber], [RiskGroupName], [IsActive] FROM [dbo].[tblRiskGroup]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(RiskGroup model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblRiskGroup]
                            SET [fkMarketSegmentId] = @fkMarketSegmentId, [RiskGroupNumber] = @RiskGroupNumber, [RiskGroupName] = @RiskGroupName, [IsActive] = @IsActive
                            WHERE [pkRiskGroupId] = @Id",
                    new
                    {
                        Id = model.Id,
                        fkMarketSegmentId = model.MarketSegmentId,
                        RiskGroupNumber = model.RiskGroupNumber,
                        RiskGroupName = model.RiskGroupName,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(RiskGroup model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblRiskGroup] WHERE [pkRiskGroupId] = @Id",
                    new { model.Id });
            }
        }
    }
}