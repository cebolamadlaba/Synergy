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
    /// ConcessionComment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionCommentRepository" />
    public class ConcessionCommentRepository : IConcessionCommentRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionCommentRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionCommentRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionComment Create(ConcessionComment model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionComment] ([fkConcessionId], [fkUserId], [fkConcessionSubStatusId], [Comment], [SystemDate], [IsActive]) 
                                VALUES (@fkConcessionId, @fkUserId, @fkConcessionSubStatusId, @Comment, @SystemDate, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkUserId = model.UserId, fkConcessionSubStatusId = model.ConcessionSubStatusId, Comment = model.Comment, SystemDate = model.SystemDate, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionComment ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionComment>(
                    "SELECT [pkConcessionCommentId] [Id], [fkConcessionId] [ConcessionId], [fkUserId] [UserId], [fkConcessionSubStatusId] [ConcessionSubStatusId], [Comment], [SystemDate], [IsActive] FROM [dbo].[tblConcessionComment] WHERE [pkConcessionCommentId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionComment> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionComment>("SELECT [pkConcessionCommentId] [Id], [fkConcessionId] [ConcessionId], [fkUserId] [UserId], [fkConcessionSubStatusId] [ConcessionSubStatusId], [Comment], [SystemDate], [IsActive] FROM [dbo].[tblConcessionComment]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionComment model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionComment]
                            SET [fkConcessionId] = @fkConcessionId, [fkUserId] = @fkUserId, [fkConcessionSubStatusId] = @fkConcessionSubStatusId, [Comment] = @Comment, [SystemDate] = @SystemDate, [IsActive] = @IsActive
                            WHERE [pkConcessionCommentId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkUserId = model.UserId, fkConcessionSubStatusId = model.ConcessionSubStatusId, Comment = model.Comment, SystemDate = model.SystemDate, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionComment model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionComment] WHERE [pkConcessionCommentId] = @Id",
                    new {model.Id});
            }
        }
    }
}
