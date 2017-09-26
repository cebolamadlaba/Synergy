using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
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
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionCommentRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionCommentRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionComment Create(ConcessionComment model)
        {
            const string sql =
                @"INSERT [dbo].[tblConcessionComment] ([fkConcessionId], [fkUserId], [fkConcessionSubStatusId], [Comment], [SystemDate], [IsActive]) 
                                VALUES (@ConcessionId, @UserId, @ConcessionSubStatusId, @Comment, @SystemDate, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        UserId = model.UserId,
                        ConcessionSubStatusId = model.ConcessionSubStatusId,
                        Comment = model.Comment,
                        SystemDate = model.SystemDate,
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
        public ConcessionComment ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionComment>(
                    "SELECT [pkConcessionCommentId] [Id], [fkConcessionId] [ConcessionId], [fkUserId] [UserId], [fkConcessionSubStatusId] [ConcessionSubStatusId], [Comment], [SystemDate], [IsActive] FROM [dbo].[tblConcessionComment] WHERE [pkConcessionCommentId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by concession identifier.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionComment> ReadByConcessionId(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionComment>(
                    @"SELECT [pkConcessionCommentId] [Id], [fkConcessionId] [ConcessionId], [fkUserId] [UserId], [fkConcessionSubStatusId] [ConcessionSubStatusId], [Comment], [SystemDate], [IsActive] 
                    FROM [dbo].[tblConcessionComment] 
                    WHERE [fkConcessionId] = @concessionId",
                    new { concessionId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionComment> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionComment>(
                    "SELECT [pkConcessionCommentId] [Id], [fkConcessionId] [ConcessionId], [fkUserId] [UserId], [fkConcessionSubStatusId] [ConcessionSubStatusId], [Comment], [SystemDate], [IsActive] FROM [dbo].[tblConcessionComment]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionComment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionComment]
                            SET [fkConcessionId] = @ConcessionId, [fkUserId] = @UserId, [fkConcessionSubStatusId] = @ConcessionSubStatusId, [Comment] = @Comment, [SystemDate] = @SystemDate, [IsActive] = @IsActive
                            WHERE [pkConcessionCommentId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        UserId = model.UserId,
                        ConcessionSubStatusId = model.ConcessionSubStatusId,
                        Comment = model.Comment,
                        SystemDate = model.SystemDate,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionComment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionComment] WHERE [pkConcessionCommentId] = @Id",
                    new {model.Id});
            }
        }
    }
}