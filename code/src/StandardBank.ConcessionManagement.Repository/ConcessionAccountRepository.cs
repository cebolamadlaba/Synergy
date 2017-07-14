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
    /// ConcessionAccount repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionAccountRepository" />
    public class ConcessionAccountRepository : IConcessionAccountRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionAccountRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionAccountRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionAccount Create(ConcessionAccount model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionAccount] ([fkConcessionId], [AccountNumber], [IsActive]) 
                                VALUES (@fkConcessionId, @AccountNumber, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, AccountNumber = model.AccountNumber, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionAccount ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionAccount>(
                    "SELECT [pkConcessionAccountId] [Id], [fkConcessionId] [ConcessionId], [AccountNumber], [IsActive] FROM [dbo].[tblConcessionAccount] WHERE [pkConcessionAccountId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionAccount> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionAccount>("SELECT [pkConcessionAccountId] [Id], [fkConcessionId] [ConcessionId], [AccountNumber], [IsActive] FROM [dbo].[tblConcessionAccount]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionAccount model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionAccount]
                            SET [fkConcessionId] = @fkConcessionId, [AccountNumber] = @AccountNumber, [IsActive] = @IsActive
                            WHERE [pkConcessionAccountId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, AccountNumber = model.AccountNumber, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionAccount model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionAccount] WHERE [pkConcessionAccountId] = @Id",
                    new {model.Id});
            }
        }
    }
}
