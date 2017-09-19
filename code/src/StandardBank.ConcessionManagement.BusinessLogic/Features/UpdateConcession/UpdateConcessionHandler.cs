using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.UpdateConcession
{
    /// <summary>
    /// Update concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{UpdateConcessionCommand, Concession}" />
    public class UpdateConcessionHandler : IAsyncRequestHandler<UpdateConcession, Concession>
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
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public UpdateConcessionHandler(IConcessionManager concessionManager, IMediator mediator,
            ILoggerFactory loggerFactory)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger<UpdateConcessionHandler>();
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Concession> Handle(UpdateConcession message)
        {
            var result = _concessionManager.UpdateConcession(message.Concession, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            message.Concession.Id = result.Id;
            message.Concession.SubStatusId = result.SubStatusId;

            if (message.User.SelectedCentre?.Id > 0)
                await TryAndSendEmail(message, result);
            else
                _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"), "Consession # {0} has no selected center", result.Id);

            return message.Concession;
        }

        /// <summary>
        /// Tries and sends the email, if it fails will just log the exception for now
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task TryAndSendEmail(UpdateConcession message, Model.Repository.Concession result)
        {
            try
            {
                await _mediator.Publish(new ConcessionAdded
                {
                    CenterId = message.User.SelectedCentre.Id,
                    ConsessionId = result.ConcessionRef
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
