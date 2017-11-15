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
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The financial cash repository
        /// </summary>
        private readonly IFinancialCashRepository _financialCashRepository;

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
        /// The misc performance repository
        /// </summary>
        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashManager"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionCashRepository">The concession cash repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="financialCashRepository">The financial cash repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="loadedPriceCashRepository">The loaded price cash repository.</param>
        /// <param name="ruleManager">The rule manager.</param>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        public CashManager(IConcessionManager concessionManager, IConcessionCashRepository concessionCashRepository,
            IMapper mapper, IFinancialCashRepository financialCashRepository, ILookupTableManager lookupTableManager,
            ILoadedPriceCashRepository loadedPriceCashRepository, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository)
        {
            _concessionManager = concessionManager;
            _concessionCashRepository = concessionCashRepository;
            _mapper = mapper;
            _financialCashRepository = financialCashRepository;
            _lookupTableManager = lookupTableManager;
            _loadedPriceCashRepository = loadedPriceCashRepository;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
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

            var cashConcessionDetails = _miscPerformanceRepository.GetCashConcessionDetails(concession.Id);

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
                UpdateApprovedTableNumber(mappedConcessionCash);
                UpdateIsMismatched(mappedConcessionCash);

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionCash);
            }
            else if (concession.Status == "Pending" && concession.SubStatus == "PCM Approved With Changes")
            {
                UpdateApprovedTableNumber(mappedConcessionCash);
            }

            _concessionCashRepository.Update(mappedConcessionCash);

            return mappedConcessionCash;
        }

        /// <summary>
        /// Updates the approved table number.
        /// </summary>
        /// <param name="mappedConcessionCash">The mapped concession cash.</param>
        private void UpdateApprovedTableNumber(ConcessionCash mappedConcessionCash)
        {
            var databaseCashConcession =
                _concessionCashRepository.ReadById(mappedConcessionCash.Id);

            if (databaseCashConcession.ApprovedTableNumberId.HasValue)
            {
                mappedConcessionCash.ApprovedTableNumberId = databaseCashConcession.ApprovedTableNumberId;
            }
            else
            {
                //the approved table number is the table number that was captured when approving
                mappedConcessionCash.ApprovedTableNumberId = mappedConcessionCash.TableNumberId;

                //the table number is what is currently in the database
                mappedConcessionCash.TableNumberId = databaseCashConcession.TableNumberId;
            }
        }

        /// <summary>
        /// Updates the is mismatched.
        /// </summary>
        /// <param name="mappedConcessionCash">The mapped concession cash.</param>
        private void UpdateIsMismatched(ConcessionCash mappedConcessionCash)
        {
            mappedConcessionCash.IsMismatched = false;

            var loadedPriceCash =
                _loadedPriceCashRepository.ReadByChannelTypeIdLegalEntityAccountId(
                    mappedConcessionCash.ChannelTypeId, mappedConcessionCash.LegalEntityAccountId);

            if (loadedPriceCash != null)
            {
                mappedConcessionCash.LoadedTableNumberId = loadedPriceCash.TableNumberId;

                if (loadedPriceCash.TableNumberId != mappedConcessionCash.ApprovedTableNumberId)
                    mappedConcessionCash.IsMismatched = true;
            }
            else
            {
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
            var concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, "Cash");

            foreach (var concession in concessions)
            {
                cashConcessions.Add(new CashConcession
                {
                    CashConcessionDetails = _miscPerformanceRepository.GetCashConcessionDetails(concession.Id),
                    Concession = concession
                });
            }

            var cashFinancial =
                _mapper.Map<CashFinancial>(_financialCashRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                                           new FinancialCash());

            var cashProducts = GetCashProducts(riskGroup);

            return new CashView
            {
                RiskGroup = riskGroup,
                CashConcessions = cashConcessions.OrderByDescending(_ => _.Concession.DateOpened),
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
            return _miscPerformanceRepository.GetCashProducts(riskGroup.Id, riskGroup.Name);
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
    }
}
