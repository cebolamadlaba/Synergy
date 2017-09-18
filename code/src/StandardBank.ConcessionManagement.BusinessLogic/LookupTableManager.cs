using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using AccrualType = StandardBank.ConcessionManagement.Model.UserInterface.AccrualType;
using ChannelType = StandardBank.ConcessionManagement.Model.UserInterface.ChannelType;
using ConcessionType = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionType;
using ConditionProduct = StandardBank.ConcessionManagement.Model.UserInterface.ConditionProduct;
using ConditionType = StandardBank.ConcessionManagement.Model.UserInterface.ConditionType;
using Period = StandardBank.ConcessionManagement.Model.UserInterface.Period;
using PeriodType = StandardBank.ConcessionManagement.Model.UserInterface.PeriodType;
using ReviewFeeType = StandardBank.ConcessionManagement.Model.UserInterface.ReviewFeeType;
using TableNumber = StandardBank.ConcessionManagement.Model.UserInterface.TableNumber;
using TransactionType = StandardBank.ConcessionManagement.Model.UserInterface.TransactionType;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Look up table manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ILookupTableManager" />
    public class LookupTableManager : ILookupTableManager
    {
        /// <summary>
        /// The status repository
        /// </summary>
        private readonly IStatusRepository _statusRepository;

        /// <summary>
        /// The sub status repository
        /// </summary>
        private readonly ISubStatusRepository _subStatusRepository;

        /// <summary>
        /// The reference type repository
        /// </summary>
        private readonly IReferenceTypeRepository _referenceTypeRepository;

        /// <summary>
        /// The market segment repository
        /// </summary>
        private readonly IMarketSegmentRepository _marketSegmentRepository;

        /// <summary>
        /// The province repository
        /// </summary>
        private readonly IProvinceRepository _provinceRepository;

        /// <summary>
        /// The concession type repository
        /// </summary>
        private readonly IConcessionTypeRepository _concessionTypeRepository;

        /// <summary>
        /// The product repository
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// The review fee type repository
        /// </summary>
        private readonly IReviewFeeTypeRepository _reviewFeeTypeRepository;

        /// <summary>
        /// The period repository
        /// </summary>
        private readonly IPeriodRepository _periodRepository;

        /// <summary>
        /// The period type repository
        /// </summary>
        private readonly IPeriodTypeRepository _periodTypeRepository;

        /// <summary>
        /// The condition type repository
        /// </summary>
        private readonly IConditionTypeRepository _conditionTypeRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The condition product repository
        /// </summary>
        private readonly IConditionProductRepository _conditionProductRepository;

        /// <summary>
        /// The condition type product repository
        /// </summary>
        private readonly IConditionTypeProductRepository _conditionTypeProductRepository;

        /// <summary>
        /// The accrual type repository
        /// </summary>
        private readonly IAccrualTypeRepository _accrualTypeRepository;

        /// <summary>
        /// The channel type repository
        /// </summary>
        private readonly IChannelTypeRepository _channelTypeRepository;

        /// <summary>
        /// The transaction type repository
        /// </summary>
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        /// <summary>
        /// The table number repository
        /// </summary>
        private readonly ITableNumberRepository _tableNumberRepository;

        /// <summary>
        /// The relationship repository
        /// </summary>
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ICentreRepository centreRepository;
        private readonly IRegionRepository regionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupTableManager"/> class.
        /// </summary>
        /// <param name="statusRepository">The status repository.</param>
        /// <param name="subStatusRepository">The sub status repository.</param>
        /// <param name="referenceTypeRepository">The reference type repository.</param>
        /// <param name="marketSegmentRepository">The market segment repository.</param>
        /// <param name="provinceRepository">The province repository.</param>
        /// <param name="concessionTypeRepository">The concession type repository.</param>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="reviewFeeTypeRepository">The review fee type repository.</param>
        /// <param name="periodRepository">The period repository.</param>
        /// <param name="periodTypeRepository">The period type repository.</param>
        /// <param name="conditionTypeRepository">The condition type repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="conditionProductRepository">The condition product repository.</param>
        /// <param name="conditionTypeProductRepository">The condition type product repository.</param>
        /// <param name="accrualTypeRepository">The accrual type repository.</param>
        /// <param name="channelTypeRepository">The channel type repository.</param>
        /// <param name="transactionTypeRepository">The transaction type repository.</param>
        /// <param name="tableNumberRepository">The table number repository.</param>
        /// <param name="relationshipRepository">The relationship repository.</param>
        public LookupTableManager(IStatusRepository statusRepository, ISubStatusRepository subStatusRepository,
            IReferenceTypeRepository referenceTypeRepository, IMarketSegmentRepository marketSegmentRepository,
            IProvinceRepository provinceRepository, IConcessionTypeRepository concessionTypeRepository,
            IProductRepository productRepository, IReviewFeeTypeRepository reviewFeeTypeRepository,
            IPeriodRepository periodRepository, IPeriodTypeRepository periodTypeRepository,
            IConditionTypeRepository conditionTypeRepository, IMapper mapper,
            IConditionProductRepository conditionProductRepository,
            IConditionTypeProductRepository conditionTypeProductRepository,
            IAccrualTypeRepository accrualTypeRepository, IChannelTypeRepository channelTypeRepository,
            ITransactionTypeRepository transactionTypeRepository, ITableNumberRepository tableNumberRepository,
            IRelationshipRepository relationshipRepository, IRoleRepository roleRepository,
            ICentreRepository centreRepository, IRegionRepository regionRepository)
        {
            _statusRepository = statusRepository;
            _subStatusRepository = subStatusRepository;
            _referenceTypeRepository = referenceTypeRepository;
            _marketSegmentRepository = marketSegmentRepository;
            _provinceRepository = provinceRepository;
            _concessionTypeRepository = concessionTypeRepository;
            _productRepository = productRepository;
            _reviewFeeTypeRepository = reviewFeeTypeRepository;
            _periodRepository = periodRepository;
            _periodTypeRepository = periodTypeRepository;
            _conditionTypeRepository = conditionTypeRepository;
            _mapper = mapper;
            _conditionProductRepository = conditionProductRepository;
            _conditionTypeProductRepository = conditionTypeProductRepository;
            _accrualTypeRepository = accrualTypeRepository;
            _channelTypeRepository = channelTypeRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _tableNumberRepository = tableNumberRepository;
            _relationshipRepository = relationshipRepository;
            this.roleRepository = roleRepository;
            this.centreRepository = centreRepository;
            this.regionRepository = regionRepository;
        }

        public IEnumerable<Model.UserInterface.Role> GetRoles()=> _mapper.Map<IEnumerable<Model.UserInterface.Role>>(roleRepository.ReadAll());
       
        public IEnumerable<Model.UserInterface.Centre> GetCentres() => _mapper.Map<IEnumerable<Model.UserInterface.Centre>>(centreRepository.ReadAll());

        public IEnumerable<Model.UserInterface.Region> GetRegions() => _mapper.Map<IEnumerable<Model.UserInterface.Region>>(regionRepository.ReadAll());
        
        /// <summary>
        /// Gets the status identifier.
        /// </summary>
        /// <param name="statusName">Name of the status.</param>
        /// <returns></returns>
        public int GetStatusId(string statusName)
        {
            var statuses = _statusRepository.ReadAll();

            return statuses.First(_ => _.Description == statusName && _.IsActive).Id;
        }

        /// <summary>
        /// Gets the status description
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public string GetStatusDescription(int statusId)
        {
            var statuses = _statusRepository.ReadAll();

            return statuses.First(_ => _.Id == statusId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the sub status identifier.
        /// </summary>
        /// <param name="subStatusName">Name of the sub status.</param>
        /// <returns></returns>
        public int GetSubStatusId(string subStatusName)
        {
            var subStatuses = _subStatusRepository.ReadAll();

            return subStatuses.First(_ => _.Description == subStatusName && _.IsActive).Id;
        }

        /// <summary>
        /// Gets the sub status description
        /// </summary>
        /// <param name="subStatusId"></param>
        /// <returns></returns>
        public string GetSubStatusDescription(int subStatusId)
        {
            var subStatuses = _subStatusRepository.ReadAll();

            return subStatuses.First(_ => _.Id == subStatusId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the reference type name for the id specified
        /// </summary>
        /// <param name="referenceTypeId"></param>
        /// <returns></returns>
        public string GetReferenceTypeName(int referenceTypeId)
        {
            var referenceTypes = _referenceTypeRepository.ReadAll();

            return referenceTypes.First(_ => _.Id == referenceTypeId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the reference type id for the reference type name supplied
        /// </summary>
        /// <param name="referenceTypeName"></param>
        /// <returns></returns>
        public int GetReferenceTypeId(string referenceTypeName)
        {
            var referenceTypes = _referenceTypeRepository.ReadAll();

            return referenceTypes.First(_ => _.Description == referenceTypeName && _.IsActive).Id;
        }

        /// <summary>
        /// Gets the market segment name for the id specified
        /// </summary>
        /// <param name="marketSegmentId"></param>
        /// <returns></returns>
        public string GetMarketSegmentName(int marketSegmentId)
        {
            var marketSegments = _marketSegmentRepository.ReadAll();

            return marketSegments.First(_ => _.Id == marketSegmentId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the province name for the id specified
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public string GetProvinceName(int provinceId)
        {
            var provinces = _provinceRepository.ReadAll();

            return provinces.First(_ => _.Id == provinceId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the condition type name
        /// </summary>
        /// <param name="conditionTypeId"></param>
        /// <returns></returns>
        public string GetConditionTypeName(int conditionTypeId)
        {
            var conditionTypes = _conditionTypeRepository.ReadAll();

            return conditionTypes.First(_ => _.Id == conditionTypeId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the product type name
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public string GetProductTypeName(int productTypeId)
        {
            var productTypes = _productRepository.ReadAll();

            return productTypes.First(_ => _.Id == productTypeId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the period type name
        /// </summary>
        /// <param name="periodTypeId"></param>
        /// <returns></returns>
        public string GetPeriodTypeName(int periodTypeId)
        {
            var periodTypes = _periodTypeRepository.ReadAll();

            return periodTypes.First(_ => _.Id == periodTypeId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the period name
        /// </summary>
        /// <param name="periodId"></param>
        /// <returns></returns>
        public string GetPeriodName(int periodId)
        {
            var periods = _periodRepository.ReadAll();

            return periods.First(_ => _.Id == periodId && _.IsActive).Description;
        }

        /// <summary>
        /// Gets the concession type id for the code passed in
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetConcessionTypeId(string code)
        {
            var concessionTypes = _concessionTypeRepository.ReadAll();

            return concessionTypes.First(_ => _.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase)).Id;
        }

        /// <summary>
        /// Gets the product type for the concession type specified
        /// </summary>
        /// <param name="concessionType"></param>
        /// <returns></returns>
        public IEnumerable<ProductType> GetProductTypesForConcessionType(string concessionType)
        {
            var productTypes = new List<ProductType>();

            var concessionTypeId = GetConcessionTypeId(concessionType);

            foreach (var productType in _productRepository.ReadByConcessionTypeIdIsActive(concessionTypeId, true))
            {
                var mappedProductType = _mapper.Map<ProductType>(productType);
                mappedProductType.ConcessionType = GetConcessionType(concessionTypeId);
                productTypes.Add(mappedProductType);
            }

            return productTypes;
        }

        /// <summary>
        /// Gets the concession type of the id specified
        /// </summary>
        /// <param name="concessionTypeId"></param>
        /// <returns></returns>
        public ConcessionType GetConcessionType(int concessionTypeId)
        {
            return _mapper.Map<ConcessionType>(_concessionTypeRepository.ReadById(concessionTypeId));
        }

        /// <summary>
        /// Gets the review fee types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReviewFeeType> GetReviewFeeTypes()
        {
            var reviewFeeTypes = _reviewFeeTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<ReviewFeeType>>(reviewFeeTypes.Where(_ => _.IsActive));
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Period> GetPeriods()
        {
            var periods = _periodRepository.ReadAll();
            return _mapper.Map<IEnumerable<Period>>(periods.Where(_ => _.IsActive));
        }

        /// <summary>
        /// Gets the period types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PeriodType> GetPeriodTypes()
        {
            var periodTypes = _periodTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<PeriodType>>(periodTypes.Where(_ => _.IsActive));
        }

        /// <summary>
        /// Gets the condition types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConditionType> GetConditionTypes()
        {
            var mappedConditionTypes = new List<ConditionType>();
            var conditionTypes = _conditionTypeRepository.ReadAll();
            var conditionProducts = _conditionProductRepository.ReadAll().Where(_ => _.IsActive);
            var conditionTypeProducts = _conditionTypeProductRepository.ReadAll().Where(_ => _.IsActive);

            foreach (var conditionType in conditionTypes.Where(_ => _.IsActive))
            {
                var mappedConditionType = _mapper.Map<ConditionType>(conditionType);
                mappedConditionType.ConditionProducts =
                    GetConditionProducts(conditionType.Id, conditionProducts, conditionTypeProducts);
                mappedConditionTypes.Add(mappedConditionType);
            }

            return mappedConditionTypes;
        }

        /// <summary>
        /// Gets the accrual types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AccrualType> GetAccrualTypes()
        {
            var accrualTypes = _accrualTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<AccrualType>>(accrualTypes.Where(_ => _.IsActive));
        }

        /// <summary>
        /// Gets the channel types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChannelType> GetChannelTypes()
        {
            var channelTypes = _channelTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<ChannelType>>(channelTypes.Where(_ => _.IsActive));
        }

        /// <summary>
        /// Gets the transaction type description.
        /// </summary>
        /// <param name="transactionTypeId">The transaction type identifier.</param>
        /// <returns></returns>
        public string GetTransactionTypeDescription(int transactionTypeId)
        {
            var transactionType = _transactionTypeRepository.ReadById(transactionTypeId);

            return transactionType.IsActive ? transactionType.Description : string.Empty;
        }

        /// <summary>
        /// Gets the type of the transaction types for concession.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        public IEnumerable<TransactionType> GetTransactionTypesForConcessionType(string concessionType)
        {
            var transactionTypes = new List<TransactionType>();

            var concessionTypeId = GetConcessionTypeId(concessionType);

            foreach (var transactionType in _transactionTypeRepository.ReadByConcessionTypeIdIsActive(concessionTypeId, true))
            {
                var mappedTransactionType = _mapper.Map<TransactionType>(transactionType);
                mappedTransactionType.ConcessionType = concessionType;
                transactionTypes.Add(mappedTransactionType);
            }

            return transactionTypes;
        }

        /// <summary>
        /// Gets the table numbers.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        public IEnumerable<TableNumber> GetTableNumbers(string concessionType)
        {
            var concessionTypeId = GetConcessionTypeId(concessionType);
            var tableNumbers = _tableNumberRepository.ReadAll();

            return _mapper.Map<IEnumerable<TableNumber>>(tableNumbers
                .Where(_ => _.IsActive && _.ConcessionTypeId == concessionTypeId).OrderBy(_ => _.TariffTable));
        }

        /// <summary>
        /// Gets the relationship identifier.
        /// </summary>
        /// <param name="relationshipDescription">The relationship description.</param>
        /// <returns></returns>
        public int GetRelationshipId(string relationshipDescription)
        {
            var relationships = _relationshipRepository.ReadAll();

            return relationships
                .First(_ => _.Description.Equals(relationshipDescription, StringComparison.CurrentCultureIgnoreCase))
                .Id;
        }

        /// <summary>
        /// Gets the relationship description.
        /// </summary>
        /// <param name="relationshipId">The relationship identifier.</param>
        /// <returns></returns>
        public string GetRelationshipDescription(int relationshipId)
        {
            var relationships = _relationshipRepository.ReadAll();

            return relationships.First(_ => _.Id == relationshipId).Description;
        }

        /// <summary>
        /// Gets the name of the condition product.
        /// </summary>
        /// <param name="conditionProductId">The condition product identifier.</param>
        /// <returns></returns>
        public string GetConditionProductName(int conditionProductId)
        {
            var conditionProduct = _conditionProductRepository.ReadById(conditionProductId);

            return conditionProduct.Description;
        }

        /// <summary>
        /// Gets the name of the review fee type.
        /// </summary>
        /// <param name="reviewFeeTypeId">The review fee type identifier.</param>
        /// <returns></returns>
        public string GetReviewFeeTypeName(int reviewFeeTypeId)
        {
            var reviewFeeType = _reviewFeeTypeRepository.ReadById(reviewFeeTypeId);

            return reviewFeeType.Description;
        }

        /// <summary>
        /// Gets the name of the channel type.
        /// </summary>
        /// <param name="channelTypeId">The channel type identifier.</param>
        /// <returns></returns>
        public string GetChannelTypeName(int channelTypeId)
        {
            var channelType = _channelTypeRepository.ReadById(channelTypeId);

            return channelType.Description;
        }

        /// <summary>
        /// Gets the condition products
        /// </summary>
        /// <param name="conditionTypeId"></param>
        /// <param name="conditionProducts"></param>
        /// <param name="conditionTypeProducts"></param>
        /// <returns></returns>
        private IEnumerable<ConditionProduct> GetConditionProducts(int conditionTypeId,
            IEnumerable<Model.Repository.ConditionProduct> conditionProducts,
            IEnumerable<ConditionTypeProduct> conditionTypeProducts)
        {
            var conditionTypeProductsForConditionType =
                conditionTypeProducts.Where(_ => _.ConditionTypeId == conditionTypeId);

            foreach (var conditionTypeProduct in conditionTypeProductsForConditionType)
                yield return _mapper.Map<ConditionProduct>(
                    conditionProducts.First(_ => _.Id == conditionTypeProduct.ConditionProductId));
        }
    }
}
