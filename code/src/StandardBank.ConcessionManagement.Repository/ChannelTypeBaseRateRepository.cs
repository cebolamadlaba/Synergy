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
    /// ChannelTypeBaseRate repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IChannelTypeBaseRateRepository" />
    public class ChannelTypeBaseRateRepository : IChannelTypeBaseRateRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelTypeBaseRateRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ChannelTypeBaseRateRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ChannelTypeBaseRate Create(ChannelTypeBaseRate model)
        {
            const string sql = @"INSERT [dbo].[tblChannelTypeBaseRate] ([fkChannelTypeId], [fkBaseRateId]) 
                                VALUES (@fkChannelTypeId, @fkBaseRateId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkChannelTypeId = model.ChannelTypeId, fkBaseRateId = model.BaseRateId}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ChannelTypeBaseRate ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ChannelTypeBaseRate>(
                    "SELECT [pkChannelTypeBaseRateId] [Id], [fkChannelTypeId] [ChannelTypeId], [fkBaseRateId] [BaseRateId] FROM [dbo].[tblChannelTypeBaseRate] WHERE [pkChannelTypeBaseRateId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChannelTypeBaseRate> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ChannelTypeBaseRate>("SELECT [pkChannelTypeBaseRateId] [Id], [fkChannelTypeId] [ChannelTypeId], [fkBaseRateId] [BaseRateId] FROM [dbo].[tblChannelTypeBaseRate]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ChannelTypeBaseRate model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblChannelTypeBaseRate]
                            SET [fkChannelTypeId] = @fkChannelTypeId, [fkBaseRateId] = @fkBaseRateId
                            WHERE [pkChannelTypeBaseRateId] = @Id",
                    new {Id = model.Id, fkChannelTypeId = model.ChannelTypeId, fkBaseRateId = model.BaseRateId});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ChannelTypeBaseRate model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblChannelTypeBaseRate] WHERE [pkChannelTypeBaseRateId] = @Id",
                    new {model.Id});
            }
        }
    }
}
