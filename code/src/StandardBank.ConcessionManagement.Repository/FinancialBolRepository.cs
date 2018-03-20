using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;


namespace StandardBank.ConcessionManagement.Repository
{

    public class FinancialBolRepository : IFinancialBolRepository
    {

        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
             

        public FinancialBolRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }


        public IEnumerable<FinancialBol> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialBol>(
                    @"SELECT [pkFinancialBolId] [Id], [fkRiskGroupId] [RiskGroupId], [TotalPayments], [TotalCollections], [TotalValueAdded] 
                    FROM [dbo].[tblFinancialBol] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }
    }
}
