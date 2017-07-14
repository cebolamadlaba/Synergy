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
    /// ConcessionType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionTypeRepository" />
    public class ConcessionTypeRepository : IConcessionTypeRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionTypeRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionTypeRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionType Create(ConcessionType model)
        {
            const string sql = @"INSERT [dbo].[rtblConcessionType] ([Description], [Code], [IsActive]) 
                                VALUES (@Description, @Code, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {Description = model.Description, Code = model.Code, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionType ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionType>(
                    "SELECT [pkConcessionTypeId] [Id], [Description], [Code], [IsActive] FROM [dbo].[rtblConcessionType] WHERE [pkConcessionTypeId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionType> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionType>("SELECT [pkConcessionTypeId] [Id], [Description], [Code], [IsActive] FROM [dbo].[rtblConcessionType]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblConcessionType]
                            SET [Description] = @Description, [Code] = @Code, [IsActive] = @IsActive
                            WHERE [pkConcessionTypeId] = @Id",
                    new {Id = model.Id, Description = model.Description, Code = model.Code, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblConcessionType] WHERE [pkConcessionTypeId] = @Id",
                    new {model.Id});
            }
        }
    }
}
