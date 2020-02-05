using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
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
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionAccountRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionAccountRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionAccount Create(ConcessionAccount model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionAccount] ([fkConcessionId], [AccountNumber], [IsActive]) 
                                VALUES (@ConcessionId, @AccountNumber, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        AccountNumber = model.AccountNumber,
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
        public ConcessionAccount ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionAccount>(
                    @"SELECT [pkConcessionAccountId] [Id], 
                             [fkConcessionId] [ConcessionId], 
                             [AccountNumber], 
                             [IsActive] 
                    FROM [dbo].[tblConcessionAccount] 
                    WHERE [pkConcessionAccountId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the concession id and is active flag
        /// </summary>
        /// <param name="concessionId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public ConcessionAccount ReadByConcessionIdIsActive(int concessionId, bool isActive)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionAccount>(
                    @"SELECT [pkConcessionAccountId] [Id], 
                             [fkConcessionId] [ConcessionId], 
                             [AccountNumber], 
                             [IsActive] 
                    FROM [dbo].[tblConcessionAccount] 
                    WHERE [fkConcessionId] = @concessionId
                        AND [IsActive] = @isActive",
                    new {concessionId, isActive}).FirstOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionAccount> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionAccount>(
                    @"SELECT [pkConcessionAccountId] [Id], 
                             [fkConcessionId] [ConcessionId], 
                             [AccountNumber], 
                             [IsActive] 
                    FROM [dbo].[tblConcessionAccount]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionAccount model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionAccount]
                    SET [fkConcessionId] = @ConcessionId, 
                        [AccountNumber] = @AccountNumber, 
                        [IsActive] = @IsActive
                    WHERE [pkConcessionAccountId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        AccountNumber = model.AccountNumber,
                        IsActive = model.IsActive
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionAccount model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblConcessionAccount] 
                            WHERE [pkConcessionAccountId] = @Id",
                    new {model.Id});
            }
        }
    }
}
