using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
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
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinesOnlineTransactionTypeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public BusinesOnlineTransactionTypeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public BusinesOnlineTransactionType Create(BusinesOnlineTransactionType model)
        {
            const string sql = @"INSERT [dbo].[tblBusinesOnlineTransactionType] ([fkTransactionGroupId], [Description], [IsActive]) 
                                VALUES (@TransactionGroupId, @Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {TransactionGroupId = model.TransactionGroupId, Description = model.Description, IsActive = model.IsActive}).Single();
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
            using (var db = _dbConnectionFactory.Connection())
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
            using (var db = _dbConnectionFactory.Connection())
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
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblBusinesOnlineTransactionType]
                            SET [fkTransactionGroupId] = @TransactionGroupId, [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkBusinesOnlineTransactionTypeId] = @Id",
                    new {Id = model.Id, TransactionGroupId = model.TransactionGroupId, Description = model.Description, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(BusinesOnlineTransactionType model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblBusinesOnlineTransactionType] WHERE [pkBusinesOnlineTransactionTypeId] = @Id",
                    new {model.Id});
            }
        }
    }
}
