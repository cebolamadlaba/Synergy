using System.Collections.Generic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Concession manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IConcessionManager" />
    public class ConcessionManager : IConcessionManager
    {
        /// <summary>
        /// The concession repository
        /// </summary>
        private readonly IConcessionRepository _concessionRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionManager"/> class.
        /// </summary>
        /// <param name="concessionRepository">The concession repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        public ConcessionManager(IConcessionRepository concessionRepository, ILookupTableManager lookupTableManager,
            ILegalEntityRepository legalEntityRepository, IRiskGroupRepository riskGroupRepository)
        {
            _concessionRepository = concessionRepository;
            _lookupTableManager = lookupTableManager;
            _legalEntityRepository = legalEntityRepository;
            _riskGroupRepository = riskGroupRepository;
        }

        /// <summary>
        /// Gets the pending concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<Concession> GetPendingConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();
            var pendingStatusId = _lookupTableManager.GetStatusId("Pending");
            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId("BCM Pending");

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        concessions.AddRange(Map(
                            _concessionRepository.ReadByRequestorIdStatusIdSubStatusIdIsActive(user.Id, pendingStatusId,
                                bcmPendingStatusId, true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        break;
                    case "PCM":
                    case "Head Office":
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Maps the specified repository concessions.
        /// </summary>
        /// <param name="repositoryConcessions">The repository concessions.</param>
        /// <returns></returns>
        private IEnumerable<Concession> Map(IEnumerable<Model.Repository.Concession> repositoryConcessions)
        {
            var concessions = new List<Concession>();

            foreach (var concession in repositoryConcessions)
            {
                var legalEntity = _legalEntityRepository.ReadById(concession.LegalEntityId);
                var riskGroup = _riskGroupRepository.ReadById(legalEntity.RiskGroupId);
                
                concessions.Add(new Concession
                {
                    Id = concession.Id,
                    CustomerName = legalEntity.CustomerName,
                    DateOpened = concession.ConcessionDate,
                    DateSentForApproval = concession.DatesentForApproval,
                    RiskGroupName = riskGroup.RiskGroupName,
                    RiskGroupNumber = riskGroup.RiskGroupNumber,
                    Seqment = _lookupTableManager.GetMarketSegmentName(legalEntity.MarketSegmentId),
                    Type = _lookupTableManager.GetReferenceTypeName(concession.TypeId)
                });
            }

            return concessions;
        }
    }
}
