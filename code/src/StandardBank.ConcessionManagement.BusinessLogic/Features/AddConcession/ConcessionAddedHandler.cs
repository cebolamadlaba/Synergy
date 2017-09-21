using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    public class ConcessionAddedHandler : IAsyncNotificationHandler<ConcessionAdded>
    {
        private IApprovalRoutingManager _approvalRoutingManager { get; }
        private IEmailManager _emailManager { get; }

        public ConcessionAddedHandler(IApprovalRoutingManager approvalRoutingManager, IEmailManager emailManager)
        {
            _approvalRoutingManager = approvalRoutingManager;
            _emailManager = emailManager;
        }

        public async Task Handle(ConcessionAdded notification)
        {
            var approvers =
                _approvalRoutingManager.GetApproversByRole(notification.CenterId, notification.ApprovalStep);

            foreach (var approver in approvers)
            {
                await _emailManager.SendTemplatedEmail(approver.EmailAddress, "Pricing Tool: New Concession", null,
                    Constants.EmailTemplates.NewConcession,
                    new { Name = approver.FirstName, ConcessionId = notification.ConsessionId });
            }

        }


    }
}