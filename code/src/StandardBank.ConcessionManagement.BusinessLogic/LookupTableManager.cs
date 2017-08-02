using System;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;

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
        /// <param name="conditionTypeRepository"></param>
        public LookupTableManager(IStatusRepository statusRepository, ISubStatusRepository subStatusRepository,
            IReferenceTypeRepository referenceTypeRepository, IMarketSegmentRepository marketSegmentRepository,
            IProvinceRepository provinceRepository, IConcessionTypeRepository concessionTypeRepository,
            IProductRepository productRepository, IReviewFeeTypeRepository reviewFeeTypeRepository,
            IPeriodRepository periodRepository, IPeriodTypeRepository periodTypeRepository,
            IConditionTypeRepository conditionTypeRepository)
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
        }

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
                productTypes.Add(new ProductType
                {
                    Id = productType.Id,
                    ConcessionType = GetConcessionType(concessionTypeId),
                    Description = productType.Description
                });
            }

            return productTypes;
        }

        /// <summary>
        /// Gets the concession type of the id specified
        /// </summary>
        /// <param name="concessionTypeId"></param>
        /// <returns></returns>
        private ConcessionType GetConcessionType(int concessionTypeId)
        {
            var concessionType = _concessionTypeRepository.ReadById(concessionTypeId);

            return new ConcessionType
            {
                Code = concessionType.Code,
                Description = concessionType.Description,
                Id = concessionType.Id
            };
        }

        /// <summary>
        /// Gets the review fee types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReviewFeeType> GetReviewFeeTypes()
        {
            var reviewFeeTypes = _reviewFeeTypeRepository.ReadAll();

            foreach (var reviewFeeType in reviewFeeTypes.Where(_ => _.IsActive))
                yield return new ReviewFeeType
                {
                    Id = reviewFeeType.Id,
                    Description = reviewFeeType.Description
                };
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Period> GetPeriods()
        {
            var periods = _periodRepository.ReadAll();

            foreach (var period in periods.Where(_ => _.IsActive))
                yield return new Period
                {
                    Id = period.Id,
                    Description = period.Description
                };
        }

        /// <summary>
        /// Gets the period types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PeriodType> GetPeriodTypes()
        {
            var periodTypes = _periodTypeRepository.ReadAll();

            foreach (var periodType in periodTypes.Where(_ => _.IsActive))
                yield return new PeriodType
                {
                    Id = periodType.Id,
                    Description = periodType.Description
                };
        }

        /// <summary>
        /// Gets the condition types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConditionType> GetConditionTypes()
        {
            var conditionTypes = _concessionTypeRepository.ReadAll();

            foreach (var conditionType in conditionTypes.Where(_ => _.IsActive))
                yield return new ConditionType
                {
                    Id = conditionType.Id,
                    Description = conditionType.Description
                };
        }
    }
}
