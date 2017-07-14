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
    /// BaseRate repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IBaseRateRepository" />
    public class BaseRateRepository : IBaseRateRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRateRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public BaseRateRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public BaseRate Create(BaseRate model)
        {
            const string sql = @"INSERT [dbo].[rtblBaseRate] ([BaseRate], [IsActive]) 
                                VALUES (@BaseRate, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {BaseRate = model.Amount, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BaseRate ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<BaseRate>(
                    "SELECT [pkBaseRateId] [Id], [BaseRate] [Amount], [IsActive] FROM [dbo].[rtblBaseRate] WHERE [pkBaseRateId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseRate> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<BaseRate>("SELECT [pkBaseRateId] [Id], [BaseRate] [Amount], [IsActive] FROM [dbo].[rtblBaseRate]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(BaseRate model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblBaseRate]
                            SET [BaseRate] = @BaseRate, [IsActive] = @IsActive
                            WHERE [pkBaseRateId] = @Id",
                    new {Id = model.Id, BaseRate = model.Amount, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(BaseRate model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblBaseRate] WHERE [pkBaseRateId] = @Id",
                    new {model.Id});
            }
        }
    }
}
