namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Pricing manager interface
    /// </summary>
    public interface IPricingManager
    {
        /// <summary>
        /// Gets the risk group name for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        string GetRiskGroupName(int riskGroupNumber);
    }
}
