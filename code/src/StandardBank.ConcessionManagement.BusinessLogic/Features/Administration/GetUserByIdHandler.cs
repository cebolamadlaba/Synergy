using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class GetUserByIdHandler : MediatR.IRequestHandler<GetUserById, User>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public User Handle(GetUserById message)
        {
            return _userManager.GetUser(message.Id);
        }
    }
}
