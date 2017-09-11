using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class GetUserById : IRequest<User>
    {
        public int Id { get; set; }
    }
}
