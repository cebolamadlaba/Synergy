using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

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
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The legal entity account repository
        /// </summary>
        private readonly ILegalEntityAccountRepository _legalEntityAccountRepository;

        /// <summary>
        /// Intializes an instance of the class
        /// </summary>
        /// <param name="pricingManager"></param>
        /// <param name="concessionManager"></param>
        /// <param name="legalEntityRepository"></param>
        /// <param name="concessionLendingRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="legalEntityAccountRepository"></param>
        public LendingManager(IPricingManager pricingManager, IConcessionManager concessionManager,
            ILegalEntityRepository legalEntityRepository, IConcessionLendingRepository concessionLendingRepository,
            IMapper mapper, ILegalEntityAccountRepository legalEntityAccountRepository)
        {
            _pricingManager = pricingManager;
            _concessionManager = concessionManager;
            _legalEntityRepository = legalEntityRepository;
            _concessionLendingRepository = concessionLendingRepository;
            _mapper = mapper;
            _legalEntityAccountRepository = legalEntityAccountRepository;
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
        /// Creates a concession lending
        /// </summary>
        /// <param name="lendingConcessionDetail"></param>
        /// <param name="concession"></param>
        /// <returns></returns>
        public ConcessionLending CreateConcessionLending(LendingConcessionDetail lendingConcessionDetail, Concession concession)
        {
            var concessionLending = _mapper.Map<ConcessionLending>(lendingConcessionDetail);

            concessionLending.ConcessionId = concession.Id;

            return _concessionLendingRepository.Create(concessionLending);
        }

        /// <summary>
        /// Gets the lending concession for the concession reference id specified
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public LendingConcession GetLendingConcession(string concessionReferenceId, User currentUser)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
            var concessionLendings = _concessionLendingRepository.ReadByConcessionId(concession.Id);

            var lendingConcessionDetails = new List<LendingConcessionDetail>();

            AddMappedConcessionLendings(concessionLendings, lendingConcessionDetails);

            return new LendingConcession
            {
                Concession = concession,
                LendingConcessionDetails = lendingConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = currentUser
            };
        }

        /// <summary>
        /// Deletes the concession lending.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        public ConcessionLending DeleteConcessionLending(LendingConcessionDetail lendingConcessionDetail)
        {
            var concessionLending = _concessionLendingRepository.ReadById(lendingConcessionDetail.LendingConcessionDetailId);

            _concessionLendingRepository.Delete(concessionLending);

            return concessionLending;
        }

        /// <summary>
        /// Updates the concession lending.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionLending UpdateConcessionLending(LendingConcessionDetail lendingConcessionDetail, Concession concession)
        {
            var concessionLending = _mapper.Map<ConcessionLending>(lendingConcessionDetail);

            concessionLending.ConcessionId = concession.Id;

            _concessionLendingRepository.Update(concessionLending);
            return concessionLending;
        }

        /// <summary>
        /// Adds the lending concession data
        /// </summary>
        /// <param name="concession"></param>
        /// <param name="lendingConcessions"></param>
        private void AddLendingConcessionData(Concession concession, ICollection<LendingConcession> lendingConcessions)
        {
            var concessionLendings = _concessionLendingRepository.ReadByConcessionId(concession.Id);

            if (concessionLendings != null && concessionLendings.Any())
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

                AddMappedConcessionLendings(concessionLendings, lendingConcessionDetails);

                lendingConcession.LendingConcessionDetails = lendingConcessionDetails;
            }
        }

        /// <summary>
        /// Adds the mapped concession lendings
        /// </summary>
        /// <param name="concessionLendings"></param>
        /// <param name="lendingConcessionDetails"></param>
        private void AddMappedConcessionLendings(IEnumerable<ConcessionLending> concessionLendings,
            ICollection<LendingConcessionDetail> lendingConcessionDetails)
        {
            foreach (var concessionLending in concessionLendings)
            {
                var legalEntity = _legalEntityRepository.ReadById(concessionLending.LegalEntityId);
                var mappedLendingConcessionDetail = _mapper.Map<LendingConcessionDetail>(concessionLending);

                mappedLendingConcessionDetail.CustomerName = legalEntity.CustomerName;

                var legalEntityAccount =
                    _legalEntityAccountRepository.ReadById(concessionLending.LegalEntityAccountId);

                if (legalEntityAccount != null)
                    mappedLendingConcessionDetail.AccountNumber = legalEntityAccount.AccountNumber;

                mappedLendingConcessionDetail.LoadedMap = concessionLending?.MarginToPrime ?? 0;
                mappedLendingConcessionDetail.ApprovedMap = concessionLending?.ApprovedMarginToPrime ?? 0;

                lendingConcessionDetails.Add(mappedLendingConcessionDetail);
            }
        }
    }
}