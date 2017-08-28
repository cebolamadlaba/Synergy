using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Transactional manager interface
    /// </summary>
    public interface ITransactionalManager
    {
        /// <summary>
        /// Gets the transactional concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        IEnumerable<TransactionalConcession> GetTransactionalConcessionsForRiskGroupNumber(int riskGroupNumber);
    }
}
