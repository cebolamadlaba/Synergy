using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    public class ConcessionAddedEventHandler : IAsyncNotificationHandler<ConcessionAddedEvent>
    {
        private IApprovalRoutingManager _approvalRoutingManager { get; }
        private IEmailManager _emailManager { get; }
        public ConcessionAddedEventHandler(IApprovalRoutingManager approvalRoutingManager, IEmailManager emailManager)
        {
            _approvalRoutingManager = approvalRoutingManager;
            _emailManager = emailManager;
        }
        public async Task Handle(ConcessionAddedEvent notification)
        {
           var approvers =  _approvalRoutingManager.GetApproversByRole(notification.CenterId, Model.Constants.ApprovalStep.BCMApproval);
            foreach (var approver in approvers)
            {
               await _emailManager.SendTemplatedEmail(approver.EmailAddress, "Pricing Tool: New Concession", null, Constants.EmailTemplates.NewConcession, new { Name = approver.FirstName , ConcessionId = notification.ConsessionId });
            }
           
        }

       
    }
}
