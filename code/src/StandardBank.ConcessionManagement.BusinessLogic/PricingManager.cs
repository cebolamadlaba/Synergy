using System.Collections.Generic;
using System.Linq;
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
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingManager"/> class.
        /// </summary>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public PricingManager(IRiskGroupRepository riskGroupRepository, ILegalEntityRepository legalEntityRepository,
            ILookupTableManager lookupTableManager)
        {
            _riskGroupRepository = riskGroupRepository;
            _legalEntityRepository = legalEntityRepository;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the legal entity for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<LegalEntity> GetLegalEntitiesForRiskGroupNumber(int riskGroupNumber)
        {
            var legalEntities = new List<LegalEntity>();
            var riskGroup = _riskGroupRepository.ReadByRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null && riskGroup.IsActive)
            {
                var legalEntitiesForRiskGroup = _legalEntityRepository.ReadByRiskGroupId(riskGroup.Id);

                foreach (var legalEntityForRiskGroup in legalEntitiesForRiskGroup.Where(_ => _.IsActive))
                {
                    var marketSegmentDescription =
                        _lookupTableManager.GetMarketSegmentName(legalEntityForRiskGroup.MarketSegmentId);

                    legalEntities.Add(new LegalEntity
                    {
                        Id = legalEntityForRiskGroup.Id,
                        RiskGroupId = riskGroup.Id,
                        RiskGroupNumber = riskGroup.RiskGroupNumber,
                        MarketSegmentId = legalEntityForRiskGroup.MarketSegmentId,
                        CustomerName = legalEntityForRiskGroup.CustomerName,
                        RiskGroupName = riskGroup.RiskGroupName,
                        CustomerNumber = legalEntityForRiskGroup.CustomerNumber,
                        MarketSegmentDescription = marketSegmentDescription
                    });
                }
            }

            return legalEntities;
        }
    }
}
