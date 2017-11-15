using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Add concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{AddConcession, Concession}" />
    public class AddConcessionHandler : IAsyncRequestHandler<AddConcession, Model.UserInterface.Concession>
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
        private readonly ILogger<AddConcessionHandler> _logger;

        /// <summary>
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public AddConcessionHandler(IConcessionManager concessionManager, IMediator mediator,
            ILogger<AddConcessionHandler> logger, IRiskGroupRepository riskGroupRepository,
            ILookupTableManager lookupTableManager)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _logger = logger;
            _riskGroupRepository = riskGroupRepository;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Model.UserInterface.Concession> Handle(AddConcession message)
        {
            var result = _concessionManager.CreateConcession(message.Concession, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            message.Concession.ReferenceNumber = result.ConcessionRef;
            message.Concession.Id = result.Id;

            if (string.IsNullOrWhiteSpace(message.Concession.RiskGroupName))
                message.Concession.RiskGroupName = _riskGroupRepository.ReadById(result.RiskGroupId).RiskGroupName;

            if (string.IsNullOrWhiteSpace(message.Concession.ConcessionType))
                message.Concession.ConcessionType =
                    _lookupTableManager.GetConcessionType(result.ConcessionTypeId).Description;

            if (message.User.SelectedCentre?.Id > 0)
                await SendNotificationEmail(message, result);
            else
                _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"), "Consession # {0} has no selected center",
                    result.Id);

            return message.Concession;
        }

        /// <summary>
        /// Tries and sends the email, if it fails will just log the exception for now
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task SendNotificationEmail(AddConcession message, Model.Repository.Concession result)
        {
            await _mediator.Publish(new ConcessionAdded
            {
                CenterId = message.User.SelectedCentre.Id,
                ConsessionId = result.ConcessionRef,
                RiskGroupName = message.Concession.RiskGroupName,
                Product = message.Concession.ConcessionType,
                DateOfRequest = result.ConcessionDate.ToString("yyyy-MM-dd")
            });
        }
    }
}