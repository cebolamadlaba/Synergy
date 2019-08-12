using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// RiskGroupNonUniversalChargeCode repository interface
    /// </summary>
    public interface IRiskGroupNonUniversalChargeCodeRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="RiskGroupId">The RiskGroupId.</param>
        /// /// <param name="ChargeCodeId">The ChargeCodeId.</param>
        /// <returns></returns>
        void Create(int RiskGroupId, int ChargeCodeId);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="chargeCodeId">The identifier.</param>
        /// <returns></returns>
        IEnumerable<RiskGroupNonUniversalChargeCode> ReadByChargeCodeId(int chargeCodeId);


        /// <summary>
        ///Delete the by identifier.
        /// </summary>
        /// <param name="chargeCodeId">The identifier.</param>
        /// <returns></returns>
        void Delete(int chargeCodeId);

    }
}