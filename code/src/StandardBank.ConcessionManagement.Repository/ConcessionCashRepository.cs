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
    /// ConcessionCash repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionCashRepository" />
    public class ConcessionCashRepository : IConcessionCashRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionCashRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionCashRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionCash Create(ConcessionCash model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionCash] ([fkConcessionId], [fkChannelTypeId], [TableNumber], [CashVolume], [CashValue], [fkBaseRateId], [AdValorem]) 
                                VALUES (@fkConcessionId, @fkChannelTypeId, @TableNumber, @CashVolume, @CashValue, @fkBaseRateId, @AdValorem) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkChannelTypeId = model.ChannelTypeId, TableNumber = model.TableNumber, CashVolume = model.CashVolume, CashValue = model.CashValue, fkBaseRateId = model.BaseRateId, AdValorem = model.AdValorem}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionCash ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionCash>(
                    "SELECT [pkConcessionCashId] [Id], [fkConcessionId] [ConcessionId], [fkChannelTypeId] [ChannelTypeId], [TableNumber], [CashVolume], [CashValue], [fkBaseRateId] [BaseRateId], [AdValorem] FROM [dbo].[tblConcessionCash] WHERE [pkConcessionCashId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionCash> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionCash>("SELECT [pkConcessionCashId] [Id], [fkConcessionId] [ConcessionId], [fkChannelTypeId] [ChannelTypeId], [TableNumber], [CashVolume], [CashValue], [fkBaseRateId] [BaseRateId], [AdValorem] FROM [dbo].[tblConcessionCash]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionCash model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionCash]
                            SET [fkConcessionId] = @fkConcessionId, [fkChannelTypeId] = @fkChannelTypeId, [TableNumber] = @TableNumber, [CashVolume] = @CashVolume, [CashValue] = @CashValue, [fkBaseRateId] = @fkBaseRateId, [AdValorem] = @AdValorem
                            WHERE [pkConcessionCashId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkChannelTypeId = model.ChannelTypeId, TableNumber = model.TableNumber, CashVolume = model.CashVolume, CashValue = model.CashValue, fkBaseRateId = model.BaseRateId, AdValorem = model.AdValorem});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionCash model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionCash] WHERE [pkConcessionCashId] = @Id",
                    new {model.Id});
            }
        }
    }
}
