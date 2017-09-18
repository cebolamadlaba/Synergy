using AutoMapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class UpdateUserHandler : MediatR.IRequestHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager _cacheManager;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, ICacheManager cacheManager)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
            _cacheManager = cacheManager;
        }
        public void Handle(UpdateUser message)
        {
            var aNumber = message.Model.ANumber;

            _cacheManager.Remove(CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));

            var model = mapper.Map<Model.Repository.User>(message.Model);

            message.AuditRecord = new AuditRecord(model, message.CurrentUser, AuditType.Update);

            _userRepository.UpdateUser(model);
        }
    }
}
