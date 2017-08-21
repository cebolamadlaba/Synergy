using System;
using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    /// <summary>
    /// Add concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{AddConcessionCommand, Concession}" />
    public class AddConcessionCommandHandler : IAsyncRequestHandler<AddConcessionCommand, Concession>
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
        /// Initializes a new instance of the <see cref="AddConcessionCommandHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public AddConcessionCommandHandler(IConcessionManager concessionManager, IMediator mediator,
            ILoggerFactory loggerFactory)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger<AddConcessionCommandHandler>();
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Concession> Handle(AddConcessionCommand message)
        {
            var result = _concessionManager.CreateConcession(message.Concession, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            message.Concession.ReferenceNumber = result.ConcessionRef;
            message.Concession.Id = result.Id;
            if (message.User.SelectedCentre?.Id > 0)
                await TryAndSendEmail(message, result);
            else
                _logger.LogWarning(new EventId(1,"ApprovalEmailNotSent"),"Consession # {0} has no selected center",result.Id);

            return message.Concession;
        }

        /// <summary>
        /// Tries and sends the email, if it fails will just log the exception for now
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task TryAndSendEmail(AddConcessionCommand message, Model.Repository.Concession result)
        {
            try
            {
                await _mediator.Publish(new ConcessionAddedEvent
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
