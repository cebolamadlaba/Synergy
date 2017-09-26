using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionTransactional repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionTransactionalRepository" />
    public class ConcessionTransactionalRepository : IConcessionTransactionalRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionTransactionalRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionTransactionalRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionTransactional Create(ConcessionTransactional model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionTransactional] ([fkConcessionId], [fkConcessionDetailId], [fkTransactionTypeId], [fkTableNumberId], [fkApprovedTableNumberId], [fkLoadedTableNumberId], [AdValorem], [BaseRate]) 
                                VALUES (@ConcessionId, @ConcessionDetailId, @TransactionTypeId, @TableNumberId, @ApprovedTableNumberId, @LoadedTableNumberId, @AdValorem, @BaseRate) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, TransactionTypeId = model.TransactionTypeId, TableNumberId = model.TableNumberId, ApprovedTableNumberId = model.ApprovedTableNumberId, LoadedTableNumberId = model.LoadedTableNumberId, AdValorem = model.AdValorem, BaseRate = model.BaseRate}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionTransactional ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTransactional>(
                    "SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [fkTableNumberId] [TableNumberId], [fkApprovedTableNumberId] [ApprovedTableNumberId], [fkLoadedTableNumberId] [LoadedTableNumberId], [AdValorem], [BaseRate] FROM [dbo].[tblConcessionTransactional] WHERE [pkConcessionTransactionalId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads the by concession identifier.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionTransactional> ReadByConcessionId(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTransactional>(
                    @"SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [fkTableNumberId] [TableNumberId], [fkApprovedTableNumberId] [ApprovedTableNumberId], [fkLoadedTableNumberId] [LoadedTableNumberId], [AdValorem], [BaseRate] 
                    FROM [dbo].[tblConcessionTransactional] 
                    WHERE [fkConcessionId] = @concessionId",
                    new { concessionId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionTransactional> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTransactional>("SELECT [pkConcessionTransactionalId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkTransactionTypeId] [TransactionTypeId], [fkTableNumberId] [TableNumberId], [fkApprovedTableNumberId] [ApprovedTableNumberId], [fkLoadedTableNumberId] [LoadedTableNumberId], [AdValorem], [BaseRate] FROM [dbo].[tblConcessionTransactional]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionTransactional]
                            SET [fkConcessionId] = @ConcessionId, [fkConcessionDetailId] = @ConcessionDetailId, [fkTransactionTypeId] = @TransactionTypeId, [fkTableNumberId] = @TableNumberId, [fkApprovedTableNumberId] = @ApprovedTableNumberId, [fkLoadedTableNumberId] = @LoadedTableNumberId, [AdValorem] = @AdValorem, [BaseRate] = @BaseRate
                            WHERE [pkConcessionTransactionalId] = @Id",
                    new {Id = model.Id, ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, TransactionTypeId = model.TransactionTypeId, TableNumberId = model.TableNumberId, ApprovedTableNumberId = model.ApprovedTableNumberId, LoadedTableNumberId = model.LoadedTableNumberId, AdValorem = model.AdValorem, BaseRate = model.BaseRate});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionTransactional model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionTransactional] WHERE [pkConcessionTransactionalId] = @Id",
                    new {model.Id});
            }
        }
    }
}