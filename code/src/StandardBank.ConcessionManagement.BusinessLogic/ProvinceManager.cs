using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Provincr manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IProvinceManager" />
    public class ProvinceManager : IProvinceManager
    {
        /// <summary>
        /// The risk group repository
        /// </summary>
        private readonly IProvinceRepository _provinceRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingManager"/> class.
        /// </summary>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="mapper"></param>
        public ProvinceManager(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the risk group for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        public IEnumerable<Province> GetProvinces()
        {
            var provinces = _provinceRepository.ReadAll();
            return _mapper.Map<IEnumerable<Province>>(provinces);
        }
    }
}
