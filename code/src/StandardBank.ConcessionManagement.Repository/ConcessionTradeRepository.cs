using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionTrade repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionTradeRepository" />
    public class ConcessionTradeRepository : IConcessionTradeRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionTradeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionTradeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionTrade Create(ConcessionTrade model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionTrade] ([fkConcessionId], [fkConcessionDetailId], [fkTransactionTypeId], [fkChannelTypeId], [fkBaseRateId], [TableNumber], [TransactionVolume], [TransactionValue], [AdValorem]) 
                                VALUES (@ConcessionId, @ConcessionDetailId, @TransactionTypeId, @ChannelTypeId, @BaseRateId, @TableNumber, @TransactionVolume, @TransactionValue, @AdValorem) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, TransactionTypeId = model.TransactionTypeId, ChannelTypeId = model.ChannelTypeId, BaseRateId = model.BaseRateId, TableNumber = model.TableNumber, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, AdValorem = model.AdValorem}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionTrade ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTrade>(
                    "SELECT [pkConcessionTradeId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [fkBaseRateId] [BaseRateId], [TableNumber], [TransactionVolume], [TransactionValue], [AdValorem] FROM [dbo].[tblConcessionTrade] WHERE [pkConcessionTradeId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionTrade> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTrade>("SELECT [pkConcessionTradeId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [fkBaseRateId] [BaseRateId], [TableNumber], [TransactionVolume], [TransactionValue], [AdValorem] FROM [dbo].[tblConcessionTrade]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionTrade model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionTrade]
                            SET [fkConcessionId] = @ConcessionId, [fkConcessionDetailId] = @ConcessionDetailId, [fkTransactionTypeId] = @TransactionTypeId, [fkChannelTypeId] = @ChannelTypeId, [fkBaseRateId] = @BaseRateId, [TableNumber] = @TableNumber, [TransactionVolume] = @TransactionVolume, [TransactionValue] = @TransactionValue, [AdValorem] = @AdValorem
                            WHERE [pkConcessionTradeId] = @Id",
                    new {Id = model.Id, ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, TransactionTypeId = model.TransactionTypeId, ChannelTypeId = model.ChannelTypeId, BaseRateId = model.BaseRateId, TableNumber = model.TableNumber, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, AdValorem = model.AdValorem});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionTrade model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionTrade] WHERE [pkConcessionTradeId] = @Id",
                    new {model.Id});
            }
        }
    }
}
