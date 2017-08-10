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
                @"INSERT [dbo].[tblConcession] ([fkTypeId], [ConcessionRef], [fkConcessionTypeId], [SMTDealNumber], [fkStatusId], [fkSubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId], [fkBCMUserId], [DateActionedByBCM], [fkPCMUserId], [DateActionedByPCM], [fkHOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS]) 
                VALUES (@fkTypeId, @ConcessionRef, @fkConcessionTypeId, @SMTDealNumber, @fkStatusId, @fkSubStatusId, @ConcessionDate, @DatesentForApproval, @Motivation, @DateApproved, @fkRequestorId, @fkBCMUserId, @DateActionedByBCM, @fkPCMUserId, @DateActionedByPCM, @fkHOUserId, @DateActionedByHO, @ExpiryDate, @CentreId, @IsCurrent, @IsActive, @MrsCrs) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        fkTypeId = model.TypeId,
                        ConcessionRef = model.ConcessionRef,
                        fkConcessionTypeId = model.ConcessionTypeId,
                        SMTDealNumber = model.SMTDealNumber,
                        fkStatusId = model.StatusId,
                        fkSubStatusId = model.SubStatusId,
                        ConcessionDate = model.ConcessionDate,
                        DatesentForApproval = model.DatesentForApproval,
                        Motivation = model.Motivation,
                        DateApproved = model.DateApproved,
                        fkRequestorId = model.RequestorId,
                        fkBCMUserId = model.BCMUserId,
                        DateActionedByBCM = model.DateActionedByBCM,
                        fkPCMUserId = model.PCMUserId,
                        DateActionedByPCM = model.DateActionedByPCM,
                        fkHOUserId = model.HOUserId,
                        DateActionedByHO = model.DateActionedByHO,
                        ExpiryDate = model.ExpiryDate,
                        CentreId = model.CentreId,
                        IsCurrent = model.IsCurrent,
                        IsActive = model.IsActive,
                        MrsCrs = model.MrsCrs
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
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs]
                    FROM [dbo].[tblConcession] 
                    WHERE [pkConcessionId] = @Id",
                    new {id}).SingleOrDefault();
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
                    "SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs] FROM [dbo].[tblConcession]");
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
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs] 
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRequestorId] = @requestorId
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
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs] 
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRequestorId] = @requestorId
                    AND [fkStatusId] = @statusId
                    AND [IsActive] = @isActive",
                    new { requestorId, statusId, isActive });
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
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive], [MRS_CRS] [MrsCrs] 
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRequestorId] = @requestorId
                    AND ([ExpiryDate] BETWEEN @startExpiryDate AND @endExpiryDate)
                    AND [IsActive] = @isActive",
                    new {requestorId, startExpiryDate, endExpiryDate, isActive});
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
                            SET [fkTypeId] = @fkTypeId, [ConcessionRef] = @ConcessionRef, [fkConcessionTypeId] = @fkConcessionTypeId, [SMTDealNumber] = @SMTDealNumber, [fkStatusId] = @fkStatusId, [fkSubStatusId] = @fkSubStatusId, [ConcessionDate] = @ConcessionDate, [DatesentForApproval] = @DatesentForApproval, [Motivation] = @Motivation, [DateApproved] = @DateApproved, [fkRequestorId] = @fkRequestorId, [fkBCMUserId] = @fkBCMUserId, [DateActionedByBCM] = @DateActionedByBCM, [fkPCMUserId] = @fkPCMUserId, [DateActionedByPCM] = @DateActionedByPCM, [fkHOUserId] = @fkHOUserId, [DateActionedByHO] = @DateActionedByHO, [ExpiryDate] = @ExpiryDate, [CentreId] = @CentreId, [IsCurrent] = @IsCurrent, [IsActive] = @IsActive, [MRS_CRS] = @MrsCrs
                            WHERE [pkConcessionId] = @Id",
                    new
                    {
                        Id = model.Id,
                        fkTypeId = model.TypeId,
                        ConcessionRef = model.ConcessionRef,
                        fkConcessionTypeId = model.ConcessionTypeId,
                        SMTDealNumber = model.SMTDealNumber,
                        fkStatusId = model.StatusId,
                        fkSubStatusId = model.SubStatusId,
                        ConcessionDate = model.ConcessionDate,
                        DatesentForApproval = model.DatesentForApproval,
                        Motivation = model.Motivation,
                        DateApproved = model.DateApproved,
                        fkRequestorId = model.RequestorId,
                        fkBCMUserId = model.BCMUserId,
                        DateActionedByBCM = model.DateActionedByBCM,
                        fkPCMUserId = model.PCMUserId,
                        DateActionedByPCM = model.DateActionedByPCM,
                        fkHOUserId = model.HOUserId,
                        DateActionedByHO = model.DateActionedByHO,
                        ExpiryDate = model.ExpiryDate,
                        CentreId = model.CentreId,
                        IsCurrent = model.IsCurrent,
                        IsActive = model.IsActive,
                        MrsCrs = model.MrsCrs
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
    }
}