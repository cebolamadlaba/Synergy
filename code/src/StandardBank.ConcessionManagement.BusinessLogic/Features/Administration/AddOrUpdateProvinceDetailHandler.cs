using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    public class AddOrUpdateProvinceDetailHandler : IAsyncRequestHandler<Administration.AddOrUpdateProvinceDetail, Model.UserInterface.Province>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IProvinceRepository _provinceRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateProvinceDetailHandler"/> class.
        /// </summary>
        /// <param name="provinceRepository">The province repository.</param>
        /// <param name="mapper">The mapper.</param>
        public AddOrUpdateProvinceDetailHandler(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Model.UserInterface.Province> Handle(AddOrUpdateProvinceDetail message)
        {
            var mappedProvince = _mapper.Map<Model.Repository.Province>(message.Province);

            if (message.Province.Id > 0)
            {
                _provinceRepository.Update(mappedProvince);
                message.AuditRecord = new AuditRecord(mappedProvince, message.User, AuditType.Update);
            }
            else
            {
                mappedProvince = _provinceRepository.Create(mappedProvince);
                message.Province.Id = mappedProvince.Id;
                message.AuditRecord = new AuditRecord(mappedProvince, message.User, AuditType.Insert);
            }

            return message.Province;
        }
    }
}
