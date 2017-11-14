using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

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

        /// <summary>
        /// Gets the lending products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<LendingProduct> GetLendingProducts(int riskGroupId, string riskGroupName);

        /// <summary>
        /// Gets the cash products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<CashProduct> GetCashProducts(int riskGroupId, string riskGroupName);

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<TransactionalProduct> GetTransactionalProducts(int riskGroupId, string riskGroupName);
    }
}