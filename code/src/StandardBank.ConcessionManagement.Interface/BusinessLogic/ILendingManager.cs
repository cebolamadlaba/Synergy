using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Lending manager interface
    /// </summary>
    public interface ILendingManager
    {
        /// <summary>
        /// Creates a concession lending
        /// </summary>
        /// <param name="lendingConcessionDetail"></param>
        /// <param name="concession"></param>
        /// <returns></returns>
        Model.Repository.ConcessionLending CreateConcessionLending(LendingConcessionDetail lendingConcessionDetail, Concession concession);

        /// <summary>
        /// Gets the lending concession for the concession reference id
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        LendingConcession GetLendingConcession(string concessionReferenceId, User currentUser);

        /// <summary>
        /// Deletes the concession lending.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        Model.Repository.ConcessionLending DeleteConcessionLending(LendingConcessionDetail lendingConcessionDetail);

        /// <summary>
        /// Updates the concession lending.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        Model.Repository.ConcessionLending UpdateConcessionLending(LendingConcessionDetail lendingConcessionDetail, Concession concession);

        /// <summary>
        /// Gets the lending view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        LendingView GetLendingViewData(int riskGroupNumber, User currentUser);

        LendingView GetLendingViewDataBySAPBPID(int sapbpid, User currentUser);

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        decimal GetLatestCrsOrMrs(int riskGroupNumber);

        /// <summary>
        /// Gets the lending financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        LendingFinancial GetLendingFinancialForRiskGroupNumber(int riskGroupNumber);

        Task ForwardLendingConcession(LendingConcession lendingConcession, User user);

        #region Concession Lending Tiered Rate
        void CreateConcessionLendingTieredRates(IEnumerable<LendingConcessionDetailTieredRate> lendingConcessionDetailTieredRates);

        void CreateConcessionLendingTieredRates(IEnumerable<LendingConcessionDetail> lendingConcessionDetails);

        void DeleteConcessionLendingTieredRate(int concessionLendingTieredRateId);

        void DeleteConcessionLendingTieredRates(IEnumerable<StandardBank.ConcessionManagement.Model.Repository.ConcessionLendingTieredRate> concessionLendingTieredRates);
        decimal GetExtensionFee();
        #endregion
    }
}
