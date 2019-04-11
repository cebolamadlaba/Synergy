using System;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
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
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        private readonly IAENumberUserRepository _aeNumberUserRepository;

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
        /// <param name="riskGroupRepository">The risk group repository.</param>
        public UpdateConcessionHandler(IConcessionManager concessionManager, IMediator mediator,
            ILogger<UpdateConcessionHandler> logger, IMapper mapper, ILookupTableManager lookupTableManager,
            IEmailManager emailManager, IUserManager userManager, IRiskGroupRepository riskGroupRepository,
            IAENumberUserRepository aeNumberUserRepository)
        {
            _concessionManager = concessionManager;
            _mediator = mediator;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _emailManager = emailManager;
            _userManager = userManager;
            _riskGroupRepository = riskGroupRepository;
            _logger = logger;
            _aeNumberUserRepository = aeNumberUserRepository;
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
            message.Concession.ConcessionType =
                _lookupTableManager.GetConcessionType(result.ConcessionTypeId).Description;

            if (string.IsNullOrWhiteSpace(message.Concession.RiskGroupName))
                message.Concession.RiskGroupName = _riskGroupRepository.ReadById(result.RiskGroupId).RiskGroupName;

            if (message.User.SelectedCentre?.Id > 0)
            {
                if (message.Concession.Status == Constants.ConcessionStatus.Pending && !string.IsNullOrWhiteSpace(message.Concession.SubStatus))
                    await SendNotificationEmail(message, result);

                if (message.Concession.Status == Constants.ConcessionStatus.Approved ||
                    message.Concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
                    SendApprovedNotificationEmail(message);

                if (message.Concession.Status == Constants.ConcessionStatus.Declined)
                    SendDeclinedNotificationEmail(message);
            }
            else
            {
                _logger.LogWarning(new EventId(1, "ApprovalEmailNotSent"), "Consession # {0} has no selected center",
                    result.Id);
            }

            return message.Concession;
        }

        /// <summary>
        /// Sends the declined notification email.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendDeclinedNotificationEmail(UpdateConcession message)
        {
            AENumberUser aeNumberUser = _aeNumberUserRepository.ReadById(message.Concession.AENumberUserId.Value);

            var requestor = _userManager.GetUser(aeNumberUser.UserId);

            //send notifcation to Account executive, if it is an assitant
            if (requestor.IsAdminAssistant && requestor.AccountExecutiveUserId != null)
            {
                BackgroundJob.Schedule(() =>
               _emailManager.SendDeclinedConcessionEmail(new DeclinedConcessionEmail
               {
                   EmailAddress = requestor.AccountExecutive.EmailAddress,
                   ConcessionId = message.Concession.ReferenceNumber,
                   Name = requestor.AccountExecutive.FirstName,
                   Approver = message.User.FullName,
                   DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                   DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                   RiskGroupName = message.Concession.RiskGroupName,
                   Product = message.Concession.ConcessionType
               }), DateTime.Now);

            }
            //send notifcation to all assitants, if it is an Account Executive
            else if (requestor.AccountAssistants != null && requestor.AccountAssistants.Count > 0)
            {
                foreach (var aa in requestor.AccountAssistants)
                {
                    var aauser = _userManager.GetUser(aa.AccountAssistantUserId);

                    BackgroundJob.Schedule(() =>
                      _emailManager.SendDeclinedConcessionEmail(new DeclinedConcessionEmail
                      {
                          EmailAddress = aauser.EmailAddress,
                          ConcessionId = message.Concession.ReferenceNumber,
                          Name = aauser.FirstName,
                          Approver = message.User.FullName,
                          DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                          DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                          RiskGroupName = message.Concession.RiskGroupName,
                          Product = message.Concession.ConcessionType
                      }), DateTime.Now);
                }
            }

            //also send the notifcation to the original requestor

            BackgroundJob.Schedule(() =>
                _emailManager.SendDeclinedConcessionEmail(new DeclinedConcessionEmail
                {
                    EmailAddress = requestor.EmailAddress,
                    Name = requestor.FirstName,
                    ConcessionId = message.Concession.ReferenceNumber,
                    Approver = message.User.FullName,
                    DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                    DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                    RiskGroupName = message.Concession.RiskGroupName,
                    Product = message.Concession.ConcessionType
                }), DateTime.Now);
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
                case Constants.ConcessionSubStatus.BcmPending:
                    await SendNotificationEmail(message, result, Constants.ApprovalStep.BCMApproval);
                    break;
                case Constants.ConcessionSubStatus.HoPending:
                case Constants.ConcessionSubStatus.PcmPending:
                    await SendNotificationEmail(message, result, Constants.ApprovalStep.PCMApproval);
                    break;
                case Constants.ConcessionSubStatus.PcmApprovedWithChanges:
                case Constants.ConcessionSubStatus.HoApprovedWithChanges:
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
            AENumberUser aeNumberUser = _aeNumberUserRepository.ReadById(message.Concession.AENumberUserId.Value);

            var requestor = _userManager.GetUser(aeNumberUser.UserId);

            //send notifcation to Account executive, if it is an assitant
            if (requestor.IsAdminAssistant && requestor.AccountExecutiveUserId != null)
            {
                BackgroundJob.Schedule(() =>
                   _emailManager.SendApprovedWithChangesConcessionEmail(new ApprovedConcessionEmail
                   {
                       EmailAddress = requestor.AccountExecutive.EmailAddress,
                       ConcessionId = message.Concession.ReferenceNumber,
                       Name = requestor.AccountExecutive.FirstName,
                       DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                       DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                       RiskGroupName = message.Concession.RiskGroupName,
                       Product = message.Concession.ConcessionType
                   }), DateTime.Now);

            }
            //send notifcation to all assitants, if it is an Account Executive
            else if (requestor.AccountAssistants != null && requestor.AccountAssistants.Count > 0)
            {
                foreach (var aa in requestor.AccountAssistants)
                {
                    var aauser = _userManager.GetUser(aa.AccountAssistantUserId);

                    //check if AA is Trade or Bol, filter based on sub role
                    if (aauser.SubRoleId.HasValue)
                    {
                        if(aauser.SubRoleId == (int)Constants.RoleSubRole.BolUser
                                && message.Concession.ConcessionType == Constants.RoleSubRoleType.Bol)
                        {
                            SendNotificationBasedOnSubRole(aauser, message);
                        }
                        else
                        {
                            SendNotificationBasedOnSubRole(aauser, message);
                        }
                    }
                    else
                    {

                        BackgroundJob.Schedule(() =>
                          _emailManager.SendApprovedWithChangesConcessionEmail(new ApprovedConcessionEmail
                          {
                              EmailAddress = aauser.EmailAddress,
                              ConcessionId = message.Concession.ReferenceNumber,
                              Name = aauser.FirstName,
                              DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                              DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                              RiskGroupName = message.Concession.RiskGroupName,
                              Product = message.Concession.ConcessionType
                          }), DateTime.Now);
                    }
                }
            }

            //also send the notifcation to the original requestor
            BackgroundJob.Schedule(() =>
                _emailManager.SendApprovedWithChangesConcessionEmail(new ApprovedConcessionEmail
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

        /// <summary>
        /// Sends the approved notification email.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendApprovedNotificationEmail(UpdateConcession message)
        {
            AENumberUser aeNumberUser = _aeNumberUserRepository.ReadById(message.Concession.AENumberUserId.Value);

            var requestor = _userManager.GetUser(aeNumberUser.UserId);

            //send notifcation to Account executive, if it is an assitant
            if (requestor.IsAdminAssistant && requestor.AccountExecutiveUserId != null)
            {
                BackgroundJob.Schedule(() =>
               _emailManager.SendApprovedConcessionEmail(new ApprovedConcessionEmail
               {
                   EmailAddress = requestor.AccountExecutive.EmailAddress,
                   ConcessionId = message.Concession.ReferenceNumber,
                   Name = requestor.AccountExecutive.FirstName,
                   DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                   DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                   RiskGroupName = message.Concession.RiskGroupName,
                   Product = message.Concession.ConcessionType
               }), DateTime.Now);

            }
            //send notifcation to all assitants, if it is an Account Executive
            else if (requestor.AccountAssistants != null && requestor.AccountAssistants.Count > 0)
            {
                foreach (var aa in requestor.AccountAssistants)
                {
                    var aauser = _userManager.GetUser(aa.AccountAssistantUserId);

                    //check if AA is Trade or Bol, filter based on sub role
                    if(aauser.SubRoleId.HasValue)
                    {                       
                        if (aauser.SubRoleId==(int)Constants.RoleSubRole.BolUser
                            && message.Concession.ConcessionType==Constants.RoleSubRoleType.Bol)
                        {
                            SendNotificationBasedOnSubRole(aauser,message);
                        }
                        else
                        {
                            SendNotificationBasedOnSubRole(aauser, message);
                        }

                    }
                    else
                    {
                        BackgroundJob.Schedule(() =>
                        _emailManager.SendApprovedConcessionEmail(new ApprovedConcessionEmail
                        {
                            EmailAddress = aauser.EmailAddress,
                            ConcessionId = message.Concession.ReferenceNumber,
                            Name = aauser.FirstName,
                            DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                            DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                            RiskGroupName = message.Concession.RiskGroupName,
                            Product = message.Concession.ConcessionType
                        }), DateTime.Now);
                    }                
                }
            }

            //also sen the notifcation to the original requestor
            BackgroundJob.Schedule(() =>
                _emailManager.SendApprovedConcessionEmail(new ApprovedConcessionEmail
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
                ApprovalStep = approvalStep,
                RiskGroupName = message.Concession.RiskGroupName,
                Product = message.Concession.ConcessionType,
                DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd")
            });
        }

        /// <summary>
        /// Sends the approved notification email.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendNotificationBasedOnSubRole(Model.UserInterface.User usr, UpdateConcession message)
        {
            BackgroundJob.Schedule(() =>
                _emailManager.SendApprovedConcessionEmail(new ApprovedConcessionEmail
                {
                    EmailAddress = usr.EmailAddress,
                    ConcessionId = message.Concession.ReferenceNumber,
                    Name = usr.FirstName,
                    DateOfRequest = message.Concession.DateOpened.ToString("yyyy-MM-dd"),
                    DateActioned = DateTime.Now.ToString("yyyy-MM-dd"),
                    RiskGroupName = message.Concession.RiskGroupName,
                    Product = message.Concession.ConcessionType
                }), DateTime.Now);

        }
    }
}