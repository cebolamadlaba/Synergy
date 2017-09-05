using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class CreateUser : IRequest<int>
    {
        public UserModel User { get; set; }
    }
}
