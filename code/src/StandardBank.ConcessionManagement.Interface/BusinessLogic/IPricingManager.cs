using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Pricing manager interface
    /// </summary>
    public interface IPricingManager
    {
        /// <summary>
        /// Gets the legal entities for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        IEnumerable<LegalEntity> GetLegalEntitiesForRiskGroupNumber(int riskGroupNumber);
    }
}
