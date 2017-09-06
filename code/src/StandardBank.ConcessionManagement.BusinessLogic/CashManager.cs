using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Cash manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ICashManager" />
    public class CashManager : ICashManager
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
        /// The concession cash repository
        /// </summary>
        private readonly IConcessionCashRepository _concessionCashRepository;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The legal entity account repository
        /// </summary>
        private readonly ILegalEntityAccountRepository _legalEntityAccountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashManager"/> class.
        /// </summary>
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionCashRepository">The concession cash repository.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="legalEntityAccountRepository">The legal entity account repository.</param>
        public CashManager(IPricingManager pricingManager, IConcessionManager concessionManager,
            IConcessionCashRepository concessionCashRepository, ILegalEntityRepository legalEntityRepository,
            IMapper mapper, ILegalEntityAccountRepository legalEntityAccountRepository)
        {
            _pricingManager = pricingManager;
            _concessionManager = concessionManager;
            _concessionCashRepository = concessionCashRepository;
            _legalEntityRepository = legalEntityRepository;
            _mapper = mapper;
            _legalEntityAccountRepository = legalEntityAccountRepository;
        }

        /// <summary>
        /// Gets the cash concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<CashConcession> GetCashConcessionsForRiskGroupNumber(int riskGroupNumber)
        {
            var cashConcessions = new List<CashConcession>();
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Cash");

                foreach (var concession in concessions)
                    AddCashConcessionData(concession, cashConcessions);
            }

            return cashConcessions;
        }

        /// <summary>
        /// Creates the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionCash CreateConcessionCash(CashConcessionDetail cashConcessionDetail, Concession concession)
        {
            var concessionCash = _mapper.Map<ConcessionCash>(cashConcessionDetail);
            concessionCash.ConcessionId = concession.Id;
            return _concessionCashRepository.Create(concessionCash);
        }

        /// <summary>
        /// Gets the cash concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public CashConcession GetCashConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
            var concessionCashEntities = _concessionCashRepository.ReadByConcessionId(concession.Id);

            var cashConcessionDetails = new List<CashConcessionDetail>();

            AddMappedConcessionCashEntities(concessionCashEntities, cashConcessionDetails);

            return new CashConcession
            {
                Concession = concession,
                CashConcessionDetails = cashConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user
            };
        }

        /// <summary>
        /// Deletes the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <returns></returns>
        public ConcessionCash DeleteConcessionCash(CashConcessionDetail cashConcessionDetail)
        {
            var concessionCash = _concessionCashRepository.ReadById(cashConcessionDetail.CashConcessionDetailId);

            _concessionCashRepository.Delete(concessionCash);

            return concessionCash;
        }

        /// <summary>
        /// Updates the concession cash.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionCash UpdateConcessionCash(CashConcessionDetail cashConcessionDetail, Concession concession)
        {
            var mappedConcessionCash = _mapper.Map<ConcessionCash>(cashConcessionDetail);
            mappedConcessionCash.ConcessionId = concession.Id;

            _concessionCashRepository.Update(mappedConcessionCash);

            return mappedConcessionCash;
        }

        /// <summary>
        /// Gets the cash view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public CashView GetCashViewData(int riskGroupNumber)
        {
            var cashConcessions = new List<CashConcession>();
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Cash");

                foreach (var concession in concessions)
                    AddCashConcessionData(concession, cashConcessions);
            }

            return new CashView
            {
                RiskGroup = riskGroup,
                CashConcessions = cashConcessions
            };
        }

        /// <summary>
        /// Adds the cash concession data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="cashConcessions">The cash concessions.</param>
        private void AddCashConcessionData(Concession concession, ICollection<CashConcession> cashConcessions)
        {
            var concessionCashEntities = _concessionCashRepository.ReadByConcessionId(concession.Id);

            if (concessionCashEntities != null && concessionCashEntities.Any())
            {
                var cashConcessionDetails = new List<CashConcessionDetail>();

                var cashConcession =
                    cashConcessions.FirstOrDefault(
                        _ => _.Concession.ReferenceNumber == concession.ReferenceNumber);

                if (cashConcession == null)
                {
                    cashConcession = new CashConcession
                    {
                        Concession = concession,
                        CashConcessionDetails = new List<CashConcessionDetail>()
                    };

                    cashConcessions.Add(cashConcession);
                }

                cashConcessionDetails.AddRange(cashConcession.CashConcessionDetails);

                AddMappedConcessionCashEntities(concessionCashEntities, cashConcessionDetails);

                cashConcession.CashConcessionDetails = cashConcessionDetails;
            }
        }

        /// <summary>
        /// Adds the mapped concession cash entities.
        /// </summary>
        /// <param name="concessionCashEntities">The concession cash entities.</param>
        /// <param name="cashConcessionDetails">The cash concession details.</param>
        private void AddMappedConcessionCashEntities(IEnumerable<ConcessionCash> concessionCashEntities,
            ICollection<CashConcessionDetail> cashConcessionDetails)
        {
            foreach (var concessionCashEntity in concessionCashEntities)
            {
                var legalEntity = _legalEntityRepository.ReadById(concessionCashEntity.LegalEntityId);
                var mappedConcessionCashEntity = _mapper.Map<CashConcessionDetail>(concessionCashEntity);

                mappedConcessionCashEntity.CustomerName = legalEntity.CustomerName;

                var legalEntityAccount =
                    _legalEntityAccountRepository.ReadById(concessionCashEntity.LegalEntityAccountId);

                if (legalEntityAccount != null)
                    mappedConcessionCashEntity.AccountNumber = legalEntityAccount.AccountNumber;

                cashConcessionDetails.Add(mappedConcessionCashEntity);
            }
        }
    }
}
