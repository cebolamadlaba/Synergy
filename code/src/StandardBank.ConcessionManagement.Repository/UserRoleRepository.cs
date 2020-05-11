using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// UserRole repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IUserRoleRepository" />
    public class UserRoleRepository : IUserRoleRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public UserRoleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public UserRole Create(UserRole model)
        {
            const string sql = @"INSERT [dbo].[tblUserRole] ([fkUserId], [fkRoleId], [IsActive]) 
                                VALUES (@UserId, @RoleId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new {UserId = model.UserId, RoleId = model.RoleId, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserRole ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<UserRole>(
                    @"SELECT [pkUserRoleId] [Id], 
                        [fkUserId] [UserId], 
                        [fkRoleId] [RoleId], 
                        [IsActive] 
                    FROM [dbo].[tblUserRole] 
                    WHERE [pkUserRoleId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserRole> ReadByUserId(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<UserRole>(
                    @"SELECT [pkUserRoleId] [Id], 
                        [fkUserId] [UserId], 
                        [fkRoleId] [RoleId],
                        [fkSubRoleId] [SubRoleId], 
                        [IsActive] 
                    FROM [dbo].[tblUserRole] 
                    WHERE [fkUserId] = @userId",
                    new { userId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserRole> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<UserRole>(
                    @"SELECT [pkUserRoleId] [Id], 
                        [fkUserId] [UserId], 
                        [fkRoleId] [RoleId], 
                        [IsActive] 
                    FROM [dbo].[tblUserRole]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(UserRole model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblUserRole]
                    SET [fkUserId] = @UserId, 
                        [fkRoleId] = @RoleId, 
                        [IsActive] = @IsActive
                    WHERE [pkUserRoleId] = @Id",
                    new {Id = model.Id, UserId = model.UserId, RoleId = model.RoleId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(UserRole model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblUserRole] 
                            WHERE [pkUserRoleId] = @Id",
                    new {model.Id});
            }
        }
    }
}