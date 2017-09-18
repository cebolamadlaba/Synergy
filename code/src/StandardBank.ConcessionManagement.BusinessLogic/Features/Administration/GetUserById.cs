using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class GetUserById : IRequest<User>
    {
        public int Id { get; set; }
    }
}
