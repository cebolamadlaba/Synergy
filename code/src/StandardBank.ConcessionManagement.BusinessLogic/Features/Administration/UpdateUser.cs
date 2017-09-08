using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    public class UpdateUser : IRequest, IAuditableCommand
    {
        public User Model { get; set; }

        public User CurrentUser { get; set; }

        public AuditRecord AuditRecord { get; set; }
    }
}
