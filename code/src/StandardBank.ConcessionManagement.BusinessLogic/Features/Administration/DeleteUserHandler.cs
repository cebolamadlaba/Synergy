using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class DeleteUserHandler : MediatR.IRequestHandler<DeleteUser, int>
    {
        private readonly IAdminRepository repository;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IAdminRepository repository, IUserManager userManager, IMapper mapper)
        {
            this.repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public int Handle(DeleteUser message)
        {
            var user = _userManager.GetUser(message.aNumber);

            var mappedUser = _mapper.Map<Model.Repository.User>(user);

            message.AuditRecord = new AuditRecord(mappedUser, message.CurrentUser, AuditType.Delete);

            return repository.DeleteUser(message.aNumber);
        }
    }
}
