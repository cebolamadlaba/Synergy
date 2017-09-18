using StandardBank.ConcessionManagement.Interface.Repository;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class DeleteUserHandler : MediatR.IRequestHandler<DeleteUser, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUserRepository userRepository, IUserManager userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public int Handle(DeleteUser message)
        {
            var user = _userManager.GetUser(message.aNumber);

            var mappedUser = _mapper.Map<Model.Repository.User>(user);
            mappedUser.IsActive = false;

            message.AuditRecord = new AuditRecord(mappedUser, message.CurrentUser, AuditType.Update);

            _userRepository.Update(mappedUser);

            return 1;
        }
    }
}
