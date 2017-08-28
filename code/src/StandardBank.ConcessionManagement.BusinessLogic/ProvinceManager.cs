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
            var provinces = new List<Province>();

            foreach (Model.Repository.Province province in _provinceRepository.ReadAll())
            {
                Province mappedProvince = _mapper.Map<Province>(province);
                provinces.Add(mappedProvince);
            }
            return provinces;
        }

        public Province MaintainProvince(Province province)
        {
            Model.Repository.Province returnedProvince;
            var mappedProvince = _mapper.Map<Model.Repository.Province>(province);
            
            if (province.Id > 0)
            {
                returnedProvince = mappedProvince;
                _provinceRepository.Update(mappedProvince);
            }
            else
            {
                returnedProvince = _provinceRepository.Create(mappedProvince);
            }
            return _mapper.Map<Province>(returnedProvince);
        }
    }
}
