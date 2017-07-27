using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Lending manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ILendingManager" />
    public class LendingManager : ILendingManager
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        public LendingManager(IPricingManager pricingManager, ILegalEntityRepository legalEntityRepository, ILookupTableManager lookupTableManager)
        {
            _pricingManager = pricingManager;
            _legalEntityRepository = legalEntityRepository;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the lending concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<LendingConcession> GetLendingConcessionsForRiskGroupNumber(int riskGroupNumber)
        {
            var lendingConcessions = new List<LendingConcession>();
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
            var lendingConcessionTypeId = _lookupTableManager.GetConcessionTypeId("Lending");

            if (riskGroup != null)
            {
                var legalEntities = _legalEntityRepository.ReadByRiskGroupIdIsActive(riskGroup.Id, true);

                if (legalEntities != null && legalEntities.Any())
                {
                    foreach (var legalEntity in legalEntities)
                    {
                        //TODO: finish this
                    }
                }
            }

            return lendingConcessions;
        }
    }
}
