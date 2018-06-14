using System;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Update concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{UpdateConcessionCommand, Concession}" />
    public class ForwardConcessionHandler : IAsyncRequestHandler<ForwardConcession, Model.UserInterface.Concession>
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
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ForwardConcessionHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="emailManager">The email manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        public ForwardConcessionHandler(IConcessionManager concessionManager, IMediator mediator,
            ILogger<ForwardConcessionHandler> logger, IMapper mapper, ILookupTableManager lookupTableManager,
            IEmailManager emailManager, IUserManager userManager, IRiskGroupRepository riskGroupRepository)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _emailManager = emailManager;
            _userManager = userManager;
            _riskGroupRepository = riskGroupRepository;
            _logger = logger;           
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
       
        public async Task<Model.UserInterface.Concession> Handle(ForwardConcession message)
        {                 

            if (message.User.SelectedCentre?.Id > 0)
            {               

                if (message.Concession.IsInProgressForwarding)// && message.Concession.SubStatusId == _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.PcmPending))
                    SendForwardNotificationEmail(message);
            }
            else
            {
                _logger.LogWarning(new EventId(1, "ForwardEmailNotSent"), "Consession # {0} has no selected center", message.Concession.Id);
            }

            return message.Concession;
        }

        

        private void SendForwardNotificationEmail(ForwardConcession message)
        {
            var requestor = message.Concession.Requestor ?? _userManager.GetUser(message.Concession.RequestorId);

            BackgroundJob.Schedule(() =>
                _emailManager.SendForwardedConcessionEmail(new ApprovedConcessionEmail
                {
                  
                    EmailAddress = requestor.EmailAddress,
                    ConcessionId = message.Concession.ReferenceNumber,
                    Name = requestor.FirstName,
                    DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                    DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                    RiskGroupName = message.Concession.RiskGroupName,
                    Product = message.Concession.ConcessionType
                }), DateTime.Now);
        }
        
    }
}