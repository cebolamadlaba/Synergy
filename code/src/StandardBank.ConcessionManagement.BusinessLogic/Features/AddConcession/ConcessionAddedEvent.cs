using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    public class ConcessionAddedEvent : INotification
    {
        public string ConsessionId { get; set; }
        public int CenterId { get; set; }
    }
}
