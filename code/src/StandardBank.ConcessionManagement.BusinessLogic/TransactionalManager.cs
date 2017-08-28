using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Transactional manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ITransactionalManager" />
    public class TransactionalManager : ITransactionalManager
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
        /// The concession transactional repository
        /// </summary>
        private readonly IConcessionTransactionalRepository _concessionTransactionalRepository;

        /// <summary>
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

        /// <summary>
        /// The legal entity account repository
        /// </summary>
        private readonly ILegalEntityAccountRepository _legalEntityAccountRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalManager"/> class.
        /// </summary>
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionTransactionalRepository">The concession transactional repository.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="legalEntityAccountRepository">The legal entity account repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public TransactionalManager(IPricingManager pricingManager, IConcessionManager concessionManager,
            IConcessionTransactionalRepository concessionTransactionalRepository,
            ILegalEntityRepository legalEntityRepository, ILegalEntityAccountRepository legalEntityAccountRepository,
            IMapper mapper, ILookupTableManager lookupTableManager)
        {
            _pricingManager = pricingManager;
            _concessionManager = concessionManager;
            _concessionTransactionalRepository = concessionTransactionalRepository;
            _legalEntityRepository = legalEntityRepository;
            _legalEntityAccountRepository = legalEntityAccountRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the cash concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<TransactionalConcession> GetCashConcessionsForRiskGroupNumber(int riskGroupNumber)
        {
            var transactionalConcessions = new List<TransactionalConcession>();
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Transactional");

                foreach (var concession in concessions)
                    AddTransactionalConcessionData(concession, transactionalConcessions);
            }

            return transactionalConcessions;
        }

        /// <summary>
        /// Adds the transactional concession data.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="transactionalConcessions">The transactional concessions.</param>
        private void AddTransactionalConcessionData(Concession concession, ICollection<TransactionalConcession> transactionalConcessions)
        {
            var concessionTransactionals = _concessionTransactionalRepository.ReadByConcessionId(concession.Id);

            if (concessionTransactionals != null && concessionTransactionals.Any())
            {
                var transactionalConcessionDetails = new List<TransactionalConcessionDetail>();

                var transactionalConcession =
                    transactionalConcessions.FirstOrDefault(
                        _ => _.Concession.ReferenceNumber == concession.ReferenceNumber);

                if (transactionalConcession == null)
                {
                    transactionalConcession = new TransactionalConcession
                    {
                        Concession = concession,
                        TransactionalConcessionDetails = new List<TransactionalConcessionDetail>()
                    };

                    transactionalConcessions.Add(transactionalConcession);
                }

                transactionalConcessionDetails.AddRange(transactionalConcession.TransactionalConcessionDetails);

                AddMappedConcessionTransactionals(concessionTransactionals, transactionalConcessionDetails);

                transactionalConcession.TransactionalConcessionDetails = transactionalConcessionDetails;
            }
        }

        /// <summary>
        /// Adds the mapped concession transactionals.
        /// </summary>
        /// <param name="concessionTransactionals">The concession transactionals.</param>
        /// <param name="transactionalConcessionDetails">The transactional concession details.</param>
        private void AddMappedConcessionTransactionals(IEnumerable<ConcessionTransactional> concessionTransactionals,
            ICollection<TransactionalConcessionDetail> transactionalConcessionDetails)
        {
            foreach (var concessionTransactional in concessionTransactionals)
            {
                var legalEntity = _legalEntityRepository.ReadById(concessionTransactional.LegalEntityId);
                var mappedTransactionalConcessionDetail = _mapper.Map<TransactionalConcessionDetail>(concessionTransactional);

                mappedTransactionalConcessionDetail.CustomerName = legalEntity.CustomerName;

                if (concessionTransactional.TransactionTypeId.HasValue)
                    mappedTransactionalConcessionDetail.TransactionType =
                        _lookupTableManager.GetTransactionTypeDescription(concessionTransactional.TransactionTypeId
                            .Value);

                var legalEntityAccount =
                    _legalEntityAccountRepository.ReadById(concessionTransactional.LegalEntityAccountId);

                if (legalEntityAccount != null)
                    mappedTransactionalConcessionDetail.AccountNumber = legalEntityAccount.AccountNumber;

                transactionalConcessionDetails.Add(mappedTransactionalConcessionDetail);
            }
        }
    }
}
