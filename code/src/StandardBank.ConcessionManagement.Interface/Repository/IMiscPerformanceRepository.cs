using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Miscellaneous repository methods to help with performance
    /// </summary>
    public interface IMiscPerformanceRepository
    {
        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber);
    }
}
