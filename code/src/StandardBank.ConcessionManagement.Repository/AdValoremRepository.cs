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
    /// Ad valorem repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IAdValoremRepository" />
    public class AdValoremRepository : IAdValoremRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdValoremRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public AdValoremRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AdValorem Create(AdValorem model)
        {
            const string sql = @"INSERT [dbo].[rtblAdValorem] ([AdValorem], [IsActive]) 
                                VALUES (@AdValorem, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {AdValorem = model.Amount, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AdValorem ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<AdValorem>(
                    "SELECT [pkAdValoremId] [Id], [AdValorem] [Amount], [IsActive] FROM [dbo].[rtblAdValorem] WHERE [pkAdValoremId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AdValorem> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<AdValorem>("SELECT [pkAdValoremId] [Id], [AdValorem] [Amount], [IsActive] FROM [dbo].[rtblAdValorem]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(AdValorem model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblAdValorem]
                            SET [AdValorem] = @AdValorem,
                            [IsActive] = @IsActive
                            WHERE [pkAdValoremId] = @pkAdValoremId",
                    new {pkAdValoremId = model.Id, AdValorem = model.Amount, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(AdValorem model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblAdValorem] WHERE [pkAdValoremId] = @pkAdValoremId",
                    new {pkAdValoremId = model.Id});
            }
        }
    }
}
