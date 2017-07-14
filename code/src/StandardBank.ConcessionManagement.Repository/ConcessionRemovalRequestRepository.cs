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
    /// ConcessionRemovalRequest repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionRemovalRequestRepository" />
    public class ConcessionRemovalRequestRepository : IConcessionRemovalRequestRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionRemovalRequestRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionRemovalRequestRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionRemovalRequest Create(ConcessionRemovalRequest model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionRemovalRequest] ([fkConcessionId], [RequestorId], [fkBCMUserId], [fkPCMUserId], [fkHOUserId], [fkSubStatusId], [SystemDate], [DateApproved]) 
                                VALUES (@fkConcessionId, @RequestorId, @fkBCMUserId, @fkPCMUserId, @fkHOUserId, @fkSubStatusId, @SystemDate, @DateApproved) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, RequestorId = model.RequestorId, fkBCMUserId = model.BCMUserId, fkPCMUserId = model.PCMUserId, fkHOUserId = model.HOUserId, fkSubStatusId = model.SubStatusId, SystemDate = model.SystemDate, DateApproved = model.DateApproved}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionRemovalRequest ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionRemovalRequest>(
                    "SELECT [pkConcessionRemovalRequestId] [Id], [fkConcessionId] [ConcessionId], [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkSubStatusId] [SubStatusId], [SystemDate], [DateApproved] FROM [dbo].[tblConcessionRemovalRequest] WHERE [pkConcessionRemovalRequestId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionRemovalRequest> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionRemovalRequest>("SELECT [pkConcessionRemovalRequestId] [Id], [fkConcessionId] [ConcessionId], [RequestorId], [fkBCMUserId] [BCMUserId], [fkPCMUserId] [PCMUserId], [fkHOUserId] [HOUserId], [fkSubStatusId] [SubStatusId], [SystemDate], [DateApproved] FROM [dbo].[tblConcessionRemovalRequest]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionRemovalRequest model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionRemovalRequest]
                            SET [fkConcessionId] = @fkConcessionId, [RequestorId] = @RequestorId, [fkBCMUserId] = @fkBCMUserId, [fkPCMUserId] = @fkPCMUserId, [fkHOUserId] = @fkHOUserId, [fkSubStatusId] = @fkSubStatusId, [SystemDate] = @SystemDate, [DateApproved] = @DateApproved
                            WHERE [pkConcessionRemovalRequestId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, RequestorId = model.RequestorId, fkBCMUserId = model.BCMUserId, fkPCMUserId = model.PCMUserId, fkHOUserId = model.HOUserId, fkSubStatusId = model.SubStatusId, SystemDate = model.SystemDate, DateApproved = model.DateApproved});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionRemovalRequest model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionRemovalRequest] WHERE [pkConcessionRemovalRequestId] = @Id",
                    new {model.Id});
            }
        }
    }
}
