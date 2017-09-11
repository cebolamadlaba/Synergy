using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class CreateUser : IRequest<int>, IAuditableCommand
    {
        public User User { get; set; }

        public User CurrentUser { get; set; }

        public AuditRecord AuditRecord { get; set; }
    }
}
