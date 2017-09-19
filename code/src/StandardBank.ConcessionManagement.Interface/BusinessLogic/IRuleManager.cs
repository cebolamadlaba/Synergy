using System;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Rule manager interface
    /// </summary>
    public interface IRuleManager
    {
        /// <summary>
        /// Calculates the expiry date.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        DateTime CalculateExpiryDate(int concessionId, string concessionType);
    }
}
