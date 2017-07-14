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
    /// ScenarioManagerToolDeal repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IScenarioManagerToolDealRepository" />
    public class ScenarioManagerToolDealRepository : IScenarioManagerToolDealRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioManagerToolDealRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ScenarioManagerToolDealRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ScenarioManagerToolDeal Create(ScenarioManagerToolDeal model)
        {
            const string sql = @"INSERT [dbo].[tblScenarioManagerToolDeal] ([DealNumber], [IsActive]) 
                                VALUES (@DealNumber, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {DealNumber = model.DealNumber, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ScenarioManagerToolDeal ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ScenarioManagerToolDeal>(
                    "SELECT [pkScenarioManagerToolDealId] [Id], [DealNumber], [IsActive] FROM [dbo].[tblScenarioManagerToolDeal] WHERE [pkScenarioManagerToolDealId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScenarioManagerToolDeal> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ScenarioManagerToolDeal>("SELECT [pkScenarioManagerToolDealId] [Id], [DealNumber], [IsActive] FROM [dbo].[tblScenarioManagerToolDeal]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ScenarioManagerToolDeal model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblScenarioManagerToolDeal]
                            SET [DealNumber] = @DealNumber, [IsActive] = @IsActive
                            WHERE [pkScenarioManagerToolDealId] = @Id",
                    new {Id = model.Id, DealNumber = model.DealNumber, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ScenarioManagerToolDeal model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblScenarioManagerToolDeal] WHERE [pkScenarioManagerToolDealId] = @Id",
                    new {model.Id});
            }
        }
    }
}
