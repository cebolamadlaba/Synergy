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
        /// Initializes a new instance of the <see cref="ConcessionMasRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionMasRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionMas Create(ConcessionMas model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionMas] ([fkConcessionId], [fkTransactionTypeId], [MerchantNumber], [Turnover], [CommissionRate]) 
                                VALUES (@fkConcessionId, @fkTransactionTypeId, @MerchantNumber, @Turnover, @CommissionRate) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkTransactionTypeId = model.TransactionTypeId, MerchantNumber = model.MerchantNumber, Turnover = model.Turnover, CommissionRate = model.CommissionRate}).Single();
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
                    "SELECT [pkConcessionMasId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [MerchantNumber], [Turnover], [CommissionRate] FROM [dbo].[tblConcessionMas] WHERE [pkConcessionMasId] = @Id",
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
                return db.Query<ConcessionMas>("SELECT [pkConcessionMasId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionTypeId] [TransactionTypeId], [MerchantNumber], [Turnover], [CommissionRate] FROM [dbo].[tblConcessionMas]");
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
                            SET [fkConcessionId] = @fkConcessionId, [fkTransactionTypeId] = @fkTransactionTypeId, [MerchantNumber] = @MerchantNumber, [Turnover] = @Turnover, [CommissionRate] = @CommissionRate
                            WHERE [pkConcessionMasId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkTransactionTypeId = model.TransactionTypeId, MerchantNumber = model.MerchantNumber, Turnover = model.Turnover, CommissionRate = model.CommissionRate});
            }
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
        }
    }
}
