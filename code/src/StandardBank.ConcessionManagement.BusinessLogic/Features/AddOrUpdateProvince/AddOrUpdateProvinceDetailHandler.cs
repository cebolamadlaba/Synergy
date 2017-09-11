using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateProvinceDetail
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    public class AddOrUpdateProvinceDetailHandler : IAsyncRequestHandler<AddOrUpdateProvinceDetail, Model.UserInterface.Province>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateProvinceDetailHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
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
