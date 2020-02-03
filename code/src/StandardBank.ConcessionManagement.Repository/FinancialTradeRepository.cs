using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;


namespace StandardBank.ConcessionManagement.Repository
{

    public class FinancialTradeRepository : IFinancialTradeRepository
    {

        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
             

        public FinancialTradeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }


        public IEnumerable<FinancialTrade> ReadByRiskGroupId(int riskGroupId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<FinancialTrade>(
                    @"SELECT [pkFinancialTradeId] [Id], 
                            [fkRiskGroupId] [RiskGroupId], 
                            [TotalAccounts], 
                            [AvgFee], 
                            [OverallForexMargin] 
                    FROM [dbo].[tblFinancialTrade] 
                    WHERE [fkRiskGroupId] = @riskGroupId",
                    new { riskGroupId });
            }
        }
    }
}
