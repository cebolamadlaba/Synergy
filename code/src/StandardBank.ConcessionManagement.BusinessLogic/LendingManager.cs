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
        /// The product lending repository
        /// </summary>
        private readonly IProductLendingRepository _productLendingRepository;

        /// <summary>
        /// The financial lending repository
        /// </summary>
        private readonly IFinancialLendingRepository _financialLendingRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendingManager"/> class.
        /// </summary>
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="concessionLendingRepository">The concession lending repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="legalEntityAccountRepository">The legal entity account repository.</param>
        /// <param name="productLendingRepository">The product lending repository.</param>
        /// <param name="financialLendingRepository">The financial lending repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public LendingManager(IPricingManager pricingManager, IConcessionManager concessionManager,
            ILegalEntityRepository legalEntityRepository, IConcessionLendingRepository concessionLendingRepository,
            IMapper mapper, ILegalEntityAccountRepository legalEntityAccountRepository,
            IProductLendingRepository productLendingRepository, IFinancialLendingRepository financialLendingRepository,
            ILookupTableManager lookupTableManager)
        {
            _pricingManager = pricingManager;
            _concessionManager = concessionManager;
            _legalEntityRepository = legalEntityRepository;
            _concessionLendingRepository = concessionLendingRepository;
            _mapper = mapper;
            _legalEntityAccountRepository = legalEntityAccountRepository;
            _productLendingRepository = productLendingRepository;
            _financialLendingRepository = financialLendingRepository;
            _lookupTableManager = lookupTableManager;
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
        /// Gets the lending view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public LendingView GetLendingViewData(int riskGroupNumber)
        {
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            var lendingConcessions = new List<LendingConcession>();

            if (riskGroup != null)
            {
                var concessions = _concessionManager.GetConcessionsForRiskGroup(riskGroup.Id, "Lending");

                foreach (var concession in concessions)
                    AddLendingConcessionData(concession, lendingConcessions);
            }

            var lendingProducts = GetLendingProducts(riskGroup.Id, riskGroup.Name);
            var lendingFinancial = _mapper.Map<LendingFinancial>(
                _financialLendingRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                new FinancialLending());

            return new LendingView
            {
                RiskGroup = riskGroup,
                LendingConcessions = lendingConcessions,
                LendingProducts = lendingProducts,
                LendingFinancial = lendingFinancial
            };
        }

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public decimal GetLatestCrsOrMrs(int riskGroupNumber)
        {
            var riskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            var lendingFinancial = _mapper.Map<LendingFinancial>(
                _financialLendingRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                new FinancialLending());

            return lendingFinancial.LatestCrsOrMrs;
        }

        /// <summary>
        /// Gets the lending products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        private IEnumerable<LendingProduct> GetLendingProducts(int riskGroupId, string riskGroupName)
        {
            var productLendings = _productLendingRepository.ReadByRiskGroupId(riskGroupId);
            var lendingProducts = new List<LendingProduct>();

            foreach (var productLending in productLendings)
            {
                var legalEntity = _legalEntityRepository.ReadById(productLending.LegalEntityId);
                var legalEntityAccount = _legalEntityAccountRepository.ReadById(productLending.LegalEntityAccountId);
                var products = _lookupTableManager.GetProductTypesForConcessionType("Lending");

                var mappedProduct = _mapper.Map<LendingProduct>(productLending);

                mappedProduct.CustomerName = legalEntity.CustomerName;
                mappedProduct.AccountNumber = legalEntityAccount.AccountNumber;
                mappedProduct.Product = products.First(_ => _.Id == productLending.ProductId).Description;
                mappedProduct.RiskGroupName = riskGroupName;

                lendingProducts.Add(mappedProduct);
            }

            return lendingProducts;
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