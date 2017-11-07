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
        /// Initializes a new instance of the <see cref="RiskGroupRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        public RiskGroupRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public RiskGroup Create(RiskGroup model)
        {
            const string sql = @"INSERT [dbo].[tblRiskGroup] ([fkMarketSegmentId], [fkRegionId], [RiskGroupNumber], [RiskGroupName], [IsActive]) 
                                VALUES (@fkMarketSegmentId, @fkRegionId, @RiskGroupNumber, @RiskGroupName, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        fkMarketSegmentId = model.MarketSegmentId,
                        fkRegionId = model.RegionId,
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
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RiskGroup>(
                    @"SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRegionId] [RegionId], [RiskGroupNumber], [RiskGroupName], [IsActive] 
                    FROM [dbo].[tblRiskGroup]
                    WHERE [pkRiskGroupId] = @id", new { id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Reads by the id and is active flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public RiskGroup ReadByIdIsActive(int id, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RiskGroup>(
                    @"SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRegionId] [RegionId], [RiskGroupNumber], [RiskGroupName], [IsActive] 
                    FROM [dbo].[tblRiskGroup]
                    WHERE [pkRiskGroupId] = @id
                    AND [IsActive] = @isActive", new {id, isActive}).Single();
            }
        }

        /// <summary>
        /// Reads by the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public RiskGroup ReadByRiskGroupNumberIsActive(int riskGroupNumber, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RiskGroup>(
                    @"SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRegionId] [RegionId], [RiskGroupNumber], [RiskGroupName], [IsActive] 
                    FROM [dbo].[tblRiskGroup]
                    WHERE [RiskGroupNumber] = @riskGroupNumber
                    AND [IsActive] = @isActive", new {riskGroupNumber, isActive}).First();
            }
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
                    "SELECT [pkRiskGroupId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRegionId] [RegionId], [RiskGroupNumber], [RiskGroupName], [IsActive] FROM [dbo].[tblRiskGroup]");
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
                            SET [fkMarketSegmentId] = @fkMarketSegmentId, [fkRegionId] = @fkRegionId, [RiskGroupNumber] = @RiskGroupNumber, [RiskGroupName] = @RiskGroupName, [IsActive] = @IsActive
                            WHERE [pkRiskGroupId] = @Id",
                    new
                    {
                        Id = model.Id,
                        fkMarketSegmentId = model.MarketSegmentId,
                        fkRegionId = model.RegionId,
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
                    new {model.Id});
            }
        }
    }
}