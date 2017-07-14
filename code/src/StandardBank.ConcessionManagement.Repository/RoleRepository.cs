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
    /// Role repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IRoleRepository" />
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public RoleRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Role Create(Role model)
        {
            const string sql = @"INSERT [dbo].[rtblRole] ([RoleName], [RoleDescription], [IsActive]) 
                                VALUES (@RoleName, @RoleDescription, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {RoleName = model.RoleName, RoleDescription = model.RoleDescription, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Role ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Role>(
                    "SELECT [pkRoleId] [Id], [RoleName], [RoleDescription], [IsActive] FROM [dbo].[rtblRole] WHERE [pkRoleId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Role> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Role>("SELECT [pkRoleId] [Id], [RoleName], [RoleDescription], [IsActive] FROM [dbo].[rtblRole]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Role model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblRole]
                            SET [RoleName] = @RoleName, [RoleDescription] = @RoleDescription, [IsActive] = @IsActive
                            WHERE [pkRoleId] = @Id",
                    new {Id = model.Id, RoleName = model.RoleName, RoleDescription = model.RoleDescription, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Role model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblRole] WHERE [pkRoleId] = @Id",
                    new {model.Id});
            }
        }
    }
}
