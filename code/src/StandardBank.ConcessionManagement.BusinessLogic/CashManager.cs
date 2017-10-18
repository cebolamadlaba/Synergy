using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
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
        /// The financial cash repository
        /// </summary>
        private readonly IFinancialCashRepository _financialCashRepository;

        /// <summary>
        /// The product cash repository
        /// </summary>
        private readonly IProductCashRepository _productCashRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The loaded price cash repository
        /// </summary>
        private readonly ILoadedPriceCashRepository _loadedPriceCashRepository;

        /// <summary>
        /// The rule manager
        /// </summary>
        private readonly IRuleManager _ruleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashManager"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionCashRepository">The concession cash repository.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="legalEntityAccountRepository">The legal entity account repository.</param>
        /// <param name="financialCashRepository">The financial cash repository.</param>
        /// <param name="productCashRepository">The product cash repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="loadedPriceCashRepository">The loaded price cash repository.</param>
        /// <param name="ruleManager">The rule manager.</param>
        public CashManager(IConcessionManager concessionManager,
            IConcessionCashRepository concessionCashRepository, ILegalEntityRepository legalEntityRepository,
            IMapper mapper, ILegalEntityAccountRepository legalEntityAccountRepository,
            IFinancialCashRepository financialCashRepository, IProductCashRepository productCashRepository,
            ILookupTableManager lookupTableManager, ILoadedPriceCashRepository loadedPriceCashRepository,
            IRuleManager ruleManager)
        {
            _concessionManager = concessionManager;
            _concessionCashRepository = concessionCashRepository;
            _legalEntityRepository = legalEntityRepository;
            _mapper = mapper;
            _legalEntityAccountRepository = legalEntityAccountRepository;
            _financialCashRepository = financialCashRepository;
            _productCashRepository = productCashRepository;
            _lookupTableManager = lookupTableManager;
            _loadedPriceCashRepository = loadedPriceCashRepository;
            _ruleManager = ruleManager;
        }

        /// <summary>
        /// Gets the cash concessions for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public IEnumerable<CashConcession> GetCashConcessionsForRiskGroupNumber(int riskGroupNumber)
        {
            var cashConcessions = new List<CashConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

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

            if (concession.Status == "Approved" || concession.Status == "Approved With Changes")
            {
                UpdateApprovedTableNumberAndIsMismatched(mappedConcessionCash);

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionCash);
            }

            _concessionCashRepository.Update(mappedConcessionCash);

            return mappedConcessionCash;
        }

        /// <summary>
        /// Updates the approved table number and is mismatched.
        /// </summary>
        /// <param name="mappedConcessionCash">The mapped concession cash.</param>
        private void UpdateApprovedTableNumberAndIsMismatched(ConcessionCash mappedConcessionCash)
        {
            var databaseCashConcession =
                _concessionCashRepository.ReadById(mappedConcessionCash.Id);

            //the approved table number is the table number that was captured when approving
            mappedConcessionCash.ApprovedTableNumberId = mappedConcessionCash.TableNumberId;

            //the table number is what is currently in the database
            mappedConcessionCash.TableNumberId = databaseCashConcession.TableNumberId;

            var loadedPriceCash =
                _loadedPriceCashRepository.ReadByChannelTypeIdLegalEntityAccountId(
                    mappedConcessionCash.ChannelTypeId, mappedConcessionCash.LegalEntityAccountId);

            if (loadedPriceCash != null)
            {
                mappedConcessionCash.LoadedTableNumberId = loadedPriceCash.TableNumberId;

                if (loadedPriceCash.TableNumberId != mappedConcessionCash.ApprovedTableNumberId)
                    mappedConcessionCash.IsMismatched = true;
            }
        }

        /// <summary>
        /// Gets the cash view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public CashView GetCashViewData(int riskGroupNumber)
        {
            var cashConcessions = new List<CashConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Cash");

                foreach (var concession in concessions)
                    AddCashConcessionData(concession, cashConcessions);
            }

            var cashFinancial =
                _mapper.Map<CashFinancial>(_financialCashRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                                           new FinancialCash());

            var cashProducts = GetCashProducts(riskGroup);

            return new CashView
            {
                RiskGroup = riskGroup,
                CashConcessions = cashConcessions,
                CashFinancial = cashFinancial,
                CashProducts = cashProducts
            };
        }

        /// <summary>
        /// Gets the cash products.
        /// </summary>
        /// <param name="riskGroup">The risk group.</param>
        /// <returns></returns>
        private IEnumerable<CashProduct> GetCashProducts(RiskGroup riskGroup)
        {
            var mappedCashProducts = new List<CashProduct>();
            var cashProducts = _productCashRepository.ReadByRiskGroupId(riskGroup.Id);
            var tableNumbers = _lookupTableManager.GetTableNumbers("Cash");

            foreach (var cashProduct in cashProducts)
            {
                var legalEntity = _legalEntityRepository.ReadById(cashProduct.LegalEntityId);
                var legalEntityAccount = _legalEntityAccountRepository.ReadById(cashProduct.LegalEntityAccountId);
                var mappedCashProduct = _mapper.Map<CashProduct>(cashProduct);

                mappedCashProduct.CustomerName = legalEntity.CustomerName;
                mappedCashProduct.AccountNumber = legalEntityAccount.AccountNumber;
                mappedCashProduct.TariffTable = tableNumbers.First(_ => _.Id == cashProduct.TableNumberId).TariffTable;

                mappedCashProducts.Add(mappedCashProduct);
            }

            return mappedCashProducts;
        }

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public decimal GetLatestCrsOrMrs(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            var cashFinancial = _mapper.Map<CashFinancial>(
                _financialCashRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialCash());

            return cashFinancial.LatestCrsOrMrs;
        }

        /// <summary>
        /// Gets the cash financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public CashFinancial GetCashFinancialForRiskGroupNumber(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            return _mapper.Map<CashFinancial>(
                _financialCashRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialCash());
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

                if (mappedConcessionCashEntity.ChannelTypeId.HasValue)
                    mappedConcessionCashEntity.Channel =
                        _lookupTableManager.GetChannelTypeName(mappedConcessionCashEntity.ChannelTypeId.Value);

                if (mappedConcessionCashEntity.ApprovedTableNumberId.HasValue)
                {
                    mappedConcessionCashEntity.ApprovedTableNumber =
                        _lookupTableManager.GetTableNumberDescription(mappedConcessionCashEntity.ApprovedTableNumberId
                            .Value);
                }

                if (mappedConcessionCashEntity.LoadedTableNumberId.HasValue)
                {
                    mappedConcessionCashEntity.LoadedTableNumber =
                        _lookupTableManager.GetTableNumberDescription(mappedConcessionCashEntity.LoadedTableNumberId
                            .Value);
                }

                cashConcessionDetails.Add(mappedConcessionCashEntity);
            }
        }
    }
}
