using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

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
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingManager"/> class.
        /// </summary>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="mapper"></param>
        public PricingManager(IRiskGroupRepository riskGroupRepository, IMapper mapper)
        {
            _riskGroupRepository = riskGroupRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the risk group for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        public RiskGroup GetRiskGroupForRiskGroupNumber(int riskGroupNumber)
        {
            var riskGroup = _riskGroupRepository.ReadByRiskGroupNumberIsActive(riskGroupNumber, true);

            return riskGroup != null ? _mapper.Map<RiskGroup>(riskGroup) : null;
        }
    }
}
