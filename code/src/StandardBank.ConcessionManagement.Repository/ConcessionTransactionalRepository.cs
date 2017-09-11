using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
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
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionTransactionalRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionTransactionalRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionTransactional Create(ConcessionTransactional model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionTransactional] ([fkConcessionId], [fkTransactionTypeId], [fkChannelTypeId], [TransactionVolume], [TransactionValue], [fkBaseRateId], [AdValorem], [fkLegalEntityId], [fkLegalEntityAccountId], [BaseRate], [fkTableNumberId]) 
                                VALUES (@ConcessionId, @TransactionTypeId, @ChannelTypeId, @TransactionVolume, @TransactionValue, @BaseRateId, @AdValorem, @LegalEntityId, @LegalEntityAccountId, @BaseRate, @TableNumberId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ConcessionId = model.ConcessionId, TransactionTypeId = model.TransactionTypeId, ChannelTypeId = model.ChannelTypeId, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, BaseRateId = model.BaseRateId, AdValorem = model.AdValorem, LegalEntityId = model.LegalEntityId, LegalEntityAccountId = model.LegalEntityAccountId, BaseRate = model.BaseRate, TableNumberId = model.TableNumberId}).Single();
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
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTransactional>(
                    "SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [TransactionVolume], [TransactionValue], [fkBaseRateId] [BaseRateId], [AdValorem], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [BaseRate], [fkTableNumberId] [TableNumberId] FROM [dbo].[tblConcessionTransactional] WHERE [pkConcessionTransactionalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by concession identifier.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionTransactional> ReadByConcessionId(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTransactional>(
                    "SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [TransactionVolume], [TransactionValue], [fkBaseRateId] [BaseRateId], [AdValorem], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [BaseRate], [fkTableNumberId] [TableNumberId] FROM [dbo].[tblConcessionTransactional] WHERE [fkConcessionId] = @concessionId",
                    new {concessionId});
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionTransactional> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTransactional>("SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [fkChannelTypeId] [ChannelTypeId], [TransactionVolume], [TransactionValue], [fkBaseRateId] [BaseRateId], [AdValorem], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [BaseRate], [fkTableNumberId] [TableNumberId] FROM [dbo].[tblConcessionTransactional]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionTransactional]
                            SET [fkConcessionId] = @ConcessionId, [fkTransactionTypeId] = @TransactionTypeId, [fkChannelTypeId] = @ChannelTypeId, [TransactionVolume] = @TransactionVolume, [TransactionValue] = @TransactionValue, [fkBaseRateId] = @BaseRateId, [AdValorem] = @AdValorem, [fkLegalEntityId] = @LegalEntityId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [BaseRate] = @BaseRate, [fkTableNumberId] = @TableNumberId
                            WHERE [pkConcessionTransactionalId] = @Id",
                    new {Id = model.Id, ConcessionId = model.ConcessionId, TransactionTypeId = model.TransactionTypeId, ChannelTypeId = model.ChannelTypeId, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, BaseRateId = model.BaseRateId, AdValorem = model.AdValorem, LegalEntityId = model.LegalEntityId, LegalEntityAccountId = model.LegalEntityAccountId, BaseRate = model.BaseRate, TableNumberId = model.TableNumberId});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionTransactional] WHERE [pkConcessionTransactionalId] = @Id",
                    new {model.Id});
            }
        }
    }
}
