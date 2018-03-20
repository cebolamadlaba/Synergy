using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// FinancialTransactional repository interface
    /// </summary>
    public interface IFinancialBolRepository
    {
      
        /// <summary>
        /// Reads by the risk group id
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        IEnumerable<FinancialBol> ReadByRiskGroupId(int riskGroupId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
      
    }
}