using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class DeleteUser : IRequest<int>
    {
        public string aNumber { get; set; }
    }
}
