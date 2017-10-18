using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionBol repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionBolRepository" />
    public class ConcessionBolRepository : IConcessionBolRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The concession detail repository
        /// </summary>
        private readonly IConcessionDetailRepository _concessionDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionBolRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public ConcessionBolRepository(IDbConnectionFactory dbConnectionFactory,
            IConcessionDetailRepository concessionDetailRepository)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _concessionDetailRepository = concessionDetailRepository;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionBol Create(ConcessionBol model)
        {
            var concessionDetail = _concessionDetailRepository.Create(model);
            model.ConcessionDetailId = concessionDetail.ConcessionDetailId;

            const string sql =
                @"INSERT [dbo].[tblConcessionBol] ([fkConcessionId], [fkConcessionDetailId], [fkTransactionGroupId], [fkBusinesOnlineTransactionTypeId], [BolUseId], [TransactionVolume], [TransactionValue], [Fee]) 
                VALUES (@ConcessionId, @ConcessionDetailId, @TransactionGroupId, @BusinesOnlineTransactionTypeId, @BolUseId, @TransactionVolume, @TransactionValue, @Fee) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        TransactionGroupId = model.TransactionGroupId,
                        BusinesOnlineTransactionTypeId = model.BusinesOnlineTransactionTypeId,
                        BolUseId = model.BolUseId,
                        TransactionVolume = model.TransactionVolume,
                        TransactionValue = model.TransactionValue,
                        Fee = model.Fee
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionBol ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionBol>(
                    @"SELECT [pkConcessionBolId] [Id], t.[fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionGroupId] [TransactionGroupId], [fkBusinesOnlineTransactionTypeId] [BusinesOnlineTransactionTypeId], [BolUseId], [TransactionVolume], [TransactionValue], [Fee], d.[fkLegalEntityId] [LegalEntityId], d.[fkLegalEntityAccountId] [LegalEntityAccountId], d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionBol] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE [pkConcessionBolId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionBol> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionBol>(
                    @"SELECT [pkConcessionBolId] [Id], t.[fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionGroupId] [TransactionGroupId], [fkBusinesOnlineTransactionTypeId] [BusinesOnlineTransactionTypeId], [BolUseId], [TransactionVolume], [TransactionValue], [Fee], d.[fkLegalEntityId] [LegalEntityId], d.[fkLegalEntityAccountId] [LegalEntityAccountId], d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionBol] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionBol model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionBol]
                            SET [fkConcessionId] = @ConcessionId, [fkConcessionDetailId] = @ConcessionDetailId, [fkTransactionGroupId] = @TransactionGroupId, [fkBusinesOnlineTransactionTypeId] = @BusinesOnlineTransactionTypeId, [BolUseId] = @BolUseId, [TransactionVolume] = @TransactionVolume, [TransactionValue] = @TransactionValue, [Fee] = @Fee
                            WHERE [pkConcessionBolId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        TransactionGroupId = model.TransactionGroupId,
                        BusinesOnlineTransactionTypeId = model.BusinesOnlineTransactionTypeId,
                        BolUseId = model.BolUseId,
                        TransactionVolume = model.TransactionVolume,
                        TransactionValue = model.TransactionValue,
                        Fee = model.Fee
                    });
            }

            _concessionDetailRepository.Update(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionBol model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionBol] WHERE [pkConcessionBolId] = @Id",
                    new {model.Id});
            }

            _concessionDetailRepository.Delete(model);
        }
    }
}