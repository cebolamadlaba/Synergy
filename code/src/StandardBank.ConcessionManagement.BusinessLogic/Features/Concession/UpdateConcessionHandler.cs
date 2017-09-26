using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
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
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public UpdateConcessionHandler(IConcessionManager concessionManager, IMediator mediator,
            ILoggerFactory loggerFactory, IMapper mapper, ILookupTableManager lookupTableManager)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _logger = loggerFactory.CreateLogger<UpdateConcessionHandler>();
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
                {
                    switch (message.Concession.SubStatus)
                    {
                        case "BCM Pending":
                            await TryAndSendEmail(message, result, Constants.ApprovalStep.BCMApproval);
                            break;
                        case "HO Pending":
                        case "PCM Pending":
                            await TryAndSendEmail(message, result, Constants.ApprovalStep.PCMApproval);
                            break;
                        case "PCM Approved With Changes":
                        case "HO Approved With Changes":
                            await TryAndSendEmail(message, result, Constants.ApprovalStep.RequestorApproval);
                            break;
                        default:
                            _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"), "Consession # {0} is not in any relevant sub status",
                                result.Id);
                            break;
                    }
                }
            }
            else
            {
                _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"), "Consession # {0} has no selected center",
                    result.Id);
            }

            return message.Concession;
        }

        /// <summary>
        /// Tries the and send email.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="result">The result.</param>
        /// <param name="approvalStep">The approval step.</param>
        /// <returns></returns>
        private async Task TryAndSendEmail(UpdateConcession message, Model.Repository.Concession result, Constants.ApprovalStep approvalStep)
        {
            try
            {
                await _mediator.Publish(new ConcessionAdded
                {
                    CenterId = message.User.SelectedCentre.Id,
                    ConsessionId = result.ConcessionRef,
                    ApprovalStep = approvalStep
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
