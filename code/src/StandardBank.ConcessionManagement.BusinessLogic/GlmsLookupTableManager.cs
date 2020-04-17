using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using BaseRateCode = StandardBank.ConcessionManagement.Model.UserInterface.BaseRateCode;
using GlmsGroup = StandardBank.ConcessionManagement.Model.UserInterface.GlmsGroup;
using InterestPricingCategory = StandardBank.ConcessionManagement.Model.UserInterface.InterestPricingCategory;
using InterestType = StandardBank.ConcessionManagement.Model.UserInterface.InterestType;
using RateType = StandardBank.ConcessionManagement.Model.UserInterface.RateType;
using SlabType = StandardBank.ConcessionManagement.Model.UserInterface.SlabType;


namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Look up table manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IGlmsLookupTableManager" />
    public class GlmsLookupTableManager : IGlmsLookupTableManager
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The glms Group repository
        /// </summary>
        private readonly IGlmsGroupRepository _glmsGroupRepository;

        /// <summary>
        /// The Interest Type repository
        /// </summary>
        private readonly IInterestTypeRepository _interestTypeRepository;

        /// <summary>
        /// The Interest Pricing Category repository
        /// </summary>
        private readonly IInterestPricingCategoryRepository _interestPricingCategoryRepository;

        /// <summary>
        /// The Rate Type repository
        /// </summary>
        private readonly IRateTypeRepository _rateTypeRepository;

        /// <summary>
        /// The Slab Type repository
        /// </summary>
        private readonly ISlabTypeRepository _slabTypeRepository;

        /// <summary>
        /// The Slab Type repository
        /// </summary>
        private readonly IBaseRateCodeRepository _baseRateCodeRepository;

        /// <summary>
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlmsLookupTableManager"/> class.

        public GlmsLookupTableManager(IMapper mapper,
            IGlmsGroupRepository glmsGroupRepository,
            IInterestPricingCategoryRepository interestPricingCategoryRepository,
            IInterestTypeRepository interestTypeRepository,
            IRateTypeRepository rateTypeRepository,
            ISlabTypeRepository slabTypeRepository,
            IBaseRateCodeRepository baseRateCodeRepository)
        {
            _mapper = mapper;
            _glmsGroupRepository = glmsGroupRepository;
            _interestPricingCategoryRepository = interestPricingCategoryRepository;
            _interestTypeRepository = interestTypeRepository;
            _rateTypeRepository = rateTypeRepository;
            _slabTypeRepository = slabTypeRepository;
            _baseRateCodeRepository = baseRateCodeRepository;

        }

        public IEnumerable<GlmsGroup> GetGlmsGroups()
        {
            var glmsGroup = _glmsGroupRepository.ReadAll();
            return _mapper.Map<IEnumerable<GlmsGroup>>(glmsGroup);
        }

        public IEnumerable<GlmsGroup> GetGlmsGroups(int riskGroupNumber, int? sapBpId)
        {
            var glmsGroup = _glmsGroupRepository.ReadAllByRiskGroupAndOrSapBpId(riskGroupNumber, sapBpId);
            return _mapper.Map<IEnumerable<GlmsGroup>>(glmsGroup);
        }

        public IEnumerable<InterestPricingCategory> GetInterestPricingCategories()
        {
            var interestCategory = _interestPricingCategoryRepository.ReadAll();
            return _mapper.Map<IEnumerable<InterestPricingCategory>>(interestCategory);
        }

        public IEnumerable<InterestType> GetInterestTypes()
        {
            var results = _interestTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<InterestType>>(results);
        }

        public IEnumerable<RateType> GetRateTypes()
        {
            var results = _rateTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<RateType>>(results);
        }

        public IEnumerable<SlabType> GetSlabTypes()
        {
            var results = _slabTypeRepository.ReadAll();
            return _mapper.Map<IEnumerable<SlabType>>(results);
        }

        public IEnumerable<BaseRateCode> GetBaseRateCodes()
        {
            var results = _baseRateCodeRepository.ReadAll();
            return _mapper.Map<IEnumerable<BaseRateCode>>(results);
        }

    }
}