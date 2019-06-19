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
                @"INSERT [dbo].[tblConcession] ([fkTypeId], [fkConcessionTypeId], [fkStatusId], [fkSubStatusId], [fkAAUserId], [fkRequestorId], [fkBCMUserId], [fkPCMUserId], [fkHOUserId], [fkRiskGroupId], [fkRegionId], [fkCentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive], [Archived], [fkAENumberUserId], [fkLegalEntityId]) 
                VALUES (@TypeId, @ConcessionTypeId, @StatusId, @SubStatusId, @AAUserId, @RequestorId, @BCMUserId, @PCMUserId, @HOUserId, @RiskGroupId, @RegionId, @CentreId, @ConcessionRef, @SMTDealNumber, @ConcessionDate, @DatesentForApproval, @Motivation, @DateActionedByBCM, @DateActionedByPCM, @DateActionedByHO, @MRS_CRS, @IsCurrent, @IsActive, @Archived, @AENumberUserId, @LegalEntityId) 
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
                        AAUserId = model.AAUserId,
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
                        DateActionedByBCM = model.DateActionedByBCM,
                        DateActionedByPCM = model.DateActionedByPCM,
                        DateActionedByHO = model.DateActionedByHO,
                        MRS_CRS = model.MRS_CRS,
                        IsCurrent = model.IsCurrent,
                        IsActive = model.IsActive,
                        Archived = model.Archived,
                        AENumberUserId = model.AENumberUserId,
                        LegalEntityId = model.LegalEntityId
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
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkAAUserId] [AAUserId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive], [Archived], [fkAENumberUserId] [AENumberUserId], [fkLegalEntityId] [LegalEntityId]
                    FROM [dbo].[tblConcession] 
                    WHERE [pkConcessionId] = @Id",
                    new { id }).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by concession reference is active.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByConcessionRefIsActive(string concessionReferenceNumber, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkAAUserId] [AAUserId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive], [Archived], [fkAENumberUserId] [AENumberUserId], [fkLegalEntityId] [LegalEntityId]
                    FROM [dbo].[tblConcession] 
                    WHERE [ConcessionRef] = @concessionReferenceNumber
                    AND [IsActive] = @isActive",
                    new { concessionReferenceNumber, isActive });
            }
        }

        /// <summary>
        /// Reads the by risk group identifier concession type identifier is active.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionTypeId">The concession type identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByRiskGroupIdConcessionTypeIdIsActive(int riskGroupId, int concessionTypeId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkAAUserId] [AAUserId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive], [Archived], [fkAENumberUserId] [AENumberUserId], [fkLegalEntityId] [LegalEntityId]
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRiskGroupId] = @riskGroupId
                    AND [fkConcessionTypeId] = @concessionTypeId
                    AND [IsActive] = @isActive",
                    new { riskGroupId, concessionTypeId, isActive });
            }
        }

        /// <summary>
        /// Reads the by risk group identifier concession type identifier is active approved.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionTypeId">The concession type identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns></returns>
        public IEnumerable<Concession> ReadByRiskGroupIdConcessionTypeIdIsActiveApproved(int riskGroupId,
            int concessionTypeId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkAAUserId] [AAUserId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive], [Archived], [fkAENumberUserId] [AENumberUserId], [fkLegalEntityId] [LegalEntityId] 
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRiskGroupId] = @riskGroupId
                    AND [fkConcessionTypeId] = @concessionTypeId
                    AND [IsActive] = @isActive
                    AND [fkStatusId] IN (2, 3)",
                    new { riskGroupId, concessionTypeId, isActive });
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
                    "SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [fkConcessionTypeId] [ConcessionTypeId], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [fkAAUserId] [AAUserId], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkRiskGroupId] [RiskGroupId], [fkRegionId] [RegionId], [fkCentreId] [CentreId], [ConcessionRef], [SMTDealNumber], [ConcessionDate], [DatesentForApproval], [Motivation], [DateActionedByBCM], [DateActionedByPCM], [DateActionedByHO], [MRS_CRS], [IsCurrent], [IsActive], [Archived], [fkAENumberUserId] [AENumberUserId], [fkLegalEntityId] [LegalEntityId] FROM [dbo].[tblConcession]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void DeactivateConcession(Concession model)
        {
            try
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    db.Execute(@"UPDATE [dbo].[tblConcessionDetail] set [Archived] = @Archived  WHERE [fkConcessionId] = @Id; 
                            UPDATE [dbo].[tblConcession]
                            SET [fkTypeId] = @TypeId, [fkConcessionTypeId] = @ConcessionTypeId, [fkStatusId] = @StatusId, [fkSubStatusId] = @SubStatusId, [fkAAUserId] = @AAUserId, [fkRequestorId] = @RequestorId, [fkBCMUserId] = @BCMUserId, [fkPCMUserId] = @PCMUserId, [fkHOUserId] = @HOUserId, [fkRiskGroupId] = @RiskGroupId, [fkRegionId] = @RegionId, [fkCentreId] = @CentreId, [ConcessionRef] = @ConcessionRef, [SMTDealNumber] = @SMTDealNumber, [ConcessionDate] = @ConcessionDate, [DatesentForApproval] = @DatesentForApproval, [Motivation] = @Motivation, [DateActionedByBCM] = @DateActionedByBCM, [DateActionedByPCM] = @DateActionedByPCM, [DateActionedByHO] = @DateActionedByHO, [MRS_CRS] = @MRS_CRS, [IsCurrent] = @IsCurrent, [IsActive] = @IsActive, [Archived] = @Archived, [fkAENumberUserId] = @AENumberUserId, [fkLegalEntityId] = @LegalEntityId
                            WHERE [pkConcessionId] = @Id",
                        new
                        {
                            Id = model.Id,
                            TypeId = model.TypeId,
                            ConcessionTypeId = model.ConcessionTypeId,
                            StatusId = model.StatusId,
                            SubStatusId = model.SubStatusId,
                            AAUserId = model.AAUserId,
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
                            DateActionedByBCM = model.DateActionedByBCM,
                            DateActionedByPCM = model.DateActionedByPCM,
                            DateActionedByHO = model.DateActionedByHO,
                            MRS_CRS = model.MRS_CRS,
                            IsCurrent = model.IsCurrent,
                            IsActive = model.IsActive,
                            Archived = model.Archived,
                            AENumberUserId = model.AENumberUserId,
                            LegalEntityId = model.LegalEntityId
                        });
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public void Update(Concession model)
        {
            try
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    db.Execute(@"UPDATE [dbo].[tblConcession]
                            SET [fkTypeId] = @TypeId, [fkConcessionTypeId] = @ConcessionTypeId, [fkStatusId] = @StatusId, [fkSubStatusId] = @SubStatusId, [fkAAUserId] = @AAUserId, [fkRequestorId] = @RequestorId, [fkBCMUserId] = @BCMUserId, [fkPCMUserId] = @PCMUserId, [fkHOUserId] = @HOUserId, [fkRiskGroupId] = @RiskGroupId, [fkRegionId] = @RegionId, [fkCentreId] = @CentreId, [ConcessionRef] = @ConcessionRef, [SMTDealNumber] = @SMTDealNumber, [ConcessionDate] = @ConcessionDate, [DatesentForApproval] = @DatesentForApproval, [Motivation] = @Motivation, [DateActionedByBCM] = @DateActionedByBCM, [DateActionedByPCM] = @DateActionedByPCM, [DateActionedByHO] = @DateActionedByHO, [MRS_CRS] = @MRS_CRS, [IsCurrent] = @IsCurrent, [IsActive] = @IsActive, [Archived] = @Archived, [fkAENumberUserId] = @AENumberUserId, [fkLegalEntityId] = @LegalEntityId
                            WHERE [pkConcessionId] = @Id",
                        new
                        {
                            Id = model.Id,
                            TypeId = model.TypeId,
                            ConcessionTypeId = model.ConcessionTypeId,
                            StatusId = model.StatusId,
                            SubStatusId = model.SubStatusId,
                            AAUserId = model.AAUserId,
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
                            DateActionedByBCM = model.DateActionedByBCM,
                            DateActionedByPCM = model.DateActionedByPCM,
                            DateActionedByHO = model.DateActionedByHO,
                            MRS_CRS = model.MRS_CRS,
                            IsCurrent = model.IsCurrent,
                            IsActive = model.IsActive,
                            Archived = model.Archived,
                            AENumberUserId = model.AENumberUserId,
                            LegalEntityId = model.LegalEntityId
                        });
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
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
                    new { model.Id });
            }
        }
    }
}