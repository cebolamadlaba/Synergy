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
    /// ConcessionInvestment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionInvestmentRepository" />
    public class ConcessionInvestmentRepository : IConcessionInvestmentRepository
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionInvestmentRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ConcessionInvestmentRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionInvestment Create(ConcessionInvestment model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionInvestment] ([fkConcessionId], [fkProductTypeId], [Balance], [Term], [InterestToCustomer]) 
                                VALUES (@fkConcessionId, @fkProductTypeId, @Balance, @Term, @InterestToCustomer) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                model.Id = db.Query<int>(sql, new {fkConcessionId = model.ConcessionId, fkProductTypeId = model.ProductTypeId, Balance = model.Balance, Term = model.Term, InterestToCustomer = model.InterestToCustomer}).Single();
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionInvestment>(
                    "SELECT [pkConcessionInvestmentId] [Id], [fkConcessionId] [ConcessionId], [fkProductTypeId] [ProductTypeId], [Balance], [Term], [InterestToCustomer] FROM [dbo].[tblConcessionInvestment] WHERE [pkConcessionInvestmentId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionInvestment> ReadAll()
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                return db.Query<ConcessionInvestment>("SELECT [pkConcessionInvestmentId] [Id], [fkConcessionId] [ConcessionId], [fkProductTypeId] [ProductTypeId], [Balance], [Term], [InterestToCustomer] FROM [dbo].[tblConcessionInvestment]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionInvestment model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute(@"UPDATE [dbo].[tblConcessionInvestment]
                            SET [fkConcessionId] = @fkConcessionId, [fkProductTypeId] = @fkProductTypeId, [Balance] = @Balance, [Term] = @Term, [InterestToCustomer] = @InterestToCustomer
                            WHERE [pkConcessionInvestmentId] = @Id",
                    new {Id = model.Id, fkConcessionId = model.ConcessionId, fkProductTypeId = model.ProductTypeId, Balance = model.Balance, Term = model.Term, InterestToCustomer = model.InterestToCustomer});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionInvestment model)
        {
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblConcessionInvestment] WHERE [pkConcessionInvestmentId] = @Id",
                    new {model.Id});
            }
        }
    }
}
