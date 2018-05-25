using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// FinancialTransactional repository interface
    /// </summary>
    public interface IFinancialTradeRepository
    {
      
        /// <summary>
        /// Reads by the risk group id
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        IEnumerable<FinancialTrade> ReadByRiskGroupId(int riskGroupId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
      
    }
}