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
    /// ConcessionTransactional repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionTransactionalRepository" />
    public class ConcessionTransactionalRepository : IConcessionTransactionalRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionTransactionalRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionTransactionalRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionTransactional Create(ConcessionTransactional model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionTransactional] ([fkConcessionId], [fkTransactionTypeId], [fkChannelTypeId], [TableNumber], [TransactionVolume], [TransactionValue], [fkBaseRateId], [AdValorem]) 
                                VALUES (@fkConcessionId, @fkTransactionTypeId, @fkChannelTypeId, @TableNumber, @TransactionVolume, @TransactionValue, @fkBaseRateId, @AdValorem) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkTransactionTypeId = model.TransactionTypeId, fkChannelTypeId = model.ChannelTypeId, TableNumber = model.TableNumber, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, fkBaseRateId = model.BaseRateId, AdValorem = model.AdValorem}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionTransactional ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionTransactional>(
                    "SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [TableNumber], [TransactionVolume], [TransactionValue], [fkBaseRateId] [BaseRateId], [AdValorem] FROM [dbo].[tblConcessionTransactional] WHERE [pkConcessionTransactionalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionTransactional> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionTransactional>("SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [TableNumber], [TransactionVolume], [TransactionValue], [fkBaseRateId] [BaseRateId], [AdValorem] FROM [dbo].[tblConcessionTransactional]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionTransactional model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionTransactional]
                            SET [fkConcessionId] = @fkConcessionId, [fkTransactionTypeId] = @fkTransactionTypeId, [fkChannelTypeId] = @fkChannelTypeId, [TableNumber] = @TableNumber, [TransactionVolume] = @TransactionVolume, [TransactionValue] = @TransactionValue, [fkBaseRateId] = @fkBaseRateId, [AdValorem] = @AdValorem
                            WHERE [pkConcessionTransactionalId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkTransactionTypeId = model.TransactionTypeId, fkChannelTypeId = model.ChannelTypeId, TableNumber = model.TableNumber, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, fkBaseRateId = model.BaseRateId, AdValorem = model.AdValorem});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionTransactional model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionTransactional] WHERE [pkConcessionTransactionalId] = @Id",
                    new {model.Id});
            }
        }
    }
}
