using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using Concession = StandardBank.ConcessionManagement.Model.Repository.Concession;
using ConcessionComment = StandardBank.ConcessionManagement.Model.Repository.ConcessionComment;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.Repository.ConcessionRelationship;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Concession manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IConcessionManager" />
    public class ConcessionManager : IConcessionManager
    {
        /// <summary>
        /// The concession repository
        /// </summary>
        private readonly IConcessionRepository _concessionRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The risk group repository
        /// </summary>
        private readonly IRiskGroupRepository _riskGroupRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The concession condition repository
        /// </summary>
        private readonly IConcessionConditionRepository _concessionConditionRepository;

        /// <summary>
        /// The concession comment repository
        /// </summary>
        private readonly IConcessionCommentRepository _concessionCommentRepository;

        /// <summary>
        /// The concession relationship repository
        /// </summary>
        private readonly IConcessionRelationshipRepository _concessionRelationshipRepository;

        /// <summary>
        /// The audit repository
        /// </summary>
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// The Role sub role repository
        /// </summary>
        private readonly IRoleSubRoleRepository _roleSubRoleRepository;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        private readonly IAENumberUserManager _aeNumberUserManager;

        /// <summary>
        /// The concession inbox view repository
        /// </summary>
        private readonly IConcessionInboxViewRepository _concessionInboxViewRepository;

        /// <summary>
        /// The concession detail repository
        /// </summary>
        private readonly IConcessionDetailRepository _concessionDetailRepository;

        /// <summary>
        /// The concession condition view repository
        /// </summary>
        private readonly IConcessionConditionViewRepository _concessionConditionViewRepository;

        /// <summary>
        /// The misc performance repository
        /// </summary>
        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        /// <summary>
        /// The centre repository
        /// </summary>
        private readonly ICentreRepository _centreRepository;

        private readonly IPrimeRateRepository _primeRateRepository;

        private readonly IConcessionLetterRepository _concessionLetterRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionManager"/> class.
        /// </summary>
        /// <param name="concessionRepository">The concession repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="concessionConditionRepository">The concession condition repository.</param>
        /// <param name="concessionCommentRepository">The concession comment repository.</param>
        /// <param name="concessionRelationshipRepository">The concession relationship repository.</param>
        /// <param name="auditRepository">The audit repository.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="concessionInboxViewRepository">The concession inbox view repository.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        /// <param name="concessionConditionViewRepository">The concession condition view repository.</param>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        /// <param name="centreRepository">The centre repository.</param>
        ///  <param name="roleSubRoleRepository">The Role Sub Role repository.</param>
        public ConcessionManager(IConcessionRepository concessionRepository, ILookupTableManager lookupTableManager,
            IRiskGroupRepository riskGroupRepository, IMapper mapper,
            IConcessionConditionRepository concessionConditionRepository,
            IConcessionCommentRepository concessionCommentRepository,
            IConcessionRelationshipRepository concessionRelationshipRepository, IAuditRepository auditRepository,
            IUserManager userManager, IConcessionInboxViewRepository concessionInboxViewRepository,
            IConcessionDetailRepository concessionDetailRepository,
            IConcessionConditionViewRepository concessionConditionViewRepository,
            IMiscPerformanceRepository miscPerformanceRepository, ICentreRepository centreRepository, IPrimeRateRepository primeRateRepository, IConcessionLetterRepository concessionLetterRepository,
            IAENumberUserManager aeNumberUserManager, IRoleSubRoleRepository roleSubRoleRepository)
        {
            _concessionRepository = concessionRepository;
            _lookupTableManager = lookupTableManager;
            _riskGroupRepository = riskGroupRepository;
            _mapper = mapper;
            _concessionConditionRepository = concessionConditionRepository;
            _concessionCommentRepository = concessionCommentRepository;
            _concessionRelationshipRepository = concessionRelationshipRepository;
            _auditRepository = auditRepository;
            _userManager = userManager;
            _concessionInboxViewRepository = concessionInboxViewRepository;
            _concessionDetailRepository = concessionDetailRepository;
            _concessionConditionViewRepository = concessionConditionViewRepository;
            _miscPerformanceRepository = miscPerformanceRepository;
            _centreRepository = centreRepository;
            _primeRateRepository = primeRateRepository;
            _concessionLetterRepository = concessionLetterRepository;
            _aeNumberUserManager = aeNumberUserManager;
            _roleSubRoleRepository = roleSubRoleRepository;
        }

        /// <summary>
        /// Gets the pending concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetPendingConcessionsForUser(User user)
        {
            var inboxConcessions = new List<InboxConcession>();
            var pendingStatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Pending);
            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.BcmPending);
            var pcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.PcmPending);

            //loop through the user roles and get the concessions for the particular user
            if (user != null && user.UserRoles != null && user.UserRoles.Count() > 0)

                foreach (var userRole in user.UserRoles)
                {

                    switch (userRole.Name.Trim())
                    {
                        case Constants.Roles.AA:
                            if (user.SubRoleId.HasValue)
                            {

                                var consType = GetSubRoleAndType(user.SubRoleId);

                                inboxConcessions.AddRange(_mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                    .ReadByRequestorIdStatusIdsIsActive(_userManager.GetUserIdForFiltering(user), new[] { pendingStatusId }, true)
                                    .Where(x => x.ConcessionType == consType)));
                            }
                            else
                            {

                                inboxConcessions.AddRange(
                                    _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                        .ReadByRequestorIdStatusIdsIsActive(_userManager.GetUserIdForFiltering(user), new[] { pendingStatusId },
                                            true)));
                            }

                            break;
                        case Constants.Roles.Requestor:
                            inboxConcessions.AddRange(
                                _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                    .ReadByRequestorIdStatusIdsIsActive(user.Id, new[] { pendingStatusId }, true)));
                            break;
                        case Constants.Roles.BCM:
                            var bcmCentreIds = (from centre in user.UserCentres
                                                select centre.Id).ToArray();

                            inboxConcessions.AddRange(
                                _mapper.Map<IEnumerable<InboxConcession>>(
                                    _concessionInboxViewRepository.ReadByCentreIdsStatusIdSubStatusIdIsActive(bcmCentreIds,
                                        pendingStatusId, bcmPendingStatusId, true)));

                            break;
                        case Constants.Roles.PCM:
                        case Constants.Roles.HeadOffice:

                            //we will only look for concessions with status BCM Pending..
                            var pcmSnIpendingStatusIds = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.PcmSnIPending);
                            if (user.SubRoleId.HasValue)
                            {
                                var consType = GetSubRoleAndType(user.SubRoleId);
                                
                                inboxConcessions.AddRange(_mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                    .ReadbyPCMPending(null, null, null, new[] { pcmSnIpendingStatusIds })
                                    .Where(x => x.ConcessionType == consType)));
                            }
                            else
                            {
                                inboxConcessions.AddRange(_mapper.Map<IEnumerable<InboxConcession>>(
                                   _concessionInboxViewRepository.ReadbyPCMPending(null, null, null, new[] { pcmPendingStatusId, pcmSnIpendingStatusIds})));
                            }                            

                            break;
                    }
                }

            return inboxConcessions;
        }

        /// <summary>
        /// Gets the distinct inbox concessions.
        /// </summary>
        /// <param name="inboxConcessions">The inbox concessions.</param>
        /// <returns></returns>
        private IEnumerable<InboxConcession> GetDistinctInboxConcessions(IEnumerable<InboxConcession> inboxConcessions)
        {
            return inboxConcessions.GroupBy(_ => _.ReferenceNumber).Select(_ => _.First()).ToList();
        }

        public ConcessionLetter CreateConcessionLetter(ConcessionLetter model)
        {

            return _concessionLetterRepository.Create(model);

        }

        /// <summary>
        /// Get the due for expiry concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetDueForExpiryConcessionsForUser(User user)
        {
            var approvedStatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Approved);
            var approvedWithChangesStatusId =
                _lookupTableManager.GetStatusId(Constants.ConcessionStatus.ApprovedWithChanges);

            var userId = _userManager.GetUserIdForFiltering(user);

            if (user.SubRoleId.HasValue)
            {
                var consType = GetSubRoleAndType(user.SubRoleId);

                return _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                         .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(userId, DateTime.Now,
                             DateTime.Now.AddMonths(3), new[] { approvedStatusId, approvedWithChangesStatusId }, true).Where(x => x.ConcessionType == consType));
            }
            else
            {
                return _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                          .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(userId, DateTime.Now,
                              DateTime.Now.AddMonths(3), new[] { approvedStatusId, approvedWithChangesStatusId }, true));
            }

        }

        /// <summary>
        /// Gets the expired concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetExpiredConcessionsForUser(User user)
        {
            var approvedStatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Approved);
            var approvedWithChangesStatusId =
                _lookupTableManager.GetStatusId(Constants.ConcessionStatus.ApprovedWithChanges);

            var userId = _userManager.GetUserIdForFiltering(user);

            if (user.SubRoleId.HasValue)
            {
                var consType = GetSubRoleAndType(user.SubRoleId);

                return _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                   .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(userId, DateTime.MinValue,
                       DateTime.Now, new[] { approvedStatusId, approvedWithChangesStatusId }, true).Where(x => x.ConcessionType == consType));
            }
            else
            {
                return _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                               .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(userId, DateTime.MinValue,
                                   DateTime.Now, new[] { approvedStatusId, approvedWithChangesStatusId }, true));
            }

        }

        /// <summary>
        /// Gets the mismatched concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetMismatchedConcessionsForUser(User user)
        {
            var approvedStatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Approved);
            var approvedWithChangesStatusId =
                _lookupTableManager.GetStatusId(Constants.ConcessionStatus.ApprovedWithChanges);

            var userId = _userManager.GetUserIdForFiltering(user);

            if (user.SubRoleId.HasValue)
            {
                var consType = GetSubRoleAndType(user.SubRoleId);
                return _mapper.Map<IEnumerable<InboxConcession>>(
                       _concessionInboxViewRepository.ReadByRequestorIdStatusIdsIsMismatchedIsActive(userId,
                           new[] { approvedStatusId, approvedWithChangesStatusId }, true, true).Where(x => x.ConcessionType == consType));

            }
            else
            {
                return _mapper.Map<IEnumerable<InboxConcession>>(
                        _concessionInboxViewRepository.ReadByRequestorIdStatusIdsIsMismatchedIsActive(userId,
                            new[] { approvedStatusId, approvedWithChangesStatusId }, true, true));
            }

        }

        /// <summary>
        /// Gets the declined concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetDeclinedConcessionsForUser(User user)
        {
            var declinedStatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Declined);

            var userId = _userManager.GetUserIdForFiltering(user);

            if (user.SubRoleId.HasValue)
            {
                var consType = GetSubRoleAndType(user.SubRoleId);
                return _mapper.Map<IEnumerable<InboxConcession>>(
                        _concessionInboxViewRepository.ReadByRequestorIdStatusIdsIsActive(userId, new[] { declinedStatusId },
                            true).Where(x => x.ConcessionType == consType));
            }
            else
            {
                return _mapper.Map<IEnumerable<InboxConcession>>(
                         _concessionInboxViewRepository.ReadByRequestorIdStatusIdsIsActive(userId, new[] { declinedStatusId },
                             true));
            }

        }

        private string GetSubRoleAndType(int? subRoleId)
        {
            string concessionType;
            switch (subRoleId)
            {
                case (int)Constants.RoleSubRole.BolUser:
                    concessionType = Constants.ConcessionType.BusinessOnlineDesc;
                    break;
                case (int)Constants.RoleSubRole.PCMSnIUser:
                    concessionType = Constants.ConcessionType.Investment;
                    break;
                default:
                    concessionType = Constants.ConcessionType.Trade;
                    break;
            }

            return concessionType;
        }

        /// <summary>
        /// Gets the actioned concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetActionedConcessionsForUser(User user)
        {
            var inboxConcessions = new List<InboxConcession>();

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case Constants.Roles.BCM:
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                .ReadByBcmUserIdIsActive(user.Id, true)));
                        break;
                    case Constants.Roles.PCM:
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                .ReadByPcmUserIdIsActive(user.Id, true)));
                        break;
                    case Constants.Roles.HeadOffice:
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                .ReadByHoUserIdIsActive(user.Id, true)));
                        break;
                }
            }

            return inboxConcessions;
        }

        /// <summary>
        /// Gets the user concessions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserConcessions GetUserConcessions(User user)
        {
            var userConcessions = new UserConcessions();

            if (user != null)
            {

                var pendingConcessions = GetDistinctInboxConcessions(GetPendingConcessionsForUser(user));

                userConcessions.PendingConcessions = pendingConcessions;
                userConcessions.PendingConcessionsCount = pendingConcessions?.Count() ?? 0;
                userConcessions.ShowPendingConcessions = true;

                if (user.CanRequest)
                {
                    var dueForExpiryConcessions = GetDistinctInboxConcessions(GetDueForExpiryConcessionsForUser(user));
                    userConcessions.DueForExpiryConcessions = dueForExpiryConcessions;
                    userConcessions.DueForExpiryConcessionsCount = dueForExpiryConcessions?.Count() ?? 0;
                    userConcessions.ShowDueForExpiryConcessions = true;

                    var expiredConcessions = GetDistinctInboxConcessions(GetExpiredConcessionsForUser(user));
                    userConcessions.ExpiredConcessions = expiredConcessions;
                    userConcessions.ExpiredConcessionsCount = expiredConcessions?.Count() ?? 0;
                    userConcessions.ShowExpiredConcessions = true;

                    var mismatchedConcessions = GetDistinctInboxConcessions(GetMismatchedConcessionsForUser(user));
                    userConcessions.MismatchedConcessions = mismatchedConcessions;
                    userConcessions.MismatchedConcessionsCount = mismatchedConcessions?.Count() ?? 0;
                    userConcessions.ShowMismatchedConcessions = true;

                    var declinedConcessions = GetDistinctInboxConcessions(GetDeclinedConcessionsForUser(user));
                    userConcessions.DeclinedConcessions = declinedConcessions;
                    userConcessions.DeclinedConcessionsCount = declinedConcessions?.Count() ?? 0;
                    userConcessions.ShowDeclinedConcessions = true;
                }

                if (user.CanBcmApprove || user.CanPcmApprove || user.IsHO)
                {
                    var actionedConcessions = GetDistinctInboxConcessions(GetActionedConcessionsForUser(user));
                    userConcessions.ActionedConcessions = actionedConcessions;
                    userConcessions.ActionedConcessionsCount = actionedConcessions?.Count() ?? 0;
                    userConcessions.ShowActionedConcessions = true;
                }

                if (user.IsHO || user.IsPCM)
                {
                    userConcessions.IsElevatedUser = true;

                }

            }

            return userConcessions;
        }

        /// <summary>
        /// Gets the concession conditions.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionCondition> GetConcessionConditions(int concessionId)
        {
            var concessionConditions = new List<ConcessionCondition>();
            var concessionConditionsData = _concessionConditionRepository.ReadByConcessionId(concessionId);

            foreach (var concessionCondition in concessionConditionsData.Where(_ => _.IsActive))
            {
                var mappedConcessionCondition = _mapper.Map<ConcessionCondition>(concessionCondition);

                mappedConcessionCondition.ConditionType =
                    _lookupTableManager.GetConditionTypeName(concessionCondition.ConditionTypeId);

                mappedConcessionCondition.ProductType =
                    _lookupTableManager.GetConditionProductName(concessionCondition.ConditionProductId);

                if (concessionCondition.PeriodTypeId.HasValue)
                    mappedConcessionCondition.PeriodType =
                        _lookupTableManager.GetPeriodTypeName(concessionCondition.PeriodTypeId.Value);

                if (concessionCondition.PeriodId.HasValue)
                    mappedConcessionCondition.Period =
                        _lookupTableManager.GetPeriodName(concessionCondition.PeriodId.Value);

                concessionConditions.Add(mappedConcessionCondition);
            }

            return concessionConditions;
        }

        /// <summary>
        /// Gets the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber, User user, string concessiontype, int? sapbpid = null)
        {
            int? userId = null;

            if (user.CanRequest && user.IsAdminAssistant)
                userId = user.AccountExecutiveUserId;
            else if (user.CanRequest && !user.IsAdminAssistant)
                userId = user.Id;

            if (user.IsPCM || user.IsHO || user.IsBCM)
            {
                //only work ons risk group not user..
                userId = null;

            }

            if (riskGroupNumber > 0)
                return _miscPerformanceRepository.GetClientAccounts(riskGroupNumber, userId, concessiontype);
            else if (sapbpid.HasValue && sapbpid.Value > 0)
                return _miscPerformanceRepository.GetClientAccounts(riskGroupNumber, userId, concessiontype, legalEntityCustomerNumber: sapbpid.Value);

            return null;
        }

        public IEnumerable<SearchConcessionDetail> SearchConsessions(int userId)
        {
            //we will only look for concessions with status BCM Pending..
            var bcmpendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.BcmPending);

            var concessions = _concessionInboxViewRepository.Search(null, null, null, new[] { bcmpendingStatusId });

            var approvedConcessionDetails = new List<SearchConcessionDetail>();
            foreach (var concession in concessions.OrderByDescending(_ => _.DateApproved ?? _.ConcessionDate))
            {
                approvedConcessionDetails.Add(new SearchConcessionDetail
                {

                    RiskGroupNumber = concession.RiskGroupNumber,
                    RiskGroupName = concession.RiskGroupName,
                    CustomerName = concession.CustomerName,
                    CustomerNumber = concession.CustomerNumber,
                    Status = concession.Status + " - " + concession.SubStatus,
                    ConcessionType = concession.ConcessionType,
                    ExpiryDate = concession.ExpiryDate,
                    DateApproved = concession.DateApproved,
                    DateOpened = concession.ConcessionDate,
                    DateSentForApproval = concession.DatesentForApproval,
                    ConcessionDetailId = concession.ConcessionDetailId,
                    ConcessionId = concession.ConcessionId,
                    ReferenceNumber = concession.ConcessionRef
                });

            }

            return approvedConcessionDetails;
        }

        public IEnumerable<SearchConcessionDetail> SearchConsessions(int region, int businesscentre, string status, DateTime datefilter, int userid)
        {
            //we will only look for concessions with status BCM Pending..

            var bcmpendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.BcmPending);

            var concessions = _concessionInboxViewRepository.Search(region, businesscentre, datefilter, new[] { bcmpendingStatusId });

            var approvedConcessionDetails = new List<SearchConcessionDetail>();
            foreach (var concession in concessions.OrderByDescending(_ => _.DateApproved ?? _.ConcessionDate))
            {
                approvedConcessionDetails.Add(new SearchConcessionDetail
                {

                    RiskGroupNumber = concession.RiskGroupNumber,
                    RiskGroupName = concession.RiskGroupName,
                    CustomerName = concession.CustomerName,
                    CustomerNumber = concession.CustomerNumber,
                    Status = concession.Status + " - " + concession.SubStatus,
                    ConcessionType = concession.ConcessionType,
                    ExpiryDate = concession.ExpiryDate,
                    DateApproved = concession.DateApproved,
                    DateOpened = concession.ConcessionDate,
                    DateSentForApproval = concession.DatesentForApproval,
                    ConcessionDetailId = concession.ConcessionDetailId,
                    ConcessionId = concession.ConcessionId,
                    ReferenceNumber = concession.ConcessionRef
                });

            }

            return approvedConcessionDetails;



        }

        /// <summary>
        /// Gets the applicable prime rate for the timeframe
        /// </summary>
        /// <param name="datefilter"></param>
        /// <returns></returns>
        public IEnumerable<string> PrimeRate(DateTime datefilter)
        {
            return _primeRateRepository.PrimeRate(datefilter);
        }

        /// <summary>
        /// Gets the approved concessions for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<ApprovedConcession> GetApprovedConcessionsForUser(int userId, User currentUser)
        {
            User loggedInUser = this._userManager.GetUser(userId);

            int requestorId = userId;
            if (loggedInUser != null && loggedInUser.IsAdminAssistant && loggedInUser.AccountExecutiveUserId.HasValue)
                requestorId = loggedInUser.AccountExecutiveUserId.Value;

            var approvedStatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Approved);
            var approvedWithChangesStatusId =
                _lookupTableManager.GetStatusId(Constants.ConcessionStatus.ApprovedWithChanges);

            var concessions =
                _concessionInboxViewRepository.GetapporvedView(requestorId, new[] { approvedStatusId, approvedWithChangesStatusId }, true);

            if (currentUser.SubRoleId != null)
            {
                if (currentUser.SubRoleId == (int)Constants.RoleSubRole.BolUser)
                    concessions = concessions.Where(a => a.ConcessionType == Constants.ConcessionType.BusinessOnlineDesc);

                if (currentUser.SubRoleId == (int)Constants.RoleSubRole.TradeUser)
                    concessions = concessions.Where(a => a.ConcessionType == Constants.ConcessionType.Trade);
            }

            var approvedConcessions = new List<ApprovedConcession>();

            foreach (var concession in concessions.OrderByDescending(_ => _.DateApproved ?? _.ConcessionDate))
            {
                var approvedConcession =
                    approvedConcessions.FirstOrDefault(_ => _.LegalEntityId == concession.LegalEntityId);

                if (approvedConcession == null)
                {
                    approvedConcession = new ApprovedConcession
                    {
                        RiskGroupNumber = concession.RiskGroupNumber,
                        RiskGroupName = concession.RiskGroupName,
                        LegalEntityId = concession.LegalEntityId,
                        CustomerName = concession.CustomerName,
                        CustomerNumber = concession.CustomerNumber,
                        Segment = concession.Segment,
                        ApprovedConcessionDetails = new List<ApprovedConcessionDetail>()
                    };

                    approvedConcessions.Add(approvedConcession);
                }

                var approvedConcessionDetails = new List<ApprovedConcessionDetail>();

                approvedConcessionDetails.AddRange(approvedConcession.ApprovedConcessionDetails);


                var newapproved = new ApprovedConcessionDetail
                {
                    Status = concession.Status,
                    ConcessionType = concession.ConcessionType,
                    ExpiryDate = concession.ExpiryDate,
                    DateApproved = concession.DateApproved,
                    DateOpened = concession.ConcessionDate,
                    DateSentForApproval = concession.DatesentForApproval,
                    ConcessionDetailId = concession.ConcessionDetailId,
                    ConcessionId = concession.ConcessionId,
                    ReferenceNumber = concession.ConcessionRef,
                    ConcessionLetterURL = concession.ConcessionLetterURL
                };

                //filter by role sub role
                if (currentUser.SubRoleId.HasValue)
                {
                    //get which concessions the user needs to see.
                    var consType = GetSubRoleAndType(currentUser.SubRoleId);

                    if (newapproved.ConcessionType.Equals(consType))
                    {
                        //remove doubles.
                        if (!approvedConcessionDetails.Contains(newapproved))
                        {
                            approvedConcessionDetails.Add(newapproved);
                        }

                        approvedConcession.ApprovedConcessionDetails = approvedConcessionDetails;
                    }
                }
                else
                {
                    //remove doubles.
                    if (!approvedConcessionDetails.Contains(newapproved))
                    {
                        approvedConcessionDetails.Add(newapproved);
                    }

                    approvedConcession.ApprovedConcessionDetails = approvedConcessionDetails;
                }

            }

            //remove concessions without concessions details..
            approvedConcessions.ForEach(x =>
            {
                if (x.ApprovedConcessionDetails.Count() == 0)
                {
                    approvedConcessions.Remove(x);
                }

            });

            return approvedConcessions;
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="periodType">Type of the period.</param>
        /// <param name="period">The period.</param>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        public IEnumerable<ConcessionCondition> GetConditions(string periodType, string period, int requestorId)
        {
            if (periodType == "Standard")
                periodType = "Once-off";


            var periodId = _lookupTableManager.GetPeriods().First(x => x.Description == period).Id;
            var periodTypeId = _lookupTableManager.GetPeriodTypes().First(x => x.Description == periodType).Id;

            var conditions =
                _concessionConditionViewRepository.ReadByPeriodIdPeriodTypeId(periodId, periodTypeId, requestorId);

            var results = _mapper.Map<IEnumerable<ConcessionCondition>>(conditions);

            results.ToList().ForEach(x => { x.RagStatus = GetRagStatus(x.Period, x.ApprovedDate.Value); });

            return results;
        }


        /// <summary>
        /// Gets the condition counts.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        public ConditionCounts GetConditionCounts(int requestorId)
        {
            var conditionCounts = _concessionConditionViewRepository.ReadConcessionCounts(requestorId);

            return new ConditionCounts
            {
                OngoingCount =
                    conditionCounts?.FirstOrDefault(_ => _.PeriodType == Constants.PeriodType.Ongoing)?.RecordCount ??
                    0,
                StandardCount =
                    conditionCounts?.FirstOrDefault(_ => _.PeriodType == Constants.PeriodType.Standard)?.RecordCount ??
                    0
            };
        }

        /// <summary>
        /// Updates the concession.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Concession UpdateConcession(Model.UserInterface.Concession concession, User user)
        {
            //manually set the values on the concession from the database
            //that the user interface version will not have
            var mappedConcession = GetMappedConcessionForUpdate(concession);

            _concessionRepository.Update(mappedConcession);

            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                //check if this is an extension or renewal for another concession, if it is then
                //we need to deactivate the parent concession since this one is approved
                DeactivateParentConcessions(mappedConcession.Id, user);
            }

            return mappedConcession;
        }

        /// <summary>
        /// Deletes the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <returns></returns>
        public Model.Repository.ConcessionCondition DeleteConcessionCondition(ConcessionCondition concessionCondition)
        {
            var concessionCondtion = _concessionConditionRepository.ReadById(concessionCondition.ConcessionConditionId);

            _concessionConditionRepository.Delete(concessionCondtion);

            return concessionCondtion;
        }

        /// <summary>
        /// Activates the concession.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Concession ActivateConcession(string concessionReferenceNumber, User user)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceNumber, false);
            var concession = concessions.OrderByDescending(_ => _.Id).First();

            concession.IsActive = true;
            concession.IsCurrent = true;

            _concessionRepository.Update(concession);

            return concession;
        }

        /// <summary>
        /// Creates the concession comment.
        /// </summary>
        /// <param name="concessionComment">The concession comment.</param>
        /// <returns></returns>
        public ConcessionComment CreateConcessionComment(ConcessionComment concessionComment)
        {
            return _concessionCommentRepository.Create(concessionComment);
        }

        /// <summary>
        /// Creates the concession.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Concession CreateConcession(Model.UserInterface.Concession concession, User user)
        {
            var mappedConcession = _mapper.Map<Model.Repository.Concession>(concession);
            mappedConcession.TypeId = _lookupTableManager.GetReferenceTypeId(concession.Type);

            mappedConcession.ConcessionTypeId =
                _lookupTableManager.GetConcessionTypeId(concession.ConcessionType);

            mappedConcession.StatusId = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Pending);
            mappedConcession.SubStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.BcmPending);
            mappedConcession.ConcessionDate = DateTime.Now;

            if (user.IsAdminAssistant)
                mappedConcession.AAUserId = user.Id;

            mappedConcession.RequestorId = _userManager.GetUserIdForFiltering(user);

            mappedConcession.CentreId = user.SelectedCentre.Id;
            mappedConcession.RegionId = GetRegionForCentre(mappedConcession.CentreId);
            mappedConcession.IsCurrent = true;
            mappedConcession.IsActive = true;
            mappedConcession.DatesentForApproval = DateTime.Now;

            AENumberUser aeNumberUser = this._aeNumberUserManager.GetAENumberUserByAccountExecutiveUserId(mappedConcession.RequestorId);
            if (aeNumberUser != null)
                mappedConcession.AENumberUserId = aeNumberUser.AENumberUserId;

            var result = _concessionRepository.Create(mappedConcession);

            //need to generate the concession reference based on the id returned
            var concessionReference =
                $"{concession.ConcessionType.Substring(0, 1)}{Convert.ToString(result.Id).PadLeft(12, '0')}";

            result.ConcessionRef = concessionReference;

            _concessionRepository.Update(result);

            return result;
        }

        /// <summary>
        /// Gets the region for centre.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        private int GetRegionForCentre(int centreId)
        {
            var centre = _centreRepository.ReadById(centreId);

            return centre.RegionId;
        }

        /// <summary>
        /// Creates the concession relationship.
        /// </summary>
        /// <param name="concessionRelationship">The concession relationship.</param>
        /// <returns></returns>
        public ConcessionRelationship CreateConcessionRelationship(
            Model.UserInterface.ConcessionRelationship concessionRelationship)
        {
            var mappedConcessionRelationship =
                _mapper.Map<Model.Repository.ConcessionRelationship>(concessionRelationship);

            mappedConcessionRelationship.RelationshipId =
                _lookupTableManager.GetRelationshipId(concessionRelationship.RelationshipDescription);

            return _concessionRelationshipRepository.Create(mappedConcessionRelationship);
        }

        /// <summary>
        /// Deactivates the concession.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Concession DeactivateConcession(string concessionReferenceNumber, bool isRecall, User user)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceNumber, true);

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            var concession = concessions.Single();

            concession.IsActive = false;
            concession.IsCurrent = false;

            if (!isRecall)
            {
                concession.Archived = DateTime.Now;
            }

            _concessionRepository.DeactivateConcession(concession);

            return concession;
        }

        public ConcessionDetail DeactivateConcessionDetailed(int ConcessionDetailId, User user)
        {
            var concession = _concessionDetailRepository.ReadById(ConcessionDetailId);

            concession.Archived = DateTime.Now;

            _concessionDetailRepository.Update(concession);

            return concession;


        }

        /// <summary>
        /// Creates the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public Model.Repository.ConcessionCondition CreateConcessionCondition(ConcessionCondition concessionCondition,
            Model.UserInterface.Concession concession)
        {
            var mappedConcessionCondition = _mapper.Map<Model.Repository.ConcessionCondition>(concessionCondition);

            mappedConcessionCondition.ConcessionId = concession.Id;
            mappedConcessionCondition.IsActive = true;

            return _concessionConditionRepository.Create(mappedConcessionCondition);
        }

        /// <summary>
        /// Updates the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public Model.Repository.ConcessionCondition UpdateConcessionCondition(ConcessionCondition concessionCondition,
            Model.UserInterface.Concession concession)
        {
            var mappedConcessionCondition = _mapper.Map<Model.Repository.ConcessionCondition>(concessionCondition);

            mappedConcessionCondition.ConcessionId = concession.Id;
            mappedConcessionCondition.IsActive = true;

            _concessionConditionRepository.Update(mappedConcessionCondition);

            return mappedConcessionCondition;
        }

        /// <summary>
        /// Gets the concessions for risk group.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        public IEnumerable<Model.UserInterface.Concession> GetConcessionsForRiskGroup(int riskGroupId,
            string concessionType, User currentUser)
        {
            var concessionTypeId = _lookupTableManager.GetConcessionTypeId(concessionType);

            return Map(_concessionRepository.ReadByRiskGroupIdConcessionTypeIdIsActive(riskGroupId, concessionTypeId,
                true), false, currentUser);
        }

        /// <summary>
        /// Gets the approved concessions for risk group.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        public IEnumerable<Model.UserInterface.Concession> GetApprovedConcessionsForRiskGroup(int riskGroupId,
            string concessionType, User currentUser)
        {
            var concessionTypeId = _lookupTableManager.GetConcessionTypeId(concessionType);

            return Map(_concessionRepository.ReadByRiskGroupIdConcessionTypeIdIsActiveApproved(riskGroupId,
                concessionTypeId,
                true), false, currentUser);
        }

        /// <summary>
        /// LegalEntityId is referenced as SAPBPID
        /// </summary>
        /// <param name="legalEntityId"></param>
        /// <param name="concessionType"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public IEnumerable<Model.UserInterface.Concession> GetApprovedConcessionsForLegalEntityId(int legalEntityId,
            string concessionType, User currentUser)
        {
            var concessionTypeId = _lookupTableManager.GetConcessionTypeId(concessionType);

            return Map(_concessionRepository.ReadByLegalEntityIdConcessionTypeIdIsActiveApproved(legalEntityId,
                concessionTypeId,
                true), false, currentUser);
        }

        /// <summary>
        /// Gets the concession for concession reference identifier.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Model.UserInterface.Concession GetConcessionForConcessionReferenceId(string concessionReferenceId, User currentUser)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceId, true);

            if (concessions == null || !concessions.Any())
                throw new Exception(
                    $"No active concession found for concession reference id {concessionReferenceId}. The concession could have been recalled.");

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            var concession = Map(concessions, true, currentUser).Single();

            return concession;
        }

        /// <summary>
        /// Gets the rag status.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <param name="dateApproved">The date approved.</param>
        /// <returns></returns>
        public string GetRagStatus(string period, DateTime dateApproved)
        {
            switch (period)
            {
                case Constants.Period.ThreeMonths:
                    return CalculateRagStatusThreeMonths(dateApproved);
                case Constants.Period.SixMonths:
                    return CalculateRagStatusSixMonths(dateApproved);
                case Constants.Period.NineMonths:
                    return CalculateRagStatusNineMonths(dateApproved);
                case Constants.Period.TwelveMonths:
                    return CalculateRagStatusTwelveMonths(dateApproved);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Maps the specified repository concessions.
        /// </summary>
        /// <param name="repositoryConcessions">The repository concessions.</param>
        /// <param name="mapAll">if set to <c>true</c> [map all].</param>
        /// <returns></returns>
        private IEnumerable<Model.UserInterface.Concession> Map(IEnumerable<Concession> repositoryConcessions, bool mapAll, User currentUser)
        {
            bool isAccountExecutiveOrAssistant = false;

            var concessions = new List<Model.UserInterface.Concession>();

            foreach (var concession in repositoryConcessions)
            {
                var mappedConcession = _mapper.Map<Model.UserInterface.Concession>(concession);

                if (concession.RiskGroupId.HasValue)
                {
                    var riskGroup = _riskGroupRepository.ReadById(concession.RiskGroupId.Value);

                    mappedConcession.RiskGroupNumber = riskGroup.RiskGroupNumber;
                    mappedConcession.RiskGroupName = riskGroup.RiskGroupName;
                }


                mappedConcession.Type = _lookupTableManager.GetReferenceTypeName(concession.TypeId);
                mappedConcession.ConcessionType = _lookupTableManager.GetConcessionType(concession.ConcessionTypeId)
                    ?.Code;

                mappedConcession.Status = _lookupTableManager.GetStatusDescription(concession.StatusId);
                mappedConcession.SubStatus = _lookupTableManager.GetSubStatusDescription(concession.SubStatusId);

                mappedConcession.CurrentAEUserId = this._aeNumberUserManager.GetCurrentAccountExecutiveUserId(concession.AENumberUserId);
                mappedConcession.CurrentAAList = this._aeNumberUserManager.GetAccountAssistantIds(concession.AENumberUserId);

                isAccountExecutiveOrAssistant = false;

                if (currentUser != null)
                    if (mappedConcession.CurrentAEUserId.HasValue && mappedConcession.CurrentAEUserId.Value == currentUser.Id)
                        isAccountExecutiveOrAssistant = true;
                    else if (mappedConcession.CurrentAAList != null && mappedConcession.CurrentAAList.Any(a => a.Value == currentUser.Id))
                        isAccountExecutiveOrAssistant = true;

                mappedConcession.AENumberUserId = concession.AENumberUserId;

                if (mapAll)
                {
                    SetIsInProgressExtensionOrRenewal(mappedConcession);

                    var isApproved = mappedConcession.Status == Constants.ConcessionStatus.Approved ||
                                     mappedConcession.Status == Constants.ConcessionStatus.ApprovedWithChanges;

                    if (!HasPendingChild(concession.Id) && isAccountExecutiveOrAssistant)
                    {
                        //this concession can be extended or renewed if there is an expiry date which is within the next three months and the concession
                        //is currently in the approved state
                        mappedConcession.CanRenew = CalculateIfCanRenew(concession, mappedConcession.Status);
                        mappedConcession.CanExtend = CalculateIfCanExtend(concession, mappedConcession.CanRenew);
                        mappedConcession.CanResubmit = CalculateIfCanResubmit(concession, mappedConcession.Status);
                        mappedConcession.CanUpdate = isApproved;// && isAccountExecutiveOrAssistant;
                        mappedConcession.CanArchive = isApproved;
                        mappedConcession.IsAENumberLinkedAccountExecutiveOrAssistant = isAccountExecutiveOrAssistant;
                    }
                    else
                    {
                        mappedConcession.CanExtend = false;
                        mappedConcession.CanRenew = false;
                        mappedConcession.CanResubmit = false;
                        mappedConcession.CanUpdate = false;
                        mappedConcession.CanArchive = false;
                        mappedConcession.IsAENumberLinkedAccountExecutiveOrAssistant = false;
                    }

                    mappedConcession.ConcessionComments = GetConcessionComments(concession.Id);

                    mappedConcession.ConcessionRelationshipDetails =
                        _mapper.Map<IEnumerable<Model.UserInterface.ConcessionRelationshipDetail>>(
                            _concessionRelationshipRepository.ReadDetailsByConcessionId(concession.Id));

                    mappedConcession.Requestor = _userManager.GetUser(concession.RequestorId);
                }

                concessions.Add(mappedConcession);
            }

            return concessions;
        }

        /// <summary>
        /// Sets the is in progress extension or renewal.
        /// </summary>
        /// <param name="concession">The concession.</param>
        private void SetIsInProgressExtensionOrRenewal(Model.UserInterface.Concession concession)
        {
            //if the status is not pending then it is not in progress
            if (concession.Status != Constants.ConcessionStatus.Pending)
            {
                concession.IsInProgressExtension = false;
                concession.IsInProgressRenewal = false;
            }
            else
            {
                var relationships = _concessionRelationshipRepository.ReadByChildConcessionId(concession.Id);
                var extenionRelationshipTypeId = _lookupTableManager.GetRelationshipId(Constants.RelationshipType.Extension);
                var renewalRelationshipTypeId = _lookupTableManager.GetRelationshipId(Constants.RelationshipType.Renewal);

                concession.IsInProgressExtension =
                    relationships.Any(_ => _.RelationshipId == extenionRelationshipTypeId);
                concession.IsInProgressRenewal = relationships.Any(_ => _.RelationshipId == renewalRelationshipTypeId);
            }
        }

        /// <summary>
        /// Calculates if can resubmit.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="currentStatus">The current status.</param>
        /// <returns></returns>
        private bool CalculateIfCanResubmit(Model.Repository.Concession concession, string currentStatus)
        {
            return currentStatus == Constants.ConcessionStatus.Declined;
        }

        /// <summary>
        /// Calculates if can renew.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="currentStatus">The current status.</param>
        /// <returns></returns>
        private bool CalculateIfCanRenew(Model.Repository.Concession concession, string currentStatus)
        {
            if (currentStatus == Constants.ConcessionStatus.Approved ||
                currentStatus == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                var concessionDetails = _concessionDetailRepository.ReadByConcessionId(concession.Id);

                foreach (var concessionDetail in concessionDetails)
                {
                    if (concessionDetail.ExpiryDate.HasValue &&
                        concessionDetail.ExpiryDate.Value <= DateTime.Now.AddMonths(3))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates if can extend.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="canRenew">if set to <c>true</c> [can renew].</param>
        /// <returns></returns>
        private bool CalculateIfCanExtend(Model.Repository.Concession concession, bool canRenew)
        {
            //if we can't renew this has already failed that check so there's no point in carrying on
            if (!canRenew)
                return false;

            //you can only extend a concession three times
            var extensionRelationshipId = _lookupTableManager.GetRelationshipId(Constants.RelationshipType.Extension);

            var relationships =
                _concessionRelationshipRepository.ReadByChildConcessionIdRelationshipIdRelationships(concession.Id,
                    extensionRelationshipId);

            if (relationships != null && relationships.Count() >= 3)
                return false;

            //if we get up to this point that means all the checks have passed and we can extend
            return true;
        }

        /// <summary>
        /// Determines whether [has pending child] [the specified concession identifier].
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns>
        ///   <c>true</c> if [has pending child] [the specified concession identifier]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasPendingChild(int concessionId)
        {
            var childConcessionRelationships = _concessionRelationshipRepository.ReadByParentConcessionId(concessionId);

            if (childConcessionRelationships != null && childConcessionRelationships.Any())
            {
                //if the child concession is still active and hasn't been declined then we cannot extend this
                foreach (var childConcessionRelationship in childConcessionRelationships)
                {
                    var childConcession = _concessionRepository.ReadById(childConcessionRelationship.ChildConcessionId);

                    if (childConcession.IsActive)
                    {
                        var declinedStatus = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Declined);
                        var removedStatus = _lookupTableManager.GetStatusId(Constants.ConcessionStatus.Removed);

                        if (childConcession.StatusId != declinedStatus && childConcession.StatusId != removedStatus)
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the concession comments.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <returns></returns>
        private IEnumerable<Model.UserInterface.ConcessionComment> GetConcessionComments(int concessionId)
        {
            var concessionComments = _concessionCommentRepository.ReadByConcessionId(concessionId);

            var mappedConcessionComments =
                _mapper.Map<IEnumerable<Model.UserInterface.ConcessionComment>>(concessionComments);

            foreach (var mappedConcessionComment in mappedConcessionComments)
            {
                mappedConcessionComment.UserDescription = _userManager.GetUserName(mappedConcessionComment.UserId);
                mappedConcessionComment.ConcessionSubStatusDescription =
                    _lookupTableManager.GetSubStatusDescription(mappedConcessionComment.ConcessionSubStatusId);
            }

            return mappedConcessionComments;
        }

        /// <summary>
        /// Calculates the rag status three months.
        /// </summary>
        /// <param name="dateApproved">The date approved.</param>
        /// <returns></returns>
        private string CalculateRagStatusThreeMonths(DateTime dateApproved)
        {
            var totalHours = GetWorkingDays(dateApproved, DateTime.Today) * 8;

            if (totalHours <= 168)
                return Constants.RagStatusResult.Green;

            if (totalHours > 168 && totalHours < 335)
                return Constants.RagStatusResult.Yellow;

            return Constants.RagStatusResult.Red;
        }

        /// <summary>
        /// Calculates the rag status six months.
        /// </summary>
        /// <param name="dateApproved">The date approved.</param>
        /// <returns></returns>
        private string CalculateRagStatusSixMonths(DateTime dateApproved)
        {
            var totalHours = GetWorkingDays(dateApproved, DateTime.Today) * 8;

            if (totalHours <= 336)
                return Constants.RagStatusResult.Green;

            if (totalHours > 336 && totalHours < 672)
                return Constants.RagStatusResult.Yellow;

            return Constants.RagStatusResult.Red;
        }

        /// <summary>
        /// Calculates the rag status nine months.
        /// </summary>
        /// <param name="dateApproved">The date approved.</param>
        /// <returns></returns>
        private string CalculateRagStatusNineMonths(DateTime dateApproved)
        {
            var totalHours = GetWorkingDays(dateApproved, DateTime.Today) * 8;

            if (totalHours <= 504)
                return Constants.RagStatusResult.Green;

            if (totalHours > 504 && totalHours <= 1007)
                return Constants.RagStatusResult.Yellow;

            return Constants.RagStatusResult.Red;
        }

        /// <summary>
        /// Calculates the rag status twelve months.
        /// </summary>
        /// <param name="dateApproved">The date approved.</param>
        /// <returns></returns>
        private string CalculateRagStatusTwelveMonths(DateTime dateApproved)
        {
            var totalHours = GetWorkingDays(dateApproved, DateTime.Today) * 8;

            if (totalHours <= 672)
                return Constants.RagStatusResult.Green;

            if (totalHours > 672 && totalHours <= 1343)
                return Constants.RagStatusResult.Yellow;

            return Constants.RagStatusResult.Red;
        }

        /// <summary>
        /// Calculates working between the two dates by excluding weekends
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int GetWorkingDays(DateTime from, DateTime to)
        {
            var totalDays = 0;

            for (var date = from.AddDays(1); date <= to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    totalDays++;
            }

            return totalDays;
        }

        /// <summary>
        /// Gets the mapped concession for update.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        private Concession GetMappedConcessionForUpdate(Model.UserInterface.Concession concession)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concession.ReferenceNumber, true);

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            var currentConcession = concessions.Single();

            var mappedConcession = _mapper.Map<Model.Repository.Concession>(concession);

            if (!string.IsNullOrWhiteSpace(concession.Type))
                mappedConcession.TypeId = _lookupTableManager.GetReferenceTypeId(concession.Type);
            else
                mappedConcession.TypeId = currentConcession.TypeId;

            mappedConcession.StatusId = string.IsNullOrWhiteSpace(concession.Status)
                ? currentConcession.StatusId
                : _lookupTableManager.GetStatusId(concession.Status);

            mappedConcession.SubStatusId = string.IsNullOrWhiteSpace(concession.SubStatus)
                ? currentConcession.SubStatusId
                : _lookupTableManager.GetSubStatusId(concession.SubStatus);

            mappedConcession.CentreId = currentConcession.CentreId;
            mappedConcession.ConcessionDate = currentConcession.ConcessionDate;
            mappedConcession.ConcessionRef = currentConcession.ConcessionRef;
            mappedConcession.ConcessionTypeId = currentConcession.ConcessionTypeId;
            mappedConcession.Id = currentConcession.Id;
            mappedConcession.IsActive = currentConcession.IsActive;
            mappedConcession.IsCurrent = currentConcession.IsCurrent;
            mappedConcession.AAUserId = currentConcession.AAUserId;
            mappedConcession.RequestorId = currentConcession.RequestorId;
            mappedConcession.DateActionedByBCM = currentConcession.DateActionedByBCM;
            mappedConcession.DateActionedByHO = currentConcession.DateActionedByHO;
            mappedConcession.DateActionedByPCM = currentConcession.DateActionedByPCM;
            mappedConcession.DatesentForApproval = currentConcession.DatesentForApproval;
            mappedConcession.HOUserId = currentConcession.HOUserId;
            mappedConcession.PCMUserId = currentConcession.PCMUserId;
            mappedConcession.RegionId = currentConcession.RegionId;

            if (concession.BcmUserId.HasValue)
            {
                mappedConcession.BCMUserId = concession.BcmUserId;
                mappedConcession.DateActionedByBCM = DateTime.Now;
            }
            else
                mappedConcession.BCMUserId = currentConcession.BCMUserId;

            if (concession.PcmUserId.HasValue)
            {
                mappedConcession.PCMUserId = concession.PcmUserId;
                mappedConcession.DateActionedByPCM = DateTime.Now;
            }
            else
                mappedConcession.PCMUserId = currentConcession.PCMUserId;

            if (concession.HoUserId.HasValue)
            {
                mappedConcession.HOUserId = concession.HoUserId;
                mappedConcession.DateActionedByHO = DateTime.Now;
            }
            else
                mappedConcession.HOUserId = currentConcession.HOUserId;

            return mappedConcession;
        }

        /// <summary>
        /// Deactivates the parent concessions.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="user">The user.</param>
        private void DeactivateParentConcessions(int concessionId, User user)
        {
            var concessionParentRelationships =
                _concessionRelationshipRepository.ReadByChildConcessionId(concessionId);

            foreach (var concessionParentRelationship in concessionParentRelationships)
            {
                var parentConcession =
                    _concessionRepository.ReadById(concessionParentRelationship.ParentConcessionId);

                if (parentConcession.IsActive || parentConcession.IsCurrent)
                {
                    parentConcession.IsActive = false;
                    parentConcession.IsCurrent = false;

                    _concessionRepository.Update(parentConcession);

                    _auditRepository.Audit(parentConcession, AuditType.Update, user.ANumber);
                }
            }
        }


    }
}