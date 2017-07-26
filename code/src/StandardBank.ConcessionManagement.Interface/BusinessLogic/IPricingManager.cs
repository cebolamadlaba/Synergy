using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Pricing manager interface
    /// </summary>
    public interface IPricingManager
    {
        /// <summary>
        /// Gets the risk group for the number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        RiskGroup GetRiskGroupForRiskGroupNumber(int riskGroupNumber);
    }
}
