using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
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
        /// The loaded price transactional repository
        /// </summary>
        private readonly ILoadedPriceTransactionalRepository _loadedPriceTransactionalRepository;

        /// <summary>
        /// The rule manager
        /// </summary>
        private readonly IRuleManager _ruleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalManager"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionTransactionalRepository">The concession transactional repository.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="legalEntityAccountRepository">The legal entity account repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="financialTransactionalRepository">The financial transactional repository.</param>
        /// <param name="productTransactionalRepository">The product transactional repository.</param>
        /// <param name="loadedPriceTransactionalRepository">The loaded price transactional repository.</param>
        /// <param name="ruleManager">The rule manager.</param>
        public TransactionalManager(IConcessionManager concessionManager,
            IConcessionTransactionalRepository concessionTransactionalRepository,
            ILegalEntityRepository legalEntityRepository, ILegalEntityAccountRepository legalEntityAccountRepository,
            IMapper mapper, ILookupTableManager lookupTableManager,
            IFinancialTransactionalRepository financialTransactionalRepository,
            IProductTransactionalRepository productTransactionalRepository,
            ILoadedPriceTransactionalRepository loadedPriceTransactionalRepository, IRuleManager ruleManager)
        {
            _concessionManager = concessionManager;
            _concessionTransactionalRepository = concessionTransactionalRepository;
            _legalEntityRepository = legalEntityRepository;
            _legalEntityAccountRepository = legalEntityAccountRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _financialTransactionalRepository = financialTransactionalRepository;
            _productTransactionalRepository = productTransactionalRepository;
            _loadedPriceTransactionalRepository = loadedPriceTransactionalRepository;
            _ruleManager = ruleManager;
        }

        /// <summary>
        /// Gets the transactional concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<TransactionalConcession> GetTransactionalConcessionsForRiskGroupNumber(int riskGroupNumber)
        {
            var transactionalConcessions = new List<TransactionalConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

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

            if (concession.Status == "Approved" || concession.Status == "Approved With Changes")
            {
                UpdateApprovedTransactionTableNumber(mappedConcessionTransactional);
                UpdateIsMismatched(mappedConcessionTransactional);

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionTransactional);
            }
            else if (concession.Status == "Pending" && concession.SubStatus == "PCM Approved With Changes")
            {
                UpdateApprovedTransactionTableNumber(mappedConcessionTransactional);
            }

            _concessionTransactionalRepository.Update(mappedConcessionTransactional);

            return mappedConcessionTransactional;
        }

        /// <summary>
        /// Updates the approved transaction table number.
        /// </summary>
        /// <param name="mappedConcessionTransactional">The mapped concession transactional.</param>
        private void UpdateApprovedTransactionTableNumber(ConcessionTransactional mappedConcessionTransactional)
        {
            var databaseTransactionalConcession =
                _concessionTransactionalRepository.ReadById(mappedConcessionTransactional.Id);

            if (databaseTransactionalConcession.ApprovedTransactionTableNumberId.HasValue)
            {
                mappedConcessionTransactional.ApprovedTransactionTableNumberId =
                    databaseTransactionalConcession.ApprovedTransactionTableNumberId;
            }
            else
            {
                //the approved table number is the table number that was captured when approving
                mappedConcessionTransactional.ApprovedTransactionTableNumberId =
                    mappedConcessionTransactional.TransactionTableNumberId;

                //the table number is what is currently in the database
                mappedConcessionTransactional.TransactionTableNumberId = databaseTransactionalConcession.TransactionTableNumberId;
            }
        }

        /// <summary>
        /// Updates the is mismatched.
        /// </summary>
        /// <param name="mappedConcessionTransactional">The mapped concession transactional.</param>
        private void UpdateIsMismatched(ConcessionTransactional mappedConcessionTransactional)
        {
            mappedConcessionTransactional.IsMismatched = false;

            if (mappedConcessionTransactional.TransactionTypeId.HasValue)
            {
                var loadedPriceTransactional =
                    _loadedPriceTransactionalRepository.ReadByTransactionTypeIdLegalEntityAccountId(
                        mappedConcessionTransactional.TransactionTypeId.Value,
                        mappedConcessionTransactional.LegalEntityAccountId);

                if (loadedPriceTransactional != null)
                {
                    mappedConcessionTransactional.LoadedTransactionTableNumberId =
                        loadedPriceTransactional.TransactionTableNumberId;

                    if (loadedPriceTransactional.TransactionTableNumberId !=
                        mappedConcessionTransactional.ApprovedTransactionTableNumberId)
                        mappedConcessionTransactional.IsMismatched = true;
                }
                else
                {
                    mappedConcessionTransactional.IsMismatched = true;
                }
            }
            else
            {
                mappedConcessionTransactional.IsMismatched = true;
            }
        }

        /// <summary>
        /// Gets the transactional view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public TransactionalView GetTransactionalViewData(int riskGroupNumber)
        {
            var transactionalConcessions = new List<TransactionalConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

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
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public decimal GetLatestCrsOrMrs(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            var transactionFinancial =
                _financialTransactionalRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                new FinancialTransactional();

            return transactionFinancial.LatestCrsOrMrs;
        }

        /// <summary>
        /// Gets the transactional financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public TransactionalFinancial GetTransactionalFinancialForRiskGroupNumber(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            return _mapper.Map<TransactionalFinancial>(
                _financialTransactionalRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                new FinancialTransactional());
        }

        /// <summary>
        /// Deletes the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <returns></returns>
        public ConcessionTransactional DeleteConcessionTransactional(TransactionalConcessionDetail transactionalConcessionDetail)
        {
            var concessionTransactional =
                _concessionTransactionalRepository.ReadById(transactionalConcessionDetail
                    .TransactionalConcessionDetailId);

            _concessionTransactionalRepository.Delete(concessionTransactional);

            return concessionTransactional;
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
            var transactionTableNumbers = _lookupTableManager.GetTransactionTableNumbers();

            foreach (var transactionalProduct in transactionalProducts)
            {
                var legalEntity = _legalEntityRepository.ReadById(transactionalProduct.LegalEntityId);
                var legalEntityAccount =
                    _legalEntityAccountRepository.ReadById(transactionalProduct.LegalEntityAccountId);
                var mappedTransactionalProduct = _mapper.Map<TransactionalProduct>(transactionalProduct);

                mappedTransactionalProduct.CustomerName = legalEntity.CustomerName;
                mappedTransactionalProduct.AccountNumber = legalEntityAccount.AccountNumber;
                mappedTransactionalProduct.TariffTable = transactionTableNumbers
                    .First(_ => _.Id == transactionalProduct.TransactionTableNumberId).TariffTable;

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

                if (mappedTransactionalConcessionDetail.ApprovedTransactionTableNumberId.HasValue)
                    mappedTransactionalConcessionDetail.ApprovedTableNumber =
                        _lookupTableManager.GetTransactionTableNumberDescription(mappedTransactionalConcessionDetail
                            .ApprovedTransactionTableNumberId.Value);

                if (mappedTransactionalConcessionDetail.LoadedTransactionTableNumberId.HasValue)
                    mappedTransactionalConcessionDetail.LoadedTableNumber =
                        _lookupTableManager.GetTransactionTableNumberDescription(mappedTransactionalConcessionDetail
                            .LoadedTransactionTableNumberId.Value);

                transactionalConcessionDetails.Add(mappedTransactionalConcessionDetail);
            }
        }
    }
}
