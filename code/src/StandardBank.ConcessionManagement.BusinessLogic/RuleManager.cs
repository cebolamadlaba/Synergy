using System;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Rule manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IRuleManager" />
    public class RuleManager : IRuleManager
    {
        /// <summary>
        /// The concession relationship repository
        /// </summary>
        private readonly IConcessionRelationshipRepository _concessionRelationshipRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleManager"/> class.
        /// </summary>
        /// <param name="concessionRelationshipRepository">The concession relationship repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public RuleManager(IConcessionRelationshipRepository concessionRelationshipRepository, ILookupTableManager lookupTableManager)
        {
            _concessionRelationshipRepository = concessionRelationshipRepository;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Updates the base fields on approval.
        /// </summary>
        /// <param name="concessionDetail">The concession detail.</param>
        public void UpdateBaseFieldsOnApproval(ConcessionDetail concessionDetail)
        {
            if (!concessionDetail.DateApproved.HasValue)
                concessionDetail.DateApproved = DateTime.Now;

            if (concessionDetail.ExpiryDate.HasValue)
            {
                if (IsExtension(concessionDetail.ConcessionId))
                {
                    var expiryDate = concessionDetail.ExpiryDate.Value;

                    if (expiryDate < DateTime.Now)
                        expiryDate = DateTime.Now;

                    concessionDetail.ExpiryDate = expiryDate.AddMonths(3);
                }
            }
        }

        /// <summary>
        /// Determines whether the specified concession identifier is extension.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified concession identifier is extension; otherwise, <c>false</c>.
        /// </returns>
        private bool IsExtension(int concessionId)
        {
            var parentRelationships = _concessionRelationshipRepository.ReadByChildConcessionId(concessionId);
            var extensionRelationshipId = _lookupTableManager.GetRelationshipId(Constants.RelationshipType.Extension);

            return parentRelationships.Any(_ => _.RelationshipId == extensionRelationshipId);
        }
    }
}
