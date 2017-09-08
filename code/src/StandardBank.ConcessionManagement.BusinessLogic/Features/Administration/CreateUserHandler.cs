using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Threading.Tasks;
using AutoMapper;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUser, int>
    {
      
        private readonly IUserManager userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserManager userManager, IMapper mapper)
        {
            this.userManager = userManager;
            _mapper = mapper;
        }
        public Task<int> Handle(CreateUser message)
        {
            var id = userManager.CreateUser(message.User);

            var mappedUser = _mapper.Map<Model.Repository.User>(message.User);
            mappedUser.Id = id;
            message.AuditRecord = new AuditRecord(mappedUser, message.CurrentUser, AuditType.Insert);

            return Task.FromResult(id);
        }
    }
}
