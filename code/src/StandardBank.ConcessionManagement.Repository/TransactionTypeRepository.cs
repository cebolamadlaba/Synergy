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
    /// TransactionType repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ITransactionTypeRepository" />
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionTypeRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public TransactionTypeRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionType Create(TransactionType model)
        {
            const string sql = @"INSERT [dbo].[rtblTransactionType] ([fkConcessionTypeId], [Description], [IsActive]) 
                                VALUES (@fkConcessionTypeId, @Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionTypeId = model.ConcessionTypeId, Description = model.Description, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TransactionType ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<TransactionType>(
                    "SELECT [pkTransactionTypeId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [Description], [IsActive] FROM [dbo].[rtblTransactionType] WHERE [pkTransactionTypeId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TransactionType> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<TransactionType>("SELECT [pkTransactionTypeId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [Description], [IsActive] FROM [dbo].[rtblTransactionType]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(TransactionType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[rtblTransactionType]
                            SET [fkConcessionTypeId] = @fkConcessionTypeId, [Description] = @Description, [IsActive] = @IsActive
                            WHERE [pkTransactionTypeId] = @Id",
                    new {Id = model.Id, fkConcessionTypeId = model.ConcessionTypeId, Description = model.Description, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(TransactionType model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[rtblTransactionType] WHERE [pkTransactionTypeId] = @Id",
                    new {model.Id});
            }
        }
    }
}
