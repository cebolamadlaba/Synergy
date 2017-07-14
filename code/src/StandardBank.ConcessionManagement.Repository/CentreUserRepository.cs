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
    /// CentreUser repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ICentreUserRepository" />
    public class CentreUserRepository : ICentreUserRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreUserRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public CentreUserRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public CentreUser Create(CentreUser model)
        {
            const string sql = @"INSERT [dbo].[tblCentreUser] ([fkCentreId], [fkUserId], [IsActive]) 
                                VALUES (@fkCentreId, @fkUserId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkCentreId = model.CentreId, fkUserId = model.UserId, IsActive = model.IsActive}).Single();
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<CentreUser>(
                    "SELECT [pkCentreUserId] [Id], [fkCentreId] [CentreId], [fkUserId] [UserId], [IsActive] FROM [dbo].[tblCentreUser] WHERE [pkCentreUserId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CentreUser> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<CentreUser>("SELECT [pkCentreUserId] [Id], [fkCentreId] [CentreId], [fkUserId] [UserId], [IsActive] FROM [dbo].[tblCentreUser]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(CentreUser model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblCentreUser]
                            SET [fkCentreId] = @fkCentreId, [fkUserId] = @fkUserId, [IsActive] = @IsActive
                            WHERE [pkCentreUserId] = @Id",
                    new {Id = model.Id, fkCentreId = model.CentreId, fkUserId = model.UserId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(CentreUser model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblCentreUser] WHERE [pkCentreUserId] = @Id",
                    new {model.Id});
            }
        }
    }
}
