using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using Concession = StandardBank.ConcessionManagement.Model.Repository.Concession;
using ConcessionComment = StandardBank.ConcessionManagement.Model.Repository.ConcessionComment;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.Repository.ConcessionRelationship;
using Condition = StandardBank.ConcessionManagement.Model.UserInterface.Condition;
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
        /// The legal entity repository
        /// </summary>
        private readonly ILegalEntityRepository _legalEntityRepository;

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
        /// The legal entity account repository
        /// </summary>
        private readonly ILegalEntityAccountRepository _legalEntityAccountRepository;

        /// <summary>
        /// The concession comment repository
        /// </summary>
        private readonly IConcessionCommentRepository _concessionCommentRepository;

        /// <summary>
        /// The concession lending repository
        /// </summary>
        private readonly IConcessionLendingRepository _concessionLendingRepository;

        /// <summary>
        /// The market segment repository
        /// </summary>
        private readonly IMarketSegmentRepository _marketSegmentRepository;

        /// <summary>
        /// The concession cash repository
        /// </summary>
        private readonly IConcessionCashRepository _concessionCashRepository;

        /// <summary>
        /// The concession transactional repository
        /// </summary>
        private readonly IConcessionTransactionalRepository _concessionTransactionalRepository;

        /// <summary>
        /// The concession relationship repository
        /// </summary>
        private readonly IConcessionRelationshipRepository _concessionRelationshipRepository;

        /// <summary>
        /// The audit repository
        /// </summary>
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The rule manager
        /// </summary>
        private readonly IRuleManager _ruleManager;

        /// <summary>
        /// The concession inbox view repository
        /// </summary>
        private readonly IConcessionInboxViewRepository _concessionInboxViewRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionManager"/> class.
        /// </summary>
        /// <param name="concessionRepository">The concession repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="concessionConditionRepository">The concession condition repository.</param>
        /// <param name="legalEntityAccountRepository">The legal entity account repository.</param>
        /// <param name="concessionCommentRepository">The concession comment repository.</param>
        /// <param name="concessionLendingRepository">The concession lending repository.</param>
        /// <param name="marketSegmentRepository">The market segment repository.</param>
        /// <param name="concessionCashRepository">The concession cash repository.</param>
        /// <param name="concessionTransactionalRepository">The concession transactional repository.</param>
        /// <param name="concessionRelationshipRepository">The concession relationship repository.</param>
        /// <param name="auditRepository">The audit repository.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="ruleManager">The rule manager.</param>
        /// <param name="concessionInboxViewRepository">The concession inbox view repository.</param>
        public ConcessionManager(IConcessionRepository concessionRepository, ILookupTableManager lookupTableManager,
            ILegalEntityRepository legalEntityRepository, IRiskGroupRepository riskGroupRepository, IMapper mapper,
            IConcessionConditionRepository concessionConditionRepository,
            ILegalEntityAccountRepository legalEntityAccountRepository,
            IConcessionCommentRepository concessionCommentRepository,
            IConcessionLendingRepository concessionLendingRepository, IMarketSegmentRepository marketSegmentRepository,
            IConcessionCashRepository concessionCashRepository,
            IConcessionTransactionalRepository concessionTransactionalRepository,
            IConcessionRelationshipRepository concessionRelationshipRepository, IAuditRepository auditRepository,
            IUserManager userManager, IRuleManager ruleManager, IConcessionInboxViewRepository concessionInboxViewRepository)
        {
            _concessionRepository = concessionRepository;
            _lookupTableManager = lookupTableManager;
            _legalEntityRepository = legalEntityRepository;
            _riskGroupRepository = riskGroupRepository;
            _mapper = mapper;
            _concessionConditionRepository = concessionConditionRepository;
            _legalEntityAccountRepository = legalEntityAccountRepository;
            _concessionCommentRepository = concessionCommentRepository;
            _concessionLendingRepository = concessionLendingRepository;
            _marketSegmentRepository = marketSegmentRepository;
            _concessionCashRepository = concessionCashRepository;
            _concessionTransactionalRepository = concessionTransactionalRepository;
            _concessionRelationshipRepository = concessionRelationshipRepository;
            _auditRepository = auditRepository;
            _userManager = userManager;
            _ruleManager = ruleManager;
            _concessionInboxViewRepository = concessionInboxViewRepository;
        }

        /// <summary>
        /// Gets the pending concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetPendingConcessionsForUser(User user)
        {
            var inboxConcessions = new List<InboxConcession>();
            var pendingStatusId = _lookupTableManager.GetStatusId("Pending");
            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId("BCM Pending");
            var pcmPendingStatusId = _lookupTableManager.GetSubStatusId("PCM Pending");

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                .ReadByRequestorIdStatusIdsIsActive(user.Id, new[] {pendingStatusId}, true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(
                                _concessionInboxViewRepository.ReadByCentreIdStatusIdSubStatusIdIsActive(
                                    user.SelectedCentre.Id, pendingStatusId, bcmPendingStatusId, true)));
                        break;
                    case "PCM":
                    case "Head Office":
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(
                                _concessionInboxViewRepository.ReadByCentreIdStatusIdSubStatusIdIsActive(
                                    user.SelectedCentre.Id, pendingStatusId, pcmPendingStatusId, true)));
                        break;
                }
            }

            return inboxConcessions;
        }

        /// <summary>
        /// Get the due for expiry concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetDueForExpiryConcessionsForUser(User user)
        {
            return _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(user.Id, DateTime.Now,
                    DateTime.Now.AddMonths(3), true));
        }

        /// <summary>
        /// Gets the expired concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetExpiredConcessionsForUser(User user)
        {
            return _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(user.Id, DateTime.MinValue, DateTime.Now,
                    true));
        }

        /// <summary>
        /// Gets the mismatched concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetMismatchedConcessionsForUser(User user)
        {
            return _mapper.Map<IEnumerable<InboxConcession>>(
                _concessionInboxViewRepository.ReadByRequestorIdIsMismatchedIsActive(user.Id, true, true));
        }

        /// <summary>
        /// Gets the declined concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetDeclinedConcessionsForUser(User user)
        {
            var declinedStatusId = _lookupTableManager.GetStatusId("Declined");

            return _mapper.Map<IEnumerable<InboxConcession>>(
                _concessionInboxViewRepository.ReadByRequestorIdStatusIdsIsActive(user.Id, new[] {declinedStatusId}, true));
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
                    case "Suite Head":
                    case "BCM":
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                .ReadByBcmUserIdIsActive(user.Id, true)));
                        break;
                    case "PCM":
                        inboxConcessions.AddRange(
                            _mapper.Map<IEnumerable<InboxConcession>>(_concessionInboxViewRepository
                                .ReadByPcmUserIdIsActive(user.Id, true)));
                        break;
                    case "Head Office":
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

            var pendingConcessions = GetPendingConcessionsForUser(user);

            userConcessions.PendingConcessions = pendingConcessions;
            userConcessions.PendingConcessionsCount = pendingConcessions?.Count() ?? 0;
            userConcessions.ShowPendingConcessions = true;

            if (user.CanRequest)
            {
                var dueForExpiryConcessions = GetDueForExpiryConcessionsForUser(user);
                userConcessions.DueForExpiryConcessions = dueForExpiryConcessions;
                userConcessions.DueForExpiryConcessionsCount = dueForExpiryConcessions?.Count() ?? 0;
                userConcessions.ShowDueForExpiryConcessions = true;

                var expiredConcessions = GetExpiredConcessionsForUser(user);
                userConcessions.ExpiredConcessions = expiredConcessions;
                userConcessions.ExpiredConcessionsCount = expiredConcessions?.Count() ?? 0;
                userConcessions.ShowExpiredConcessions = true;

                var mismatchedConcessions = GetMismatchedConcessionsForUser(user);
                userConcessions.MismatchedConcessions = mismatchedConcessions;
                userConcessions.MismatchedConcessionsCount = mismatchedConcessions?.Count() ?? 0;
                userConcessions.ShowMismatchedConcessions = true;

                var declinedConcessions = GetDeclinedConcessionsForUser(user);
                userConcessions.DeclinedConcessions = declinedConcessions;
                userConcessions.DeclinedConcessionsCount = declinedConcessions?.Count() ?? 0;
                userConcessions.ShowDeclinedConcessions = true;
            }

            if (user.CanBcmApprove || user.CanPcmApprove || user.IsHO)
            {
                var actionedConcessions = GetActionedConcessionsForUser(user);
                userConcessions.ActionedConcessions = actionedConcessions;
                userConcessions.ActionedConcessionsCount = actionedConcessions?.Count() ?? 0;
                userConcessions.ShowActionedConcessions = true;
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
        /// <returns></returns>
        public IEnumerable<ClientAccount> GetClientAccounts(int riskGroupNumber)
        {
            var clientAccounts = new List<ClientAccount>();

            var riskGroup = _riskGroupRepository.ReadByRiskGroupNumberIsActive(riskGroupNumber, true);

            var legalEntities = _legalEntityRepository.ReadByRiskGroupIdIsActive(riskGroup.Id, true);

            foreach (var legalEntity in legalEntities)
            {
                var legalEntityAccounts =
                    _legalEntityAccountRepository.ReadByLegalEntityIdIsActive(legalEntity.Id, true);

                foreach (var legalEntityAccount in legalEntityAccounts)
                {
                    clientAccounts.Add(new ClientAccount
                    {
                        AccountNumber = legalEntityAccount.AccountNumber,
                        LegalEntityId = legalEntity.Id,
                        RiskGroupId = riskGroup.Id,
                        LegalEntityAccountId = legalEntityAccount.Id,
                        CustomerName = legalEntity.CustomerName
                    });
                }
            }

            return clientAccounts;
        }

        /// <summary>
        /// Gets the approved concessions for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<InboxConcession> GetApprovedConcessionsForUser(int userId)
        {
            var approvedStatusId = _lookupTableManager.GetStatusId("Approved");
            var approvedWithChangesStatusId = _lookupTableManager.GetStatusId("Approved With Changes");

            return _mapper.Map<IEnumerable<InboxConcession>>(
                _concessionInboxViewRepository.ReadByRequestorIdStatusIdsIsActive(userId,
                    new[] {approvedStatusId, approvedWithChangesStatusId}, true));
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <param name="periodType">Type of the period.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        public IEnumerable<Condition> GetConditions(string periodType, string period)
        {
            var approvedStatusId = _lookupTableManager.GetStatusId("Approved");
            var approvedWithChangesStatusId = _lookupTableManager.GetStatusId("Approved With Changes");

            var periodId = _lookupTableManager.GetPeriods().First(x => x.Description == period).Id;
            var periodTypeId = _lookupTableManager.GetPeriodTypes().First(x => x.Description == periodType).Id;

            var conditions = new List<Model.Repository.Condition>();

            conditions.AddRange(
                _concessionConditionRepository.ReadByPeriodAndApprovalStatus(approvedStatusId, periodId, periodTypeId));

            conditions.AddRange(
                _concessionConditionRepository.ReadByPeriodAndApprovalStatus(approvedWithChangesStatusId, periodId,
                    periodTypeId));

            var results = _mapper.Map<IEnumerable<Condition>>(conditions);

            results.ToList().ForEach(x => x.RagStatus = GetRagStatus(x.PeriodName, x.ApprovedDate));

            return results;
        }

        /// <summary>
        /// Gets the condition counts.
        /// </summary>
        /// <returns></returns>
        public ConditionCounts GetConditionCounts()
        {
            var conditionCounts = _concessionConditionRepository.ReadConditionCounts();

            return new ConditionCounts
            {
                OngoingCount = conditionCounts?.FirstOrDefault(_ => _.PeriodType == "Ongoing")?.RecordCount ?? 0,
                StandardCount = conditionCounts?.FirstOrDefault(_ => _.PeriodType == "Standard")?.RecordCount ?? 0
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

            if (concession.Status == "Approved" || concession.Status == "Approved With Changes")
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

            mappedConcession.StatusId = _lookupTableManager.GetStatusId("Pending");
            mappedConcession.SubStatusId = _lookupTableManager.GetSubStatusId("BCM Pending");
            mappedConcession.ConcessionDate = DateTime.Now;
            mappedConcession.RequestorId = user.Id;

            mappedConcession.CentreId = user.SelectedCentre.Id;
            mappedConcession.RegionId = user.SelectedRegion.Id;
            mappedConcession.IsCurrent = true;
            mappedConcession.IsActive = true;

            //TODO: Check if this should be set on create? Because on create the item is sent for approval?
            mappedConcession.DatesentForApproval = DateTime.Now;

            var result = _concessionRepository.Create(mappedConcession);

            //need to generate the concession reference based on the id returned
            var concessionReference =
                $"{concession.ConcessionType.Substring(0, 1)}{Convert.ToString(result.Id).PadLeft(12, '0')}";

            result.ConcessionRef = concessionReference;

            _concessionRepository.Update(result);

            return result;
        }

        /// <summary>
        /// Creates the concession relationship.
        /// </summary>
        /// <param name="concessionRelationship">The concession relationship.</param>
        /// <returns></returns>
        public ConcessionRelationship CreateConcessionRelationship(
            Model.UserInterface.ConcessionRelationship concessionRelationship)
        {
            var mappedConcessionRelationship = _mapper.Map<Model.Repository.ConcessionRelationship>(concessionRelationship);

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
        public Concession DeactivateConcession(string concessionReferenceNumber, User user)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceNumber, true);

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            var concession = concessions.Single();

            concession.IsActive = false;
            concession.IsCurrent = false;

            _concessionRepository.Update(concession);

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

        public IEnumerable<Model.UserInterface.Concession> GetConcessionsForRiskGroup(int riskGroupId,
            string concessionType)
        {
            //TODO: Fix this
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the concession for concession reference identifier.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Model.UserInterface.Concession GetConcessionForConcessionReferenceId(string concessionReferenceId)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceId, true);

            if (concessions == null || !concessions.Any())
                throw new Exception(
                    $"No active concession found for concession reference id {concessionReferenceId}. The concession could have been recalled.");

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            var concession = Map(concessions).Single();

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
                case "3 Months":
                    return CalculateRagStatusThreeMonths(dateApproved);
                case "6 Months":
                    return CalculateRagStatusSixMonths(dateApproved);
                case "9 Months":
                    return CalculateRagStatusNineMonths(dateApproved);
                case "12 Months":
                    return CalculateRagStatusTwelveMonths(dateApproved);
                default:
                    return "";
            }
        }

        /// <summary>
        /// Maps the specified repository concessions.
        /// </summary>
        /// <param name="repositoryConcessions">The repository concessions.</param>
        /// <returns></returns>
        private IEnumerable<Model.UserInterface.Concession> Map(IEnumerable<Concession> repositoryConcessions)
        {
            var concessions = new List<Model.UserInterface.Concession>();

            foreach (var concession in repositoryConcessions)
            {
                var mappedConcession = _mapper.Map<Model.UserInterface.Concession>(concession);
                var riskGroup = _riskGroupRepository.ReadById(concession.RiskGroupId);

                mappedConcession.RiskGroupNumber = riskGroup.RiskGroupNumber;
                mappedConcession.RiskGroupName = riskGroup.RiskGroupName;

                mappedConcession.Type = _lookupTableManager.GetReferenceTypeName(concession.TypeId);
                mappedConcession.ConcessionType = _lookupTableManager.GetConcessionType(concession.ConcessionTypeId)
                    ?.Code;

                mappedConcession.Status = _lookupTableManager.GetStatusDescription(concession.StatusId);
                mappedConcession.SubStatus = _lookupTableManager.GetSubStatusDescription(concession.SubStatusId);

                if (!HasPendingChild(concession.Id))
                {
                    //this concession can be extended or renewed if there is an expiry date which is within the next three months and the concession
                    //is currently in the approved state
                    mappedConcession.CanExtend = CalculateIfCanExtend(concession, mappedConcession.Status);
                    mappedConcession.CanRenew = CalculateIfCanRenew(concession, mappedConcession.Status);
                    mappedConcession.CanResubmit = CalculateIfCanResubmit(concession, mappedConcession.Status);
                    mappedConcession.CanUpdate = CalculateIfCanUpdate(concession, mappedConcession.Status);
                }
                else
                {
                    mappedConcession.CanExtend = false;
                    mappedConcession.CanRenew = false;
                    mappedConcession.CanResubmit = false;
                    mappedConcession.CanUpdate = false;
                }

                mappedConcession.ConcessionComments = GetConcessionComments(concession.Id);

                mappedConcession.ConcessionRelationshipDetails =
                    _mapper.Map<IEnumerable<Model.UserInterface.ConcessionRelationshipDetail>>(
                        _concessionRelationshipRepository.ReadDetailsByConcessionId(concession.Id));

                var user = _userManager.GetUser(concession.RequestorId);

                mappedConcession.Requestor = new RequestorModel
                {
                    FullName = user.FullName,
                    ANumber = user.ANumber,
                    BusinessCentre = user.SelectedCentre.Name,
                    Region = user.SelectedRegion.Description
                };

                concessions.Add(mappedConcession);
            }

            return concessions;
        }

        /// <summary>
        /// Calculates if can update.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="currentStatus">The current status.</param>
        /// <returns></returns>
        private bool CalculateIfCanUpdate(Model.Repository.Concession concession, string currentStatus)
        {
            return currentStatus == "Approved" || currentStatus == "Approved With Changes";
        }

        /// <summary>
        /// Calculates if can resubmit.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="currentStatus">The current status.</param>
        /// <returns></returns>
        private bool CalculateIfCanResubmit(Model.Repository.Concession concession, string currentStatus)
        {
            return currentStatus == "Declined";
        }

        /// <summary>
        /// Calculates if can renew.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="currentStatus">The current status.</param>
        /// <returns></returns>
        private bool CalculateIfCanRenew(Model.Repository.Concession concession, string currentStatus)
        {
            //TODO: Fix this
            return (currentStatus == "Approved" || currentStatus == "Approved With Changes");
            //return concession.ExpiryDate.HasValue &&
            //       concession.ExpiryDate.Value <= DateTime.Now.AddMonths(3) &&
            //       (currentStatus == "Approved" || currentStatus == "Approved With Changes");
        }

        /// <summary>
        /// Calculates if can extend.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="currentStatus">The current status.</param>
        /// <returns></returns>
        private bool CalculateIfCanExtend(Model.Repository.Concession concession, string currentStatus)
        {
            //you can only extend a concession three times
            var extensionRelationshipId = _lookupTableManager.GetRelationshipId("Extension");

            var relationships =
                _concessionRelationshipRepository.ReadByChildConcessionIdRelationshipIdRelationships(concession.Id,
                    extensionRelationshipId);

            if (relationships != null && relationships.Count() >= 3)
                return false;
            
            //TODO: Fix this
            return (currentStatus == "Approved" || currentStatus == "Approved With Changes");
            //return concession.ExpiryDate.HasValue &&
            //       concession.ExpiryDate.Value <= DateTime.Now.AddMonths(3) &&
            //       (currentStatus == "Approved" || currentStatus == "Approved With Changes");
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
                        var declinedStatus = _lookupTableManager.GetStatusId("Declined");
                        var removedStatus = _lookupTableManager.GetStatusId("Removed");

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

            var mappedConcessionComments = _mapper.Map<IEnumerable<Model.UserInterface.ConcessionComment>>(concessionComments);

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
                return "green";

            if (totalHours > 168 && totalHours < 335)
                return "yellow";

            return "red";
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
                return "green";

            if (totalHours > 336 && totalHours < 672)
                return "yellow";

            return "red";
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
                return "green";
            
            if (totalHours > 504 && totalHours <= 1007)
                return "yellow";

            return "red";
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
                return "green";
            
            if (totalHours > 672 && totalHours <= 1343)
                return "yellow";
            
            return "red";
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
            mappedConcession.RequestorId = currentConcession.RequestorId;
            mappedConcession.DateActionedByBCM = currentConcession.DateActionedByBCM;
            mappedConcession.DateActionedByHO = currentConcession.DateActionedByHO;
            mappedConcession.DateActionedByPCM = currentConcession.DateActionedByPCM;
            mappedConcession.DateApproved = currentConcession.DateApproved;
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

            if (concession.Status == "Approved" || concession.Status == "Approved With Changes")
            {
                if (!mappedConcession.DateApproved.HasValue)
                    mappedConcession.DateApproved = DateTime.Now;

                //TODO: Fix this
                //if (!mappedConcession.ExpiryDate.HasValue)
                //    mappedConcession.ExpiryDate = _ruleManager.CalculateExpiryDate(currentConcession.Id, concession.ConcessionType);
            }

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
