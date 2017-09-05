using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    class CreateUserHandler : MediatR.IRequestHandler<CreateUser, int>
    {
      
        private readonly IUserManager userManager;

        public CreateUserHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        public int Handle(CreateUser message)
        {
            return userManager.CreateUser(message.User);
        }
    }
}
