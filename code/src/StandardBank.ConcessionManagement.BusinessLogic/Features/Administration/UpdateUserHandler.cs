using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class UpdateUserHandler : MediatR.IRequestHandler<UpdateUser>
    {
        private readonly IAdminRepository adminRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager _cacheManager;

        public UpdateUserHandler(IAdminRepository adminRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.adminRepository = adminRepository;
            this.mapper = mapper;
            _cacheManager = cacheManager;
        }
        public void Handle(UpdateUser message)
        {
            var aNumber = message.Model.ANumber;

            _cacheManager.Remove(CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));

            var model = mapper.Map<Model.Repository.UserModel>(message.Model);
           adminRepository.UpdateUser(model);
        }
    }
}
