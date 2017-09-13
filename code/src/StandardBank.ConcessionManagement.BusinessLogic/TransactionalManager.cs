using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.Pricing.RiskGroup;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

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
        /// The financial transactional repository
        /// </summary>
        private readonly IFinancialTransactionalRepository _financialTransactionalRepository;

        /// <summary>
        /// The product transactional repository
        /// </summary>
        private readonly IProductTransactionalRepository _productTransactionalRepository;

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
        /// <param name="financialTransactionalRepository">The financial transactional repository.</param>
        /// <param name="productTransactionalRepository">The product transactional repository.</param>
        public TransactionalManager(IPricingManager pricingManager, IConcessionManager concessionManager,
            IConcessionTransactionalRepository concessionTransactionalRepository,
            ILegalEntityRepository legalEntityRepository, ILegalEntityAccountRepository legalEntityAccountRepository,
            IMapper mapper, ILookupTableManager lookupTableManager,
            IFinancialTransactionalRepository financialTransactionalRepository,
            IProductTransactionalRepository productTransactionalRepository)
        {
            _pricingManager = pricingManager;
            _concessionManager = concessionManager;
            _concessionTransactionalRepository = concessionTransactionalRepository;
            _legalEntityRepository = legalEntityRepository;
            _legalEntityAccountRepository = legalEntityAccountRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _financialTransactionalRepository = financialTransactionalRepository;
            _productTransactionalRepository = productTransactionalRepository;
        }

        /// <summary>
        /// Gets the transactional concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<TransactionalConcession> GetTransactionalConcessionsForRiskGroupNumber(int riskGroupNumber)
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
        /// Gets the transactional concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public TransactionalConcession GetTransactionalConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
            var concessionCashEntities = _concessionTransactionalRepository.ReadByConcessionId(concession.Id);

            var transactionalConcessionDetails = new List<TransactionalConcessionDetail>();

            AddMappedConcessionTransactionals(concessionCashEntities, transactionalConcessionDetails);

            return new TransactionalConcession
            {
                Concession = concession,
                TransactionalConcessionDetails = transactionalConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user
            };
        }

        /// <summary>
        /// Creates the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionTransactional CreateConcessionTransactional(TransactionalConcessionDetail transactionalConcessionDetail,
            Concession concession)
        {
            var concessionTransactional = _mapper.Map<ConcessionTransactional>(transactionalConcessionDetail);
            concessionTransactional.ConcessionId = concession.Id;
            return _concessionTransactionalRepository.Create(concessionTransactional);
        }

        /// <summary>
        /// Updates the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionTransactional UpdateConcessionTransactional(TransactionalConcessionDetail transactionalConcessionDetail,
            Concession concession)
        {
            var mappedConcessionTransactional = _mapper.Map<ConcessionTransactional>(transactionalConcessionDetail);
            mappedConcessionTransactional.ConcessionId = concession.Id;

            _concessionTransactionalRepository.Update(mappedConcessionTransactional);

            return mappedConcessionTransactional;
        }

        /// <summary>
        /// Gets the transactional view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public TransactionalView GetTransactionalViewData(int riskGroupNumber)
        {
            var transactionalConcessions = new List<TransactionalConcession>();
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Transactional");

                foreach (var concession in concessions)
                    AddTransactionalConcessionData(concession, transactionalConcessions);
            }

            var transactionalFinancial =
                _mapper.Map<TransactionalFinancial>(_financialTransactionalRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                                           new FinancialTransactional());

            var transactionalProducts = GetTransactionalProducts(riskGroup);

            return new TransactionalView
            {
                RiskGroup = riskGroup,
                TransactionalConcessions = transactionalConcessions,
                TransactionalFinancial = transactionalFinancial,
                TransactionalProducts = transactionalProducts
            };
        }

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroup">The risk group.</param>
        /// <returns></returns>
        private IEnumerable<TransactionalProduct> GetTransactionalProducts(RiskGroup riskGroup)
        {
            var mappedTransactionalProducts = new List<TransactionalProduct>();
            var transactionalProducts = _productTransactionalRepository.ReadByRiskGroupId(riskGroup.Id);
            var tableNumbers = _lookupTableManager.GetTableNumbers();

            foreach (var transactionalProduct in transactionalProducts)
            {
                var legalEntity = _legalEntityRepository.ReadById(transactionalProduct.LegalEntityId);
                var legalEntityAccount = _legalEntityAccountRepository.ReadById(transactionalProduct.LegalEntityAccountId);
                var mappedTransactionalProduct = _mapper.Map<TransactionalProduct>(transactionalProduct);

                mappedTransactionalProduct.CustomerName = legalEntity.CustomerName;
                mappedTransactionalProduct.AccountNumber = legalEntityAccount.AccountNumber;
                mappedTransactionalProduct.TariffTable = tableNumbers.First(_ => _.Id == transactionalProduct.TableNumberId).TariffTable;

                mappedTransactionalProduct.TransactionType =
                    _lookupTableManager.GetTransactionTypeDescription(transactionalProduct.TransactionTypeId);

                mappedTransactionalProducts.Add(mappedTransactionalProduct);
            }

            return mappedTransactionalProducts;
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
