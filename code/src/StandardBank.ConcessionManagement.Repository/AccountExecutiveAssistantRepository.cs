using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// AccountExecutiveAssistant repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IAccountExecutiveAssistantRepository" />
    public class AccountExecutiveAssistantRepository : IAccountExecutiveAssistantRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountExecutiveAssistantRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public AccountExecutiveAssistantRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public AccountExecutiveAssistant Create(AccountExecutiveAssistant model)
        {
            const string sql =
                @"INSERT [dbo].[tblAccountExecutiveAssistant] ([fkAccountAssistantUserId], [fkAccountExecutiveUserId], [IsActive]) 
                VALUES (@AccountAssistantUserId, @AccountExecutiveUserId, @IsActive) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        AccountAssistantUserId = model.AccountAssistantUserId,
                        AccountExecutiveUserId = model.AccountExecutiveUserId,
                        IsActive = model.IsActive
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AccountExecutiveAssistant ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AccountExecutiveAssistant>(
                    "SELECT [pkAccountExecutiveAssistantId] [Id], [fkAccountAssistantUserId] [AccountAssistantUserId], [fkAccountExecutiveUserId] [AccountExecutiveUserId], [IsActive] FROM [dbo].[tblAccountExecutiveAssistant] WHERE [pkAccountExecutiveAssistantId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by account assistant user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AccountExecutiveAssistant ReadByAccountAssistantUserId(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AccountExecutiveAssistant>(
                    @"SELECT [pkAccountExecutiveAssistantId] [Id], [fkAccountAssistantUserId] [AccountAssistantUserId], [fkAccountExecutiveUserId] [AccountExecutiveUserId], [IsActive]
                    FROM [dbo].[tblAccountExecutiveAssistant] 
                    WHERE [fkAccountAssistantUserId] = @userId
                    AND [IsActive] = 1",
                    new { userId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Reads the by account executive user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<AccountExecutiveAssistant> ReadByAccountExecutiveUserId(int userId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AccountExecutiveAssistant>(
                    @"SELECT [pkAccountExecutiveAssistantId] [Id], [fkAccountAssistantUserId] [AccountAssistantUserId], [fkAccountExecutiveUserId] [AccountExecutiveUserId], [IsActive]
                    FROM [dbo].[tblAccountExecutiveAssistant] 
                    WHERE [fkAccountExecutiveUserId] = @userId
                    AND [IsActive] = 1",
                    new { userId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AccountExecutiveAssistant> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<AccountExecutiveAssistant>(
                    "SELECT [pkAccountExecutiveAssistantId] [Id], [fkAccountAssistantUserId] [AccountAssistantUserId], [fkAccountExecutiveUserId] [AccountExecutiveUserId], [IsActive] FROM [dbo].[tblAccountExecutiveAssistant]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(AccountExecutiveAssistant model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblAccountExecutiveAssistant]
                            SET [fkAccountAssistantUserId] = @AccountAssistantUserId, [fkAccountExecutiveUserId] = @AccountExecutiveUserId, [IsActive] = @IsActive
                            WHERE [pkAccountExecutiveAssistantId] = @Id",
                    new
                    {
                        Id = model.Id,
                        AccountAssistantUserId = model.AccountAssistantUserId,
                        AccountExecutiveUserId = model.AccountExecutiveUserId,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(AccountExecutiveAssistant model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblAccountExecutiveAssistant] WHERE [pkAccountExecutiveAssistantId] = @Id",
                    new {model.Id});
            }
        }
    }
}