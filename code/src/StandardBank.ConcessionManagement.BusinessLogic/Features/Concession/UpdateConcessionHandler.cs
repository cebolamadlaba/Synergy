using System;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Update concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{UpdateConcessionCommand, Concession}" />
    public class UpdateConcessionHandler : IAsyncRequestHandler<UpdateConcession, Model.UserInterface.Concession>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The email manager
        /// </summary>
        private readonly IEmailManager _emailManager;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UpdateConcessionHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="userManager">The user manager.</param>
        public UpdateConcessionHandler(IConcessionManager concessionManager, IMediator mediator,
            ILogger<UpdateConcessionHandler> logger, IMapper mapper, ILookupTableManager lookupTableManager,
            IEmailManager emailManager, IUserManager userManager)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _emailManager = emailManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Model.UserInterface.Concession> Handle(UpdateConcession message)
        {
            var result = _concessionManager.UpdateConcession(message.Concession, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            message.Concession = _mapper.Map<Model.UserInterface.Concession>(result);

            message.Concession.Status = _lookupTableManager.GetStatusDescription(result.StatusId);
            message.Concession.SubStatus = _lookupTableManager.GetSubStatusDescription(result.SubStatusId);

            if (message.User.SelectedCentre?.Id > 0)
            {
                if (message.Concession.Status == "Pending" && !string.IsNullOrWhiteSpace(message.Concession.SubStatus))
                    await SendNotificationEmail(message, result);
            }
            else
            {
                _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"), "Consession # {0} has no selected center",
                    result.Id);
            }

            return message.Concession;
        }

        /// <summary>
        /// Sends the notification email.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private async Task SendNotificationEmail(UpdateConcession message, Model.Repository.Concession result)
        {
            switch (message.Concession.SubStatus)
            {
                case "BCM Pending":
                    await SendNotificationEmail(message, result, Constants.ApprovalStep.BCMApproval);
                    break;
                case "HO Pending":
                case "PCM Pending":
                    await SendNotificationEmail(message, result, Constants.ApprovalStep.PCMApproval);
                    break;
                case "PCM Approved With Changes":
                case "HO Approved With Changes":
                    SendApprovedWithChangesNotificationEmail(message);
                    break;
                default:
                    _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"),
                        "Consession # {0} is not in any relevant sub status",
                        result.Id);
                    break;
            }
        }

        /// <summary>
        /// Sends the approved with changes notification email.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendApprovedWithChangesNotificationEmail(UpdateConcession message)
        {
            var requestor = message.Concession.Requestor ?? _userManager.GetUser(message.Concession.RequestorId);

            BackgroundJob.Schedule(() =>
                _emailManager.SendConcessionAddedEmail(new ConcessionAddedEmail
                {
                    EmailAddress = requestor.EmailAddress,
                    FirstName = requestor.FirstName,
                    ConsessionId = message.Concession.ReferenceNumber
                }), DateTime.Now);
        }

        /// <summary>
        /// Sends the notification email.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="result">The result.</param>
        /// <param name="approvalStep">The approval step.</param>
        /// <returns></returns>
        private async Task SendNotificationEmail(UpdateConcession message, Model.Repository.Concession result,
            Constants.ApprovalStep approvalStep)
        {
            await _mediator.Publish(new ConcessionAdded
            {
                CenterId = message.User.SelectedCentre.Id,
                ConsessionId = result.ConcessionRef,
                ApprovalStep = approvalStep
            });
        }
    }
}