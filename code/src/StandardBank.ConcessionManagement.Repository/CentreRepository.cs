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
    /// Centre repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ICentreRepository" />
    public class CentreRepository : ICentreRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public CentreRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public Centre Create(Centre model)
        {
            const string sql = @"INSERT [dbo].[tblCentre] ([fkProvinceId], [CentreName], [IsActive]) 
                                VALUES (@fkProvinceId, @CentreName, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkProvinceId = model.ProvinceId, CentreName = model.CentreName, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Centre ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Centre>(
                    "SELECT [pkCentreId] [Id], [fkProvinceId] [ProvinceId], [CentreName], [IsActive] FROM [dbo].[tblCentre] WHERE [pkCentreId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Centre> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<Centre>("SELECT [pkCentreId] [Id], [fkProvinceId] [ProvinceId], [CentreName], [IsActive] FROM [dbo].[tblCentre]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Centre model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblCentre]
                            SET [fkProvinceId] = @fkProvinceId, [CentreName] = @CentreName, [IsActive] = @IsActive
                            WHERE [pkCentreId] = @Id",
                    new {Id = model.Id, fkProvinceId = model.ProvinceId, CentreName = model.CentreName, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(Centre model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblCentre] WHERE [pkCentreId] = @Id",
                    new {model.Id});
            }
        }
    }
}
