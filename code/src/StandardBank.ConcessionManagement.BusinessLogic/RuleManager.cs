using System;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Rule manager implementation
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IRuleManager" />
    public class RuleManager : IRuleManager
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The concession relationship repository
        /// </summary>
        private readonly IConcessionRelationshipRepository _concessionRelationshipRepository;

        /// <summary>
        /// The concession repository
        /// </summary>
        private readonly IConcessionRepository _concessionRepository;

        /// <summary>
        /// The concession lending repository
        /// </summary>
        private readonly IConcessionLendingRepository _concessionLendingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleManager"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="concessionRelationshipRepository">The concession relationship repository.</param>
        /// <param name="concessionRepository">The concession repository.</param>
        /// <param name="concessionLendingRepository">The concession lending repository.</param>
        public RuleManager(ILookupTableManager lookupTableManager,
            IConcessionRelationshipRepository concessionRelationshipRepository,
            IConcessionRepository concessionRepository, IConcessionLendingRepository concessionLendingRepository)
        {
            _lookupTableManager = lookupTableManager;
            _concessionRelationshipRepository = concessionRelationshipRepository;
            _concessionRepository = concessionRepository;
            _concessionLendingRepository = concessionLendingRepository;
        }

        /// <summary>
        /// Calculates the expiry date.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        public DateTime CalculateExpiryDate(int concessionId, string concessionType)
        {
            //if this is an extension then the expiry date is three months from the current concenssions expiry date
            var expiryRelationdshipId = _lookupTableManager.GetRelationshipId("Extension");

            var relationships =
                _concessionRelationshipRepository.ReadByChildConcessionIdRelationshipIdRelationships(concessionId,
                    expiryRelationdshipId);

            if (relationships != null && relationships.Any())
            {
                //TODO: Fix this
                ////find the concession we're extending and use it's expiry date to calculate the correct expiry date
                ////for this one
                //foreach (var relationship in relationships.OrderByDescending(_ => _.ParentConcessionId))
                //{
                //    var parentConcession = _concessionRepository.ReadById(relationship.ParentConcessionId);

                //    if (parentConcession.IsActive && parentConcession.IsCurrent)
                //        return parentConcession.ExpiryDate.GetValueOrDefault(DateTime.Now).AddMonths(3);
                //}

                return DateTime.Now.AddMonths(3);
            }

            switch (concessionType)
            {
                case "Lending":
                    return CalculateLendingExpiryDate(concessionId);
            }

            return DateTime.Now.AddMonths(12);
        }

        /// <summary>
        /// Calculates the lending expiry date.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        private DateTime CalculateLendingExpiryDate(int concessionId)
        {
            var lendingConcessionDetails = _concessionLendingRepository.ReadByConcessionId(concessionId);
            var hasOverdraft = false;

            //default the minimum term to the first term vailable
            var minimumTerms = lendingConcessionDetails.First(_ => _.Term.HasValue).Term;

            foreach (var lendingConcessionDetail in lendingConcessionDetails)
            {
                var productName = _lookupTableManager.GetProductTypeName(lendingConcessionDetail.ProductTypeId);

                if (productName == "Overdraft")
                    hasOverdraft = true;
                
                //if the term is lower then the current minimum term we have, then that's the new minimum term
                if (lendingConcessionDetail.Term.HasValue && minimumTerms > lendingConcessionDetail.Term.Value)
                    minimumTerms = lendingConcessionDetail.Term.Value;
            }

            //if there is no minimum term default it to 12
            if (!minimumTerms.HasValue)
                minimumTerms = 12;

            //if we have an overdraft and the minimum term is more than a year,
            //default the minimum terms to a year
            if (hasOverdraft)
            {
                if (minimumTerms.Value > 12)
                    minimumTerms = 12;
            }

            return DateTime.Now.AddMonths(minimumTerms.Value);
        }
    }
}
