using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Concession Create(Concession model)
        {
            const string sql = @"INSERT [dbo].[tblConcession] ([fkTypeId], [ConcessionRef], [fkLegalEntityId], [fkConcessionTypeId], [SMTDealNumber], [fkStatusId], [fkSubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId], [fkBCMUserId], [DateActionedByBCM], [fkPCMUserId], [DateActionedByPCM], [fkHOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive]) 
                                VALUES (@fkTypeId, @ConcessionRef, @fkLegalEntityId, @fkConcessionTypeId, @SMTDealNumber, @fkStatusId, @fkSubStatusId, @ConcessionDate, @DatesentForApproval, @Motivation, @DateApproved, @fkRequestorId, @fkBCMUserId, @DateActionedByBCM, @fkPCMUserId, @DateActionedByPCM, @fkHOUserId, @DateActionedByHO, @ExpiryDate, @CentreId, @IsCurrent, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkTypeId = model.TypeId, ConcessionRef = model.ConcessionRef, fkLegalEntityId = model.LegalEntityId, fkConcessionTypeId = model.ConcessionTypeId, SMTDealNumber = model.SMTDealNumber, fkStatusId = model.StatusId, fkSubStatusId = model.SubStatusId, ConcessionDate = model.ConcessionDate, DatesentForApproval = model.DatesentForApproval, Motivation = model.Motivation, DateApproved = model.DateApproved, fkRequestorId = model.RequestorId, fkBCMUserId = model.BCMUserId, DateActionedByBCM = model.DateActionedByBCM, fkPCMUserId = model.PCMUserId, DateActionedByPCM = model.DateActionedByPCM, fkHOUserId = model.HOUserId, DateActionedByHO = model.DateActionedByHO, ExpiryDate = model.ExpiryDate, CentreId = model.CentreId, IsCurrent = model.IsCurrent, IsActive = model.IsActive}).Single();
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkLegalEntityId] [LegalEntityId], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive] 
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Concession>("SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkLegalEntityId] [LegalEntityId], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive] FROM [dbo].[tblConcession]");
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Concession>(
                    @"SELECT [pkConcessionId] [Id], [fkTypeId] [TypeId], [ConcessionRef], [fkLegalEntityId] [LegalEntityId], [fkConcessionTypeId] [ConcessionTypeId], [SMTDealNumber], [fkStatusId] [StatusId], [fkSubStatusId] [SubStatusId], [ConcessionDate], [DatesentForApproval], [Motivation], [DateApproved], [fkRequestorId] [RequestorId], [fkBCMUserId] [BCMUserId], [DateActionedByBCM], [fkPCMUserId] [PCMUserId], [DateActionedByPCM], [fkHOUserId] [HOUserId], [DateActionedByHO], [ExpiryDate], [CentreId], [IsCurrent], [IsActive] 
                    FROM [dbo].[tblConcession] 
                    WHERE [fkRequestorId] = @requestorId
                    AND [fkStatusId] = @statusId
                    AND [fkSubStatusId] = @subStatusId
                    AND [IsActive] = @isActive",
                    new {requestorId, statusId, subStatusId, isActive});
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Concession model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcession]
                            SET [fkTypeId] = @fkTypeId, [ConcessionRef] = @ConcessionRef, [fkLegalEntityId] = @fkLegalEntityId, [fkConcessionTypeId] = @fkConcessionTypeId, [SMTDealNumber] = @SMTDealNumber, [fkStatusId] = @fkStatusId, [fkSubStatusId] = @fkSubStatusId, [ConcessionDate] = @ConcessionDate, [DatesentForApproval] = @DatesentForApproval, [Motivation] = @Motivation, [DateApproved] = @DateApproved, [fkRequestorId] = @fkRequestorId, [fkBCMUserId] = @fkBCMUserId, [DateActionedByBCM] = @DateActionedByBCM, [fkPCMUserId] = @fkPCMUserId, [DateActionedByPCM] = @DateActionedByPCM, [fkHOUserId] = @fkHOUserId, [DateActionedByHO] = @DateActionedByHO, [ExpiryDate] = @ExpiryDate, [CentreId] = @CentreId, [IsCurrent] = @IsCurrent, [IsActive] = @IsActive
                            WHERE [pkConcessionId] = @Id",
                    new {Id = model.Id, fkTypeId = model.TypeId, ConcessionRef = model.ConcessionRef, fkLegalEntityId = model.LegalEntityId, fkConcessionTypeId = model.ConcessionTypeId, SMTDealNumber = model.SMTDealNumber, fkStatusId = model.StatusId, fkSubStatusId = model.SubStatusId, ConcessionDate = model.ConcessionDate, DatesentForApproval = model.DatesentForApproval, Motivation = model.Motivation, DateApproved = model.DateApproved, fkRequestorId = model.RequestorId, fkBCMUserId = model.BCMUserId, DateActionedByBCM = model.DateActionedByBCM, fkPCMUserId = model.PCMUserId, DateActionedByPCM = model.DateActionedByPCM, fkHOUserId = model.HOUserId, DateActionedByHO = model.DateActionedByHO, ExpiryDate = model.ExpiryDate, CentreId = model.CentreId, IsCurrent = model.IsCurrent, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Concession model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcession] WHERE [pkConcessionId] = @Id",
                    new {model.Id});
            }
        }
    }
}
