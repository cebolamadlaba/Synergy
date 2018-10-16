using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Cash manager interface
    /// </summary>
    public interface ICashManager
    {
        /// <summary>
        /// Creates the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        ConcessionCash CreateConcessionCash(CashConcessionDetail cashConcessionDetail, Concession concession);

        /// <summary>
        /// Gets the cash concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        CashConcession GetCashConcession(string concessionReferenceId, User user);

        /// <summary>
        /// Deletes the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <returns></returns>
        ConcessionCash DeleteConcessionCash(CashConcessionDetail cashConcessionDetail);

        /// <summary>
        /// Updates the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        ConcessionCash UpdateConcessionCash(CashConcessionDetail cashConcessionDetail, Concession concession);

        /// <summary>
        /// Gets the cash view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        CashView GetCashViewData(int riskGroupNumber);

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        decimal GetLatestCrsOrMrs(int riskGroupNumber);

        /// <summary>
        /// Gets the cash financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        CashFinancial GetCashFinancialForRiskGroupNumber(int riskGroupNumber);

        Task ForwardCashConcession(CashConcession cashConcession, User user);

        ChannelType CreateChannelType(ChannelType channelType);

        Model.Repository.TableNumber CreateupdateTableNumber(Model.UserInterface.TableNumber cashTableNumber);

    }
}
