using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUser, int>
    {
      
        private readonly IUserManager userManager;

        public CreateUserHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        public Task<int> Handle(CreateUser message)
        {
            var id = userManager.CreateUser(message.User);
            return Task.FromResult(id);
        }
    }
}
