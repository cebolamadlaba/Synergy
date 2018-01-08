using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// LegalEntity repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ILegalEntityRepository" />
    public class LegalEntityRepository : ILegalEntityRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LegalEntityRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public LegalEntityRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public LegalEntity Create(LegalEntity model)
        {
            const string sql =
                @"INSERT [dbo].[tblLegalEntity] ([fkMarketSegmentId], [fkRiskGroupId], [CustomerName], [CustomerNumber], [IsActive], [ContactPerson], [PostalAddress], [City], [PostalCode], [fkUserId]) 
                VALUES (@MarketSegmentId, @RiskGroupId, @CustomerName, @CustomerNumber, @IsActive, @ContactPerson, @PostalAddress, @City, @PostalCode, @UserId) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        MarketSegmentId = model.MarketSegmentId,
                        RiskGroupId = model.RiskGroupId,
                        CustomerName = model.CustomerName,
                        CustomerNumber = model.CustomerNumber,
                        IsActive = model.IsActive,
                        ContactPerson = model.ContactPerson,
                        PostalAddress = model.PostalAddress,
                        City = model.City,
                        PostalCode = model.PostalCode,
                        UserId = model.UserId
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LegalEntity ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LegalEntity>(
                    @"SELECT [pkLegalEntityId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRiskGroupId] [RiskGroupId], [CustomerName], [CustomerNumber], [IsActive], [ContactPerson], [PostalAddress], [City], [PostalCode], [fkUserId] [UserId] 
                    FROM [dbo].[tblLegalEntity] 
                    WHERE [pkLegalEntityId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the id and is active flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public LegalEntity ReadByIdIsActive(int id, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LegalEntity>(
                    @"SELECT [pkLegalEntityId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRiskGroupId] [RiskGroupId], [CustomerName], [CustomerNumber], [IsActive], [ContactPerson], [PostalAddress], [City], [PostalCode], [fkUserId] [UserId] 
                    FROM [dbo].[tblLegalEntity] 
                    WHERE [pkLegalEntityId] = @Id 
                    AND [IsActive] = @isActive",
                    new {id, isActive}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the risk group id specified
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<LegalEntity> ReadByRiskGroupIdIsActive(int riskGroupId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LegalEntity>(
                    @"SELECT [pkLegalEntityId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRiskGroupId] [RiskGroupId], [CustomerName], [CustomerNumber], [IsActive], [ContactPerson], [PostalAddress], [City], [PostalCode], [fkUserId] [UserId] 
                    FROM [dbo].[tblLegalEntity] 
                    WHERE [fkRiskGroupId] = @riskGroupId
                    AND [IsActive] = @isActive",
                    new {riskGroupId, isActive});
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LegalEntity> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LegalEntity>(
                    @"SELECT [pkLegalEntityId] [Id], [fkMarketSegmentId] [MarketSegmentId], [fkRiskGroupId] [RiskGroupId], [CustomerName], [CustomerNumber], [IsActive], [ContactPerson], [PostalAddress], [City], [PostalCode], [fkUserId] [UserId] 
                    FROM [dbo].[tblLegalEntity]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(LegalEntity model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblLegalEntity]
                            SET [fkMarketSegmentId] = @MarketSegmentId, [fkRiskGroupId] = @RiskGroupId, [CustomerName] = @CustomerName, [CustomerNumber] = @CustomerNumber, [IsActive] = @IsActive, [ContactPerson] = @ContactPerson, [PostalAddress] = @PostalAddress, [City] = @City, [PostalCode] = @PostalCode, [fkUserId] = @UserId
                            WHERE [pkLegalEntityId] = @Id",
                    new
                    {
                        Id = model.Id,
                        MarketSegmentId = model.MarketSegmentId,
                        RiskGroupId = model.RiskGroupId,
                        CustomerName = model.CustomerName,
                        CustomerNumber = model.CustomerNumber,
                        IsActive = model.IsActive,
                        ContactPerson = model.ContactPerson,
                        PostalAddress = model.PostalAddress,
                        City = model.City,
                        PostalCode = model.PostalCode,
                        UserId = model.UserId
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(LegalEntity model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblLegalEntity] WHERE [pkLegalEntityId] = @Id",
                    new {model.Id});
            }
        }
    }
}