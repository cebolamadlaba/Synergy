using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// CentreUser repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ICentreUserRepository" />
    public class CentreUserRepository : ICentreUserRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreUserRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public CentreUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public CentreUser Create(CentreUser model)
        {
            const string sql = @"INSERT [dbo].[tblCentreUser] ([fkCentreId], [fkUserId], [IsActive]) 
                                VALUES (@CentreId, @UserId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new {CentreId = model.CentreId, UserId = model.UserId, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public CentreUser ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CentreUser>(
                    @"SELECT [pkCentreUserId] [Id], 
                             [fkCentreId] [CentreId], 
                             [fkUserId] [UserId], 
                             [IsActive] 
                    FROM [dbo].[tblCentreUser] 
                    WHERE [pkCentreUserId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<CentreUser> ReadByUserId(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CentreUser>(
                    @"SELECT [pkCentreUserId] [Id], 
                             [fkCentreId] [CentreId], 
                             [fkUserId] [UserId], 
                             [IsActive] 
                    FROM [dbo].[tblCentreUser] 
                    WHERE [fkUserId] = @userId",
                    new { userId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CentreUser> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CentreUser>(
                    @"SELECT [pkCentreUserId] [Id], 
                             [fkCentreId] [CentreId], 
                             [fkUserId] [UserId], 
                             [IsActive] 
                    FROM [dbo].[tblCentreUser]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(CentreUser model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblCentreUser]
                    SET [fkCentreId] = @CentreId, 
                        [fkUserId] = @UserId, 
                        [IsActive] = @IsActive
                    WHERE [pkCentreUserId] = @Id",
                    new {Id = model.Id, CentreId = model.CentreId, UserId = model.UserId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(CentreUser model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblCentreUser] 
                            WHERE [pkCentreUserId] = @Id",
                    new {model.Id});
            }
        }
    }
}
