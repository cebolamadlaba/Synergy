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
    /// CentreBusinessManager repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ICentreBusinessManagerRepository" />
    public class CentreBusinessManagerRepository : ICentreBusinessManagerRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreBusinessManagerRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public CentreBusinessManagerRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public CentreBusinessManager Create(CentreBusinessManager model)
        {
            const string sql = @"INSERT [dbo].[tblCentreBusinessManager] ([fkCentreId], [fkUserId], [IsActive]) 
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
        public CentreBusinessManager ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<CentreBusinessManager>(
                    "SELECT [pkCentreBusinessManagerId] [Id], [fkCentreId] [CentreId], [fkUserId] [UserId], [IsActive] FROM [dbo].[tblCentreBusinessManager] WHERE [pkCentreBusinessManagerId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CentreBusinessManager> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<CentreBusinessManager>("SELECT [pkCentreBusinessManagerId] [Id], [fkCentreId] [CentreId], [fkUserId] [UserId], [IsActive] FROM [dbo].[tblCentreBusinessManager]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(CentreBusinessManager model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblCentreBusinessManager]
                            SET [fkCentreId] = @fkCentreId, [fkUserId] = @fkUserId, [IsActive] = @IsActive
                            WHERE [pkCentreBusinessManagerId] = @Id",
                    new {Id = model.Id, fkCentreId = model.CentreId, fkUserId = model.UserId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(CentreBusinessManager model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblCentreBusinessManager] WHERE [pkCentreBusinessManagerId] = @Id",
                    new {model.Id});
            }
        }
    }
}
