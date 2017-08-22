using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;

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
    }
}
