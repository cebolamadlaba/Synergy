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
    /// UserRole repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IUserRoleRepository" />
    public class UserRoleRepository : IUserRoleRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public UserRoleRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public UserRole Create(UserRole model)
        {
            const string sql = @"INSERT [dbo].[tblUserRole] ([fkUserId], [fkRoleId], [IsActive]) 
                                VALUES (@fkUserId, @fkRoleId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkUserId = model.UserId, fkRoleId = model.RoleId, IsActive = model.IsActive}).Single();
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<UserRole>(
                    "SELECT [pkUserRoleId] [Id], [fkUserId] [UserId], [fkRoleId] [RoleId], [IsActive] FROM [dbo].[tblUserRole] WHERE [pkUserRoleId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserRole> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<UserRole>("SELECT [pkUserRoleId] [Id], [fkUserId] [UserId], [fkRoleId] [RoleId], [IsActive] FROM [dbo].[tblUserRole]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(UserRole model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblUserRole]
                            SET [fkUserId] = @fkUserId, [fkRoleId] = @fkRoleId, [IsActive] = @IsActive
                            WHERE [pkUserRoleId] = @Id",
                    new {Id = model.Id, fkUserId = model.UserId, fkRoleId = model.RoleId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(UserRole model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblUserRole] WHERE [pkUserRoleId] = @Id",
                    new {model.Id});
            }
        }
    }
}
