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
    /// ConcessionApproval repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionApprovalRepository" />
    public class ConcessionApprovalRepository : IConcessionApprovalRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionApprovalRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionApprovalRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionApproval Create(ConcessionApproval model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionApproval] ([fkConcessionId], [fkOldSubStatusId], [fkNewSubStatusId], [fkUserId], [SystemDate], [IsActive]) 
                                VALUES (@fkConcessionId, @fkOldSubStatusId, @fkNewSubStatusId, @fkUserId, @SystemDate, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkOldSubStatusId = model.OldSubStatusId, fkNewSubStatusId = model.NewSubStatusId, fkUserId = model.UserId, SystemDate = model.SystemDate, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionApproval ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionApproval>(
                    "SELECT [pkConcessionApprovalId] [Id], [fkConcessionId] [ConcessionId], [fkOldSubStatusId] [OldSubStatusId], [fkNewSubStatusId] [NewSubStatusId], [fkUserId] [UserId], [SystemDate], [IsActive] FROM [dbo].[tblConcessionApproval] WHERE [pkConcessionApprovalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionApproval> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionApproval>("SELECT [pkConcessionApprovalId] [Id], [fkConcessionId] [ConcessionId], [fkOldSubStatusId] [OldSubStatusId], [fkNewSubStatusId] [NewSubStatusId], [fkUserId] [UserId], [SystemDate], [IsActive] FROM [dbo].[tblConcessionApproval]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionApproval model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionApproval]
                            SET [fkConcessionId] = @fkConcessionId, [fkOldSubStatusId] = @fkOldSubStatusId, [fkNewSubStatusId] = @fkNewSubStatusId, [fkUserId] = @fkUserId, [SystemDate] = @SystemDate, [IsActive] = @IsActive
                            WHERE [pkConcessionApprovalId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkOldSubStatusId = model.OldSubStatusId, fkNewSubStatusId = model.NewSubStatusId, fkUserId = model.UserId, SystemDate = model.SystemDate, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionApproval model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionApproval] WHERE [pkConcessionApprovalId] = @Id",
                    new {model.Id});
            }
        }
    }
}
