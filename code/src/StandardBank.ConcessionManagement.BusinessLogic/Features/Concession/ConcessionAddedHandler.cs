using System;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Threading.Tasks;
using Hangfire;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Concession added handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncNotificationHandler{StandardBank.ConcessionManagement.BusinessLogic.Features.Concession.ConcessionAdded}" />
    public class ConcessionAddedHandler : IAsyncNotificationHandler<ConcessionAdded>
    {
        /// <summary>
        /// The approval routing manager
        /// </summary>
        private readonly IApprovalRoutingManager _approvalRoutingManager;

        /// <summary>
        /// The email manager
        /// </summary>
        private readonly IEmailManager _emailManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionAddedHandler"/> class.
        /// </summary>
        /// <param name="approvalRoutingManager">The approval routing manager.</param>
        /// <param name="emailManager">The email manager.</param>
        public ConcessionAddedHandler(IApprovalRoutingManager approvalRoutingManager, IEmailManager emailManager)
        {
            _approvalRoutingManager = approvalRoutingManager;
            _emailManager = emailManager;
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns></returns>
        public async Task Handle(ConcessionAdded notification)
        {
            var approvers =
                _approvalRoutingManager.GetApproversByRole(notification.CenterId, notification.ApprovalStep);

            foreach (var approver in approvers)
            {
                BackgroundJob.Schedule(() =>
                    _emailManager.SendConcessionAddedEmail(new ConcessionAddedEmail
                    {
                        EmailAddress = approver.EmailAddress,
                        FirstName = approver.FirstName,
                        ConsessionId = notification.ConsessionId,
                        RiskGroupName = notification.RiskGroupName,
                        DateOfRequest = notification.DateOfRequest,
                        Product = notification.Product
                    }), DateTime.Now);
            }
        }
    }
}