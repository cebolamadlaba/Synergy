using StandardBank.ConcessionManagement.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class DeleteUserHandler : MediatR.IRequestHandler<DeleteUser, int>
    {
        private readonly IAdminRepository repository;

        public DeleteUserHandler(IAdminRepository repository)
        {
            this.repository = repository;
        }
        public int Handle(DeleteUser message)
        {
            return repository.DeleteUser(message.aNumber);
        }
    }
}
