using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using StandardBank.ConcessionManagement.Model;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    public class ConcessionAdded : INotification
    {
        public string ConsessionId { get; set; }
        public int CenterId { get; set; }

        /// <summary>
        /// Gets or sets the approval step.
        /// </summary>
        /// <value>
        /// The approval step.
        /// </value>
        public Constants.ApprovalStep ApprovalStep { get; set; }
    }
}
