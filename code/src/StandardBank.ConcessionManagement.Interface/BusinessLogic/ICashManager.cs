using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Cash manager interface
    /// </summary>
    public interface ICashManager
    {
        /// <summary>
        /// Gets the cash concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        IEnumerable<CashConcession> GetCashConcessionsForRiskGroupNumber(int riskGroupNumber);

        /// <summary>
        /// Creates the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        ConcessionCash CreateConcessionCash(CashConcessionDetail cashConcessionDetail, Concession concession);
    }
}
