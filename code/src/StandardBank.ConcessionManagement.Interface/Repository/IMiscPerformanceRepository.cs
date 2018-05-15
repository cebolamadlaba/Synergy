using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;

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
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, int? userId);

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

        IEnumerable<BolProduct> GetBolProducts(int riskGroupId, string riskGroupName);

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        IEnumerable<TransactionalProduct> GetTransactionalProducts(int riskGroupId, string riskGroupName);

        /// <summary>
        /// Gets the cash concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<CashConcessionDetail> GetCashConcessionDetails(int concessionId);

        IEnumerable<BolConcessionDetail> GetBolConcessionDetails(int concessionId);

        /// <summary>
        /// Gets the lending concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<LendingConcessionDetail> GetLendingConcessionDetails(int concessionId);

        /// <summary>
        /// Gets the transactional concession details.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        IEnumerable<TransactionalConcessionDetail> GetTransactionalConcessionDetails(int concessionId);

        /// <summary>
        /// Gets the business centre management models.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessCentreManagementModel> GetBusinessCentreManagementModels();

         BusinessCentreManagementModel GetBusinessCentreManager(int pkCentreId);
    }
}