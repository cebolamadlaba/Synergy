using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
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
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionApprovalRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionApprovalRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionApproval Create(ConcessionApproval model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionApproval] ([fkConcessionId], [fkOldSubStatusId], [fkNewSubStatusId], [fkUserId], [SystemDate], [IsActive]) 
                                VALUES (@ConcessionId, @OldSubStatusId, @NewSubStatusId, @UserId, @SystemDate, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ConcessionId = model.ConcessionId, OldSubStatusId = model.OldSubStatusId, NewSubStatusId = model.NewSubStatusId, UserId = model.UserId, SystemDate = model.SystemDate, IsActive = model.IsActive}).Single();
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
            using (var db = _dbConnectionFactory.Connection())
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
            using (var db = _dbConnectionFactory.Connection())
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
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionApproval]
                            SET [fkConcessionId] = @ConcessionId, [fkOldSubStatusId] = @OldSubStatusId, [fkNewSubStatusId] = @NewSubStatusId, [fkUserId] = @UserId, [SystemDate] = @SystemDate, [IsActive] = @IsActive
                            WHERE [pkConcessionApprovalId] = @Id",
                    new {Id = model.Id, ConcessionId = model.ConcessionId, OldSubStatusId = model.OldSubStatusId, NewSubStatusId = model.NewSubStatusId, UserId = model.UserId, SystemDate = model.SystemDate, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionApproval model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionApproval] WHERE [pkConcessionApprovalId] = @Id",
                    new {model.Id});
            }
        }
    }
}
