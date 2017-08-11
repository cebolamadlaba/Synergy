﻿using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;

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
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The concession lending repository
        /// </summary>
        private readonly IConcessionLendingRepository _concessionLendingRepository;

        /// <summary>
        /// Intializes an instance of the class
        /// </summary>
        /// <param name="pricingManager"></param>
        /// <param name="concessionManager"></param>
        /// <param name="legalEntityRepository"></param>
        /// <param name="concessionLendingRepository"></param>
        public LendingManager(IPricingManager pricingManager, IConcessionManager concessionManager,
            ILegalEntityRepository legalEntityRepository, IConcessionLendingRepository concessionLendingRepository)
        {
            _pricingManager = pricingManager;
            _concessionManager = concessionManager;
            _legalEntityRepository = legalEntityRepository;
            _concessionLendingRepository = concessionLendingRepository;
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

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Lending");

                foreach (var concession in concessions)
                    AddLendingConcessionData(concession, lendingConcessions);
            }

            return lendingConcessions;
        }

        /// <summary>
        /// Adds the lending concession data
        /// </summary>
        /// <param name="concession"></param>
        /// <param name="lendingConcessions"></param>
        private void AddLendingConcessionData(Concession concession, ICollection<LendingConcession> lendingConcessions)
        {
            var lendingConcessionData = _concessionLendingRepository.ReadByConcessionId(concession.Id);

            if (lendingConcessionData != null)
            {
                var lendingConcessionDetails = new List<LendingConcessionDetail>();

                var lendingConcession =
                    lendingConcessions.FirstOrDefault(
                        _ => _.Concession.ReferenceNumber == concession.ReferenceNumber);

                if (lendingConcession == null)
                {
                    lendingConcession = new LendingConcession
                    {
                        Concession = concession,
                        LendingConcessionDetails = new List<LendingConcessionDetail>()
                    };

                    lendingConcessions.Add(lendingConcession);
                }

                lendingConcessionDetails.AddRange(lendingConcession.LendingConcessionDetails);

                var legalEntity = _legalEntityRepository.ReadById(lendingConcessionData.LegalEntityId);

                lendingConcessionDetails.Add(new LendingConcessionDetail
                {
                    CustomerName = legalEntity.CustomerName,
                    AccountNumber = concession.AccountNumber,
                    Limit = lendingConcessionData?.Limit ?? 0,
                    Term = lendingConcessionData?.Term ?? 0,
                    LoadedMap = lendingConcessionData?.MarginToPrime ?? 0,
                    ApprovedMap = lendingConcessionData?.ApprovedMarginToPrime ?? 0
                });

                lendingConcession.LendingConcessionDetails = lendingConcessionDetails;
            }
        }
    }
}