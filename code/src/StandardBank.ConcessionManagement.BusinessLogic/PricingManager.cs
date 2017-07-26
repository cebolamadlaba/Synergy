using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Pricing manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IPricingManager" />
    public class PricingManager : IPricingManager
    {
        /// <summary>
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingManager"/> class.
        /// </summary>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        public PricingManager(IRiskGroupRepository riskGroupRepository)
        {
            _riskGroupRepository = riskGroupRepository;
        }

        /// <summary>
        /// Gets the risk group name for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        public string GetRiskGroupName(int riskGroupNumber)
        {
            var riskGroup = _riskGroupRepository.ReadByRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null && riskGroup.IsActive)
                return riskGroup.RiskGroupName;

            return string.Empty;
        }
    }
}
