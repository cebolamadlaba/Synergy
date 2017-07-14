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
    /// BusinesOnlineTransactionType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IBusinesOnlineTransactionTypeRepository" />
    public class BusinesOnlineTransactionTypeRepository : IBusinesOnlineTransactionTypeRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinesOnlineTransactionTypeRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public BusinesOnlineTransactionTypeRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public BusinesOnlineTransactionType Create(BusinesOnlineTransactionType model)
        {
            const string sql = @"INSERT [dbo].[tblBusinesOnlineTransactionType] ([fkTransactionGroupId], [Description], [IsActive]) 
                                VALUES (@fkTransactionGroupId, @Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkTransactionGroupId = model.TransactionGroupId, Description = model.Description, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BusinesOnlineTransactionType ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<BusinesOnlineTransactionType>(
                    "SELECT [pkBusinesOnlineTransactionTypeId] [Id], [fkTransactionGroupId] [TransactionGroupId], [Description], [IsActive] FROM [dbo].[tblBusinesOnlineTransactionType] WHERE [pkBusinesOnlineTransactionTypeId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinesOnlineTransactionType> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<BusinesOnlineTransactionType>("SELECT [pkBusinesOnlineTransactionTypeId] [Id], [fkTransactionGroupId] [TransactionGroupId], [Description], [IsActive] FROM [dbo].[tblBusinesOnlineTransactionType]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(BusinesOnlineTransactionType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblBusinesOnlineTransactionType]
                            SET [fkTransactionGroupId] = @fkTransactionGroupId, [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkBusinesOnlineTransactionTypeId] = @Id",
                    new {Id = model.Id, fkTransactionGroupId = model.TransactionGroupId, Description = model.Description, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(BusinesOnlineTransactionType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblBusinesOnlineTransactionType] WHERE [pkBusinesOnlineTransactionTypeId] = @Id",
                    new {model.Id});
            }
        }
    }
}
