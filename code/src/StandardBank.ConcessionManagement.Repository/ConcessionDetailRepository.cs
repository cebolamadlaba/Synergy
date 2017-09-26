using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionDetail repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionDetailRepository" />
    public class ConcessionDetailRepository : IConcessionDetailRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionDetailRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionDetailRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionDetail Create(ConcessionDetail model)
        {
            const string sql =
                @"INSERT [dbo].[tblConcessionDetail] ([fkConcessionId], [fkLegalEntityId], [fkLegalEntityAccountId], [ExpiryDate]) 
                                VALUES (@ConcessionId, @LegalEntityId, @LegalEntityAccountId, @ExpiryDate) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.ConcessionDetailId = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        ExpiryDate = model.ExpiryDate
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionDetail ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionDetail>(
                    "SELECT [pkConcessionDetailId] [ConcessionDetailId], [fkConcessionId] [ConcessionId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [ExpiryDate] FROM [dbo].[tblConcessionDetail] WHERE [pkConcessionDetailId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionDetail> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionDetail>(
                    "SELECT [pkConcessionDetailId] [ConcessionDetailId], [fkConcessionId] [ConcessionId], [fkLegalEntityId] [LegalEntityId], [fkLegalEntityAccountId] [LegalEntityAccountId], [ExpiryDate] FROM [dbo].[tblConcessionDetail]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionDetail model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionDetail]
                            SET [fkConcessionId] = @ConcessionId, [fkLegalEntityId] = @LegalEntityId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [ExpiryDate] = @ExpiryDate
                            WHERE [pkConcessionDetailId] = @ConcessionDetailId",
                    new
                    {
                        ConcessionDetailId = model.ConcessionDetailId,
                        ConcessionId = model.ConcessionId,
                        LegalEntityId = model.LegalEntityId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        ExpiryDate = model.ExpiryDate
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionDetail model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionDetail] WHERE [pkConcessionDetailId] = @ConcessionDetailId",
                    new {model.ConcessionDetailId});
            }
        }
    }
}
