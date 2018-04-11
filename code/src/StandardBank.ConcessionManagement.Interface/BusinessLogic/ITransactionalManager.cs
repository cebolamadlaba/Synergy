using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Transactional manager interface
    /// </summary>
    public interface ITransactionalManager
    {
        /// <summary>
        /// Gets the transactional concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        TransactionalConcession GetTransactionalConcession(string concessionReferenceId, User user);

        /// <summary>
        /// Creates the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        ConcessionTransactional CreateConcessionTransactional(
            TransactionalConcessionDetail transactionalConcessionDetail, Concession concession);

        /// <summary>
        /// Updates the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        ConcessionTransactional UpdateConcessionTransactional(
            TransactionalConcessionDetail transactionalConcessionDetail, Concession concession);

        /// <summary>
        /// Gets the transactional view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        TransactionalView GetTransactionalViewData(int riskGroupNumber);

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        decimal GetLatestCrsOrMrs(int riskGroupNumber);

        /// <summary>
        /// Gets the transactional financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        TransactionalFinancial GetTransactionalFinancialForRiskGroupNumber(int riskGroupNumber);

        /// <summary>
        /// Deletes the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <returns></returns>
        ConcessionTransactional DeleteConcessionTransactional(TransactionalConcessionDetail transactionalConcessionDetail);

        Task ForwardTransactionalConcession(TransactionalConcession transactionalConcession, User user);
    }
}
