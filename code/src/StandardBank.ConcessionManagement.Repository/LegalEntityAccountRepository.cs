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
    /// LegalEntityAccount repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ILegalEntityAccountRepository" />
    public class LegalEntityAccountRepository : ILegalEntityAccountRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="LegalEntityAccountRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public LegalEntityAccountRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public LegalEntityAccount Create(LegalEntityAccount model)
        {
            const string sql = @"INSERT [dbo].[tblLegalEntityAccount] ([fkLegalEntityId], [AccountNumber], [IsActive]) 
                                VALUES (@fkLegalEntityId, @AccountNumber, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkLegalEntityId = model.LegalEntityId, AccountNumber = model.AccountNumber, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LegalEntityAccount ReadById(int id)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<LegalEntityAccount>(
                    "SELECT [pkLegalEntityAccountId] [Id], [fkLegalEntityId] [LegalEntityId], [AccountNumber], [IsActive] FROM [dbo].[tblLegalEntityAccount] WHERE [pkLegalEntityAccountId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LegalEntityAccount> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<LegalEntityAccount>("SELECT [pkLegalEntityAccountId] [Id], [fkLegalEntityId] [LegalEntityId], [AccountNumber], [IsActive] FROM [dbo].[tblLegalEntityAccount]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(LegalEntityAccount model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblLegalEntityAccount]
                            SET [fkLegalEntityId] = @fkLegalEntityId, [AccountNumber] = @AccountNumber, [IsActive] = @IsActive
                            WHERE [pkLegalEntityAccountId] = @Id",
                    new {Id = model.Id, fkLegalEntityId = model.LegalEntityId, AccountNumber = model.AccountNumber, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(LegalEntityAccount model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblLegalEntityAccount] WHERE [pkLegalEntityAccountId] = @Id",
                    new {model.Id});
            }
        }
    }
}
