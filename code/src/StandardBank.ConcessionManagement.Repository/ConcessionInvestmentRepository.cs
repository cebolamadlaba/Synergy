using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionInvestment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionInvestmentRepository" />
    public class ConcessionInvestmentRepository : IConcessionInvestmentRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionInvestmentRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionInvestmentRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionInvestment Create(ConcessionInvestment model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionInvestment] ([fkConcessionId], [fkConcessionDetailId], [fkProductTypeId], [Balance], [Term], [InterestToCustomer]) 
                                VALUES (@ConcessionId, @ConcessionDetailId, @ProductTypeId, @Balance, @Term, @InterestToCustomer) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, ProductTypeId = model.ProductTypeId, Balance = model.Balance, Term = model.Term, InterestToCustomer = model.InterestToCustomer}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionInvestment ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInvestment>(
                    "SELECT [pkConcessionInvestmentId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkProductTypeId] [ProductTypeId], [Balance], [Term], [InterestToCustomer] FROM [dbo].[tblConcessionInvestment] WHERE [pkConcessionInvestmentId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionInvestment> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInvestment>("SELECT [pkConcessionInvestmentId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkProductTypeId] [ProductTypeId], [Balance], [Term], [InterestToCustomer] FROM [dbo].[tblConcessionInvestment]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionInvestment]
                            SET [fkConcessionId] = @ConcessionId, [fkConcessionDetailId] = @ConcessionDetailId, [fkProductTypeId] = @ProductTypeId, [Balance] = @Balance, [Term] = @Term, [InterestToCustomer] = @InterestToCustomer
                            WHERE [pkConcessionInvestmentId] = @Id",
                    new {Id = model.Id, ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, ProductTypeId = model.ProductTypeId, Balance = model.Balance, Term = model.Term, InterestToCustomer = model.InterestToCustomer});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionInvestment] WHERE [pkConcessionInvestmentId] = @Id",
                    new {model.Id});
            }
        }
    }
}
