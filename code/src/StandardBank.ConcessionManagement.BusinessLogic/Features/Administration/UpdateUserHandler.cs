using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class UpdateUserHandler : MediatR.IRequestHandler<UpdateUser>
    {
        private readonly IAdminRepository adminRepository;
        private readonly IMapper mapper;

        public UpdateUserHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.mapper = mapper;
        }
        public void Handle(UpdateUser message)
        {
           var model = mapper.Map<Model.Repository.UserModel>(message.Model);
           adminRepository.UpdateUser(model);
        }
    }
}
