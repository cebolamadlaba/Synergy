using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Concession repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionRepository" />
    public class ConcessionRepository : IConcessionRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Concession Create(Concession model)
        {
            const string sql =
                @"INSERT [dbo].[tblConcession] ([fkTypeId], [fkConcessionTypeId], [fkStatusId], [fkSubStatusId], [fkRequestorId], [fkBCMUserId], [fkPCMUserId], [fkHOUserId], [fkRiskGroupId], [fkRegionId], [fkCentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]) 
                                VALUES (@TypeId, @ConcessionTypeId, @StatusId, @SubStatusId, @RequestorId, @BCMUserId, @PCMUserId, @HOUserId, @RiskGroupId, @RegionId, @CentreId, @ConcessionRef, @SMTDealNumber, @ConcessionDate, @DatesentForApproval, @Motivation, @DateApproved, @DateActionedByBCM, @DateActionedByPCM, @DateActionedByHO, @MRS_CRS, @IsCurrent, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        TypeId = model.TypeId,
                        ConcessionTypeId = model.ConcessionTypeId,
                        StatusId = model.StatusId,
                        SubStatusId = model.SubStatusId,
                        RequestorId = model.RequestorId,
                        BCMUserId = model.BCMUserId,
                        PCMUserId = model.PCMUserId,
                        HOUserId = model.HOUserId,
                        RiskGroupId = model.RiskGroupId,
                        RegionId = model.RegionId,
                        CentreId = model.CentreId,
                        ConcessionRef = model.ConcessionRef,
                        SMTDealNumber = model.SMTDealNumber,
                        ConcessionDate = model.ConcessionDate,
                        DatesentForApproval = model.DatesentForApproval,
                        Motivation = model.Motivation,
                        DateApproved = model.DateApproved,
                        DateActionedByBCM = model.DateActionedByBCM,
                        DateActionedByPCM = model.DateActionedByPCM,
                        DateActionedByHO = model.DateActionedByHO,
                        MRS_CRS = model.MRS_CRS,
                        IsCurrent = model.IsCurrent,
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
        public Concession ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    "SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive] FROM [dbo].[tblConcession] WHERE [pkConcessionId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the approved concessions for the requestor Id
        /// </summary>
        /// <param name="requestorId"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadApprovedConcessions(int requestorId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId] 
                    FROM [dbo].[tblConcession] 
                    where [fkRequestorId] = @requestorId
                    and [fkStatusId] in (2, 3)
                    and IsActive = 1
                    and IsCurrent = 1
                    and (ExpiryDate is null or ExpiryDate > getdate())",
                    new {requestorId});
            }
        }

        /// <summary>
        /// Reads all the records for the requestor id, status id, sub status id and is active
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="subStatusId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByRequestorIdStatusIdSubStatusIdIsActive(int requestorId, int statusId,
            int subStatusId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    WHERE ([fkRequestorId] = @requestorId OR @requestorId = 0)
                    AND [fkStatusId] = @statusId
                    AND [fkSubStatusId] = @subStatusId
                    AND [IsActive] = @isActive",
                    new {requestorId, statusId, subStatusId, isActive});
            }
        }

        /// <summary>
        /// Reads by the requestor id, status id and is active flag
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="statusId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByRequestorIdStatusIdIsActive(int requestorId, int statusId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRequestorId] = @requestorId
                    AND [fkStatusId] = @statusId
                    AND [IsActive] = @isActive",
                    new {requestorId, statusId, isActive});
            }
        }

        /// <summary>
        /// Reads by the requestor id, between the start and end expiry date and is active 
        /// </summary>
        /// <param name="requestorId"></param>
        /// <param name="startExpiryDate"></param>
        /// <param name="endExpiryDate"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(int requestorId,
            DateTime startExpiryDate,
            DateTime endExpiryDate, bool isActive)
        {
            if (startExpiryDate == DateTime.MinValue)
                startExpiryDate = new DateTime(1900, 1, 1);

            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId] 
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRequestorId] = @requestorId
                    AND ([ExpiryDate] BETWEEN @startExpiryDate AND @endExpiryDate)
                    AND [IsActive] = @isActive",
                    new {requestorId, startExpiryDate, endExpiryDate, isActive});
            }
        }

        /// <summary>
        /// Reads by the risk group id, concession type is and the is active flag
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <param name="concessionTypeId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByRiskGroupIdConcessionTypeIdIsActive(int riskGroupId, int concessionTypeId,
            bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] c
                    WHERE [fkRiskGroupId] = @riskGroupId
                    AND [fkConcessionTypeId] = @concessionTypeId
                    AND c.[IsActive] = @isActive",
                    new {riskGroupId, concessionTypeId, isActive});
            }
        }

        /// <summary>
        /// Reads by the concession reference and the is active flag
        /// </summary>
        /// <param name="concessionRef"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByConcessionRefIsActive(string concessionRef, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] c
                    WHERE [ConcessionRef] = @concessionRef
                    AND c.[IsActive] = @isActive",
                    new {concessionRef, isActive});
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Concession> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    "SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive] FROM [dbo].[tblConcession]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Concession model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcession]
                            SET [fkTypeId] = @TypeId, [fkConcessionTypeId] = @ConcessionTypeId, [fkStatusId] = @StatusId, [fkSubStatusId] = @SubStatusId, [fkRequestorId] = @RequestorId, [fkBCMUserId] = @BCMUserId, [fkPCMUserId] = @PCMUserId, [fkHOUserId] = @HOUserId, [fkRiskGroupId] = @RiskGroupId, [fkRegionId] = @RegionId, [fkCentreId] = @CentreId, [ConcessionRef] = @ConcessionRef, [SMTDealNumber] = @SMTDealNumber, [ConcessionDate] = @ConcessionDate, [DatesentForApproval] = @DatesentForApproval, [Motivation] = @Motivation, [DateApproved] = @DateApproved, [DateActionedByBCM] = @DateActionedByBCM, [DateActionedByPCM] = @DateActionedByPCM, [DateActionedByHO] = @DateActionedByHO, [MRS_CRS] = @MRS_CRS, [IsCurrent] = @IsCurrent, [IsActive] = @IsActive
                            WHERE [pkConcessionId] = @Id",
                    new
                    {
                        Id = model.Id,
                        TypeId = model.TypeId,
                        ConcessionTypeId = model.ConcessionTypeId,
                        StatusId = model.StatusId,
                        SubStatusId = model.SubStatusId,
                        RequestorId = model.RequestorId,
                        BCMUserId = model.BCMUserId,
                        PCMUserId = model.PCMUserId,
                        HOUserId = model.HOUserId,
                        RiskGroupId = model.RiskGroupId,
                        RegionId = model.RegionId,
                        CentreId = model.CentreId,
                        ConcessionRef = model.ConcessionRef,
                        SMTDealNumber = model.SMTDealNumber,
                        ConcessionDate = model.ConcessionDate,
                        DatesentForApproval = model.DatesentForApproval,
                        Motivation = model.Motivation,
                        DateApproved = model.DateApproved,
                        DateActionedByBCM = model.DateActionedByBCM,
                        DateActionedByPCM = model.DateActionedByPCM,
                        DateActionedByHO = model.DateActionedByHO,
                        MRS_CRS = model.MRS_CRS,
                        IsCurrent = model.IsCurrent,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Concession model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcession] WHERE [pkConcessionId] = @Id",
                    new {model.Id});
            }
        }

        public IEnumerable<Concession> GetActionedByBCMUser(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    where fkBCMUserId = @userId
                    AND [IsActive] = 1", new {userId});
            }
        }

        public IEnumerable<Concession> GetActionedByPCMUser(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    where [fkPCMUserId] = @userId
                    AND [IsActive] = 1", new {userId});
            }
        }

        public IEnumerable<Concession> GetActionedByHOUser(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    where [fkHOUserId] = @userId
                    AND [IsActive] = 1", new {userId});
            }
        }

        public IEnumerable<Concession> GetConcessions(IEnumerable<int> concessionIds)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    where [pkConcessionId] IN (@concessionIds)
                    AND [IsActive] = 1", new {concessionIds = concessionIds.ToArray()});
            }
        }

        /// <summary>
        /// Reads by the centre id, status id, sub status id and the is active flag
        /// </summary>
        /// <param name="centreId"></param>
        /// <param name="statusId"></param>
        /// <param name="subStatusId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByCentreIdStatusIdSubStatusIdIsActive(int centreId, int statusId,
            int subStatusId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive]
                    FROM [dbo].[tblConcession] 
                    WHERE [fkCentreId] = @centreId
                    AND [fkStatusId] = @statusId
                    AND [fkSubStatusId] = @subStatusId
                    AND [IsActive] = @isActive",
                    new {centreId, statusId, subStatusId, isActive});
            }
        }
    }
}