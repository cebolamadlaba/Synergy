using System;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Threading.Tasks;
using Hangfire;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
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
        /// Gets the approval routing manager.
        /// </summary>
        /// <value>
        /// The approval routing manager.
        /// </value>
        private IApprovalRoutingManager _approvalRoutingManager { get; }

        /// <summary>
        /// Gets the email manager.
        /// </summary>
        /// <value>
        /// The email manager.
        /// </value>
        private IEmailManager _emailManager { get; }

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
                        ConsessionId = notification.ConsessionId
                    }), DateTime.Now);
            }

        }
    }
}