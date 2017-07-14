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
    /// ConcessionBol repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionBolRepository" />
    public class ConcessionBolRepository : IConcessionBolRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionBolRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionBolRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionBol Create(ConcessionBol model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionBol] ([fkConcessionId], [fkTransactionGroupId], [fkBusinesOnlineTransactionTypeId], [BolUseId], [TransactionVolume], [TransactionValue], [Fee]) 
                                VALUES (@fkConcessionId, @fkTransactionGroupId, @fkBusinesOnlineTransactionTypeId, @BolUseId, @TransactionVolume, @TransactionValue, @Fee) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkTransactionGroupId = model.TransactionGroupId, fkBusinesOnlineTransactionTypeId = model.BusinesOnlineTransactionTypeId, BolUseId = model.BolUseId, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, Fee = model.Fee}).Single();
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionBol>(
                    "SELECT [pkConcessionBolId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionGroupId] [TransactionGroupId], [fkBusinesOnlineTransactionTypeId] [BusinesOnlineTransactionTypeId], [BolUseId], [TransactionVolume], [TransactionValue], [Fee] FROM [dbo].[tblConcessionBol] WHERE [pkConcessionBolId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionBol> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionBol>("SELECT [pkConcessionBolId] [Id], [fkConcessionId] [ConcessionId], [fkTransactionGroupId] [TransactionGroupId], [fkBusinesOnlineTransactionTypeId] [BusinesOnlineTransactionTypeId], [BolUseId], [TransactionVolume], [TransactionValue], [Fee] FROM [dbo].[tblConcessionBol]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionBol model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionBol]
                            SET [fkConcessionId] = @fkConcessionId, [fkTransactionGroupId] = @fkTransactionGroupId, [fkBusinesOnlineTransactionTypeId] = @fkBusinesOnlineTransactionTypeId, [BolUseId] = @BolUseId, [TransactionVolume] = @TransactionVolume, [TransactionValue] = @TransactionValue, [Fee] = @Fee
                            WHERE [pkConcessionBolId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkTransactionGroupId = model.TransactionGroupId, fkBusinesOnlineTransactionTypeId = model.BusinesOnlineTransactionTypeId, BolUseId = model.BolUseId, TransactionVolume = model.TransactionVolume, TransactionValue = model.TransactionValue, Fee = model.Fee});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionBol model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionBol] WHERE [pkConcessionBolId] = @Id",
                    new {model.Id});
            }
        }
    }
}
