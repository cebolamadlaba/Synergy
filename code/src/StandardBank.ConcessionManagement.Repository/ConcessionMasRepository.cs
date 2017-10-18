using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionMas repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionMasRepository" />
    public class ConcessionMasRepository : IConcessionMasRepository
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
        /// Initializes a new instance of the <see cref="ConcessionMasRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public ConcessionMasRepository(IDbConnectionFactory dbConnectionFactory,
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
        public ConcessionMas Create(ConcessionMas model)
        {
            var concessionDetail = _concessionDetailRepository.Create(model);
            model.ConcessionDetailId = concessionDetail.ConcessionDetailId;

            const string sql =
                @"INSERT [dbo].[tblConcessionMas] ([fkConcessionId], [fkConcessionDetailId], [fkTransactionTypeId], [MerchantNumber], [Turnover], [CommissionRate]) 
                VALUES (@ConcessionId, @ConcessionDetailId, @TransactionTypeId, @MerchantNumber, @Turnover, @CommissionRate) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        TransactionTypeId = model.TransactionTypeId,
                        MerchantNumber = model.MerchantNumber,
                        Turnover = model.Turnover,
                        CommissionRate = model.CommissionRate
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionMas ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionMas>(
                    @"SELECT [pkConcessionMasId] [Id], t.[fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [MerchantNumber], [Turnover], [CommissionRate], d.[fkLegalEntityId] [LegalEntityId], d.[fkLegalEntityAccountId] [LegalEntityAccountId], d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionMas] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE [pkConcessionMasId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionMas> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionMas>(
                    @"SELECT [pkConcessionMasId] [Id], t.[fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [MerchantNumber], [Turnover], [CommissionRate], d.[fkLegalEntityId] [LegalEntityId], d.[fkLegalEntityAccountId] [LegalEntityAccountId], d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionMas] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionMas model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionMas]
                            SET [fkConcessionId] = @ConcessionId, [fkConcessionDetailId] = @ConcessionDetailId, [fkTransactionTypeId] = @TransactionTypeId, [MerchantNumber] = @MerchantNumber, [Turnover] = @Turnover, [CommissionRate] = @CommissionRate
                            WHERE [pkConcessionMasId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        TransactionTypeId = model.TransactionTypeId,
                        MerchantNumber = model.MerchantNumber,
                        Turnover = model.Turnover,
                        CommissionRate = model.CommissionRate
                    });
            }

            _concessionDetailRepository.Update(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionMas model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionMas] WHERE [pkConcessionMasId] = @Id",
                    new {model.Id});
            }

            _concessionDetailRepository.Delete(model);
        }
    }
}