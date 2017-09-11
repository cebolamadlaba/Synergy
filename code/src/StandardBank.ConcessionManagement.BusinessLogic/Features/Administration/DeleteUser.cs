using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class DeleteUser : IRequest<int>, IAuditableCommand
    {
        public string aNumber { get; set; }

        public User CurrentUser { get; set; }

        public AuditRecord AuditRecord { get; set; }
    }
}
