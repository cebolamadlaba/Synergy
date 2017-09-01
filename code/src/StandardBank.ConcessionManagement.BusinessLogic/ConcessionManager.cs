using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;
using Condition = StandardBank.ConcessionManagement.Model.UserInterface.Condition;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionRelationship;

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
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// The concession account repository
        /// </summary>
        private readonly IConcessionAccountRepository _concessionAccountRepository;

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

        private readonly IConcessionCommentRepository _concessionCommentRepository;
        private readonly IConcessionLendingRepository _concessionLendingRepository;
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
        /// Initializes a new instance of the <see cref="ConcessionManager"/> class.
        /// </summary>
        /// <param name="concessionRepository">The concession repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="legalEntityRepository">The legal entity repository.</param>
        /// <param name="riskGroupRepository">The risk group repository.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="concessionAccountRepository">The concession account repository.</param>
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
        public ConcessionManager(IConcessionRepository concessionRepository, ILookupTableManager lookupTableManager,
            ILegalEntityRepository legalEntityRepository, IRiskGroupRepository riskGroupRepository,
            ICacheManager cacheManager, IConcessionAccountRepository concessionAccountRepository, IMapper mapper,
            IConcessionConditionRepository concessionConditionRepository,
            ILegalEntityAccountRepository legalEntityAccountRepository,
            IConcessionCommentRepository concessionCommentRepository,
            IConcessionLendingRepository concessionLendingRepository, IMarketSegmentRepository marketSegmentRepository,
            IConcessionCashRepository concessionCashRepository,
            IConcessionTransactionalRepository concessionTransactionalRepository,
            IConcessionRelationshipRepository concessionRelationshipRepository, IAuditRepository auditRepository)
        {
            _concessionRepository = concessionRepository;
            _lookupTableManager = lookupTableManager;
            _legalEntityRepository = legalEntityRepository;
            _riskGroupRepository = riskGroupRepository;
            _cacheManager = cacheManager;
            _concessionAccountRepository = concessionAccountRepository;
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
        }

        /// <summary>
        /// Gets the pending concessions for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IEnumerable<Concession> GetPendingConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();
            var pendingStatusId = _lookupTableManager.GetStatusId("Pending");
            var bcmpendingStatusId = _lookupTableManager.GetSubStatusId("BCM Pending");
            var pcmpendingStatusId = _lookupTableManager.GetSubStatusId("PCM Pending");

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        concessions.AddRange(Map(
                            _concessionRepository.ReadByRequestorIdStatusIdIsActive(user.Id, pendingStatusId, true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        concessions.AddRange(Map(
                            _concessionRepository.ReadByCentreIdStatusIdSubStatusIdIsActive(user.SelectedCentre.Id,
                                pendingStatusId, bcmpendingStatusId, true)));
                        break;
                    case "PCM":
                    case "Head Office":
                        concessions.AddRange(Map(
                            _concessionRepository.ReadByCentreIdStatusIdSubStatusIdIsActive(user.SelectedCentre.Id,
                                pendingStatusId, pcmpendingStatusId, true)));
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Gets the due for expiry concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<Concession> GetDueForExpiryConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        concessions.AddRange(Map(_concessionRepository
                            .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(user.Id, DateTime.Now,
                                DateTime.Now.AddMonths(3), true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        break;
                    case "PCM":
                    case "Head Office":
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Gets the expired concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<Concession> GetExpiredConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        concessions.AddRange(Map(_concessionRepository
                            .ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(user.Id, DateTime.MinValue,
                                DateTime.Now, true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        break;
                    case "PCM":
                    case "Head Office":
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Gets the mismatched concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<Concession> GetMismatchedConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();
            var approvedWithChangesStatusId = _lookupTableManager.GetStatusId("Approved With Changes");

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        concessions.AddRange(Map(
                            _concessionRepository.ReadByRequestorIdStatusIdIsActive(user.Id,
                                approvedWithChangesStatusId, true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        break;
                    case "PCM":
                    case "Head Office":
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Gets the declined concessions for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<Concession> GetDeclinedConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();
            var declinedStatusId = _lookupTableManager.GetStatusId("Declined");

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                    case "Requestor":
                        concessions.AddRange(Map(
                            _concessionRepository.ReadByRequestorIdStatusIdIsActive(user.Id, declinedStatusId, true)));
                        break;
                    case "Suite Head":
                    case "BCM":
                        break;
                    case "PCM":
                    case "Head Office":
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Gets the users concessions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserConcessions GetUserConcessions(User user)
        {
            var userConcessions = new UserConcessions();

            var pendingConcessions = GetPendingConcessionsForUser(user);
            var dueForExpiryConcessions = GetDueForExpiryConcessionsForUser(user);
            var expiredConcessions = GetExpiredConcessionsForUser(user);
            var mismatchedConcessions = GetMismatchedConcessionsForUser(user);
            var declinedConcessions = GetDeclinedConcessionsForUser(user);

            var isRequestor = user.UserRoles.Any(_ => _.Name == "Requestor");

            userConcessions.PendingConcessions = pendingConcessions;
            userConcessions.PendingConcessionsCount = pendingConcessions.Count();
            userConcessions.ShowPendingConcessions = true;

            userConcessions.DueForExpiryConcessions = dueForExpiryConcessions;
            userConcessions.DueForExpiryConcessionsCount = dueForExpiryConcessions.Count();
            userConcessions.ShowDueForExpiryConcessions = isRequestor;

            userConcessions.ExpiredConcessions = expiredConcessions;
            userConcessions.ExpiredConcessionsCount = expiredConcessions.Count();
            userConcessions.ShowExpiredConcessions = isRequestor;

            userConcessions.MismatchedConcessions = mismatchedConcessions;
            userConcessions.MismatchedConcessionsCount = mismatchedConcessions.Count();
            userConcessions.ShowMismatchedConcessions = isRequestor;

            userConcessions.DeclinedConcessions = declinedConcessions;
            userConcessions.DeclinedConcessionsCount = declinedConcessions.Count();
            userConcessions.ShowDeclinedConcessions = isRequestor;

            return userConcessions;
        }

        /// <summary>
        /// Gets the concession conditions
        /// </summary>
        /// <param name="concessionId"></param>
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
                    _lookupTableManager.GetProductTypeName(concessionCondition.ConditionProductId);

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
        /// Creates a concession and returns the repository object
        /// </summary>
        /// <param name="concession"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Model.Repository.Concession CreateConcession(Concession concession, User user)
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
        /// Gets the client accounts for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
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
        /// Maps the specified repository concessions.
        /// </summary>
        /// <param name="repositoryConcessions">The repository concessions.</param>
        /// <returns></returns>
        private IEnumerable<Concession> Map(IEnumerable<Model.Repository.Concession> repositoryConcessions)
        {
            var concessions = new List<Concession>();

            foreach (var concession in repositoryConcessions)
            {
                var concessionAccount = _concessionAccountRepository.ReadByConcessionIdIsActive(concession.Id, true);

                var mappedConcession = _mapper.Map<Concession>(concession);
                var riskGroup = _riskGroupRepository.ReadById(concession.RiskGroupId);

                mappedConcession.RiskGroupNumber = riskGroup.RiskGroupNumber;
                mappedConcession.RiskGroupName = riskGroup.RiskGroupName;

                mappedConcession.Type = _lookupTableManager.GetReferenceTypeName(concession.TypeId);
                mappedConcession.ConcessionType = _lookupTableManager.GetConcessionType(concession.ConcessionTypeId)
                    ?.Code;
                mappedConcession.AccountNumber = concessionAccount?.AccountNumber;

                mappedConcession.Status = _lookupTableManager.GetStatusDescription(concession.StatusId);

                //this concession can be extended if there is an expiry date which is within the next three months and the concession
                //is currently in the approved state
                mappedConcession.CanExtend = CalculateIfCanExtend(concession, mappedConcession.Status);

                if (concession.SubStatusId.HasValue)
                    mappedConcession.SubStatus =
                        _lookupTableManager.GetSubStatusDescription(concession.SubStatusId.Value);

                concessions.Add(mappedConcession);
            }

            return concessions;
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

            var doesChildHaveThreeParentRelationships =
                _concessionRelationshipRepository.DoesChildHaveThreeParentRelationships(concession.Id,
                    extensionRelationshipId);

            if (doesChildHaveThreeParentRelationships)
                return false;

            //you cannot extend a concession that's already got an extension or renewal pending
            var childConcessionRelationships = _concessionRelationshipRepository.ReadByParentConcessionId(concession.Id);

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
                            return false;
                    }
                }
            }

            return concession.ExpiryDate.HasValue &&
                   concession.ExpiryDate.Value <= DateTime.Now.AddMonths(3) &&
                   currentStatus == "Approved";
        }

        public IEnumerable<Concession> GetActionedConcessionsForUser(User user)
        {
            var concessions = new List<Concession>();
          

            //loop through the user roles and get the concessions for the particular user
            foreach (var userRole in user.UserRoles)
            {
                switch (userRole.Name.Trim())
                {
                   
                    case "Suite Head":
                    case "BCM":
                        concessions.AddRange(Map(_concessionRepository.GetActionedByBCMUser(user.Id)));
                        break;
                    case "PCM":
                        concessions.AddRange(Map(_concessionRepository.GetActionedByPCMUser(user.Id)));
                        break;
                    case "Head Office":
                        concessions.AddRange(Map(_concessionRepository.GetActionedByHOUser(user.Id)));
                        break;
                }
            }

            return concessions;
        }

        /// <summary>
        /// Gets the concessions for the risk group for the concession type
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <param name="concessionType"></param>
        /// <returns></returns>
        public IEnumerable<Concession> GetConcessionsForRiskGroup(int riskGroupId, string concessionType)
        {
            var concessionTypeId = _lookupTableManager.GetConcessionTypeId(concessionType);
            return Map(_concessionRepository.ReadByRiskGroupIdConcessionTypeIdIsActive(riskGroupId, concessionTypeId, true));
        }

        /// <summary>
        /// Creates a concession condition
        /// </summary>
        /// <param name="concessionCondition"></param>
        /// <param name="concession"></param>
        /// <returns></returns>
        public Model.Repository.ConcessionCondition CreateConcessionCondition(ConcessionCondition concessionCondition, Concession concession)
        {
            var mappedConcessionCondition = _mapper.Map<Model.Repository.ConcessionCondition>(concessionCondition);

            mappedConcessionCondition.ConcessionId = concession.Id;
            mappedConcessionCondition.IsActive = true;

            return _concessionConditionRepository.Create(mappedConcessionCondition);
        }

        /// <summary>
        /// Gets the concessions for the concession reference id
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <returns></returns>
        public Concession GetConcessionForConcessionReferenceId(string concessionReferenceId)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceId, true);

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            return Map(concessions).Single();
        }

        /// <summary>
        /// Deactivates the concession
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Model.Repository.Concession DeactivateConcession(string concessionReferenceId, User user)
        {
            var concessions = _concessionRepository.ReadByConcessionRefIsActive(concessionReferenceId, true);

            //if there is more than one record returned then there is something wrong,
            //there shouldn't be two active concessions with the same concession reference number
            var concession = concessions.Single();

            concession.IsActive = false;
            concession.IsCurrent = false;

            _concessionRepository.Update(concession);

            return concession;
        }

        /// <summary>
        /// Updates the concession
        /// </summary>
        /// <param name="concession"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Model.Repository.Concession UpdateConcession(Concession concession, User user)
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
            mappedConcession.ExpiryDate = currentConcession.ExpiryDate;
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

            if (concession.Status == "Approved")
            {
                if (!mappedConcession.DateApproved.HasValue)
                    mappedConcession.DateApproved = DateTime.Now;

                if (!mappedConcession.ExpiryDate.HasValue)
                    mappedConcession.ExpiryDate = CalculateExpiryDate(currentConcession);
            }

            _concessionRepository.Update(mappedConcession);

            if (concession.Status == "Approved")
            {
                //check if this is an extension or renewal for another concession, if it is then
                //we need to deactivate the parent concession since this one is approved
                DeactivateParentConcessions(currentConcession.Id, user);
            }

            return mappedConcession;
        }

        /// <summary>
        /// Calculates the expiry date.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        private DateTime CalculateExpiryDate(Model.Repository.Concession concession)
        {
            //TODO: if this is an extension then the expiry date is three months from the current concenssions expiry date

            //TODO: otherwise calculate this based on the product type


            return DateTime.Now.AddMonths(12);
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

            var extensionRelationshipId = _lookupTableManager.GetRelationshipId("Extension");

            foreach (var concessionParentRelationship in concessionParentRelationships.Where(
                _ => _.RelationshipId == extensionRelationshipId))
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

        public IEnumerable<Condition> GetConditions(string periodType, string period)
        {
            var statusid = _lookupTableManager.GetStatusId("Approved");
            var periodId = _lookupTableManager.GetPeriods().First(x => x.Description == period).Id;
            var periodTypeId = _lookupTableManager.GetPeriodTypes().First(x => x.Description == periodType).Id;
            return _mapper.Map<IEnumerable<Condition>>(_concessionConditionRepository.ReadByPeriodAndApprovalStatus(statusid,periodId,periodTypeId));
        }

        /// <summary>
        /// Creates the concession comment
        /// </summary>
        /// <param name="concessionComment"></param>
        /// <returns></returns>
        public ConcessionComment CreateConcessionComment(ConcessionComment concessionComment)
        {
            var result = _concessionCommentRepository.Create(concessionComment);

            return result;
        }

        /// <summary>
        /// Gets the approved concessions for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<ApprovedConcession> GetApprovedConcessionsForUser(int userId)
        {
            var approvedConcessions = new List<ApprovedConcession>();

            var concessions = Map(_concessionRepository.ReadApprovedConcessions(userId));

            foreach (var concession in concessions)
            {
                approvedConcessions.Add(new ApprovedConcession
                {
                    RiskGroupNumber = concession.RiskGroupNumber.GetValueOrDefault(0),
                    RiskGroupName = concession.RiskGroupName,
                    ConcessionId = concession.Id,
                    ConcessionReferenceNumber = concession.ReferenceNumber,
                    ConcessionType = concession.ConcessionType,
                    ApprovedConcessionDetails = GetApprovedConcessionDetails(concession)
                });
            }

            return approvedConcessions;
        }

        /// <summary>
        /// Gets the approved concession details
        /// </summary>
        /// <param name="concession"></param>
        /// <returns></returns>
        public IEnumerable<ApprovedConcessionDetail> GetApprovedConcessionDetails(Concession concession)
        {
            switch (concession.ConcessionType)
            {
                case "Lending":
                    return GetApprovedLendingConcessionDetails(concession);
                case "Cash":
                    return GetApprovedCashConcessionDetails(concession);
                case "Transactional":
                    return GetApprovedTransactionalConcessionDetails(concession);
                default:
                    throw new NotImplementedException(concession.ConcessionType);
            }
        }

        /// <summary>
        /// Gets the approved transactional concession details.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        private IEnumerable<ApprovedConcessionDetail> GetApprovedTransactionalConcessionDetails(Concession concession)
        {
            var approvedConcessionDetails = new List<ApprovedConcessionDetail>();
            var concessionTransactionals = _concessionTransactionalRepository.ReadByConcessionId(concession.Id);

            foreach (var concessionCash in concessionTransactionals)
            {
                var legalEntity = _legalEntityRepository.ReadById(concessionCash.LegalEntityId);
                var marketSegment = _marketSegmentRepository.ReadById(legalEntity.MarketSegmentId);

                approvedConcessionDetails.Add(new ApprovedConcessionDetail
                {
                    CustomerName = legalEntity.CustomerName,
                    ConcessionType = "Transactional",
                    Status = concession.Status,
                    DateOpened = concession.DateOpened,
                    DateSentForApproval = concession.DateSentForApproval.GetValueOrDefault(DateTime.Now),
                    Segment = marketSegment.Description
                });
            }

            return approvedConcessionDetails;
        }

        /// <summary>
        /// Updates the concession condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public Model.Repository.ConcessionCondition UpdateConcessionCondition(ConcessionCondition concessionCondition, Concession concession)
        {
            var mappedConcessionCondition = _mapper.Map<Model.Repository.ConcessionCondition>(concessionCondition);

            mappedConcessionCondition.ConcessionId = concession.Id;
            mappedConcessionCondition.IsActive = true;

            _concessionConditionRepository.Update(mappedConcessionCondition);

            return mappedConcessionCondition;
        }

        /// <summary>
        /// Creates the concession relationship.
        /// </summary>
        /// <param name="concessionRelationship">The concession relationship.</param>
        /// <returns></returns>
        public Model.Repository.ConcessionRelationship CreateConcessionRelationship(ConcessionRelationship concessionRelationship)
        {
            var mappedConcessionRelationship = _mapper.Map<Model.Repository.ConcessionRelationship>(concessionRelationship);

            mappedConcessionRelationship.RelationshipId =
                _lookupTableManager.GetRelationshipId(concessionRelationship.RelationshipDescription);

            return _concessionRelationshipRepository.Create(mappedConcessionRelationship);
        }

        /// <summary>
        /// Gets the approved cash concession details.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        private IEnumerable<ApprovedConcessionDetail> GetApprovedCashConcessionDetails(Concession concession)
        {
            var approvedConcessionDetails = new List<ApprovedConcessionDetail>();
            var concessionCashEntities = _concessionCashRepository.ReadByConcessionId(concession.Id);

            foreach (var concessionCash in concessionCashEntities)
            {
                var legalEntity = _legalEntityRepository.ReadById(concessionCash.LegalEntityId);
                var marketSegment = _marketSegmentRepository.ReadById(legalEntity.MarketSegmentId);

                approvedConcessionDetails.Add(new ApprovedConcessionDetail
                {
                    CustomerName = legalEntity.CustomerName,
                    ConcessionType = "Cash",
                    Status = concession.Status,
                    DateOpened = concession.DateOpened,
                    DateSentForApproval = concession.DateSentForApproval.GetValueOrDefault(DateTime.Now),
                    Segment = marketSegment.Description
                });
            }

            return approvedConcessionDetails;
        }

        /// <summary>
        /// Gets the approved lending concession details
        /// </summary>
        /// <param name="concession"></param>
        /// <returns></returns>
        private IEnumerable<ApprovedConcessionDetail> GetApprovedLendingConcessionDetails(Concession concession)
        {
            var approvedConcessionDetails = new List<ApprovedConcessionDetail>();
            var concessionLendings = _concessionLendingRepository.ReadByConcessionId(concession.Id);

            foreach (var concessionLending in concessionLendings)
            {
                var legalEntity = _legalEntityRepository.ReadById(concessionLending.LegalEntityId);
                var marketSegment = _marketSegmentRepository.ReadById(legalEntity.MarketSegmentId);

                approvedConcessionDetails.Add(new ApprovedConcessionDetail
                {
                    CustomerName = legalEntity.CustomerName,
                    ConcessionType = "Lending",
                    Status = concession.Status,
                    DateOpened = concession.DateOpened,
                    DateSentForApproval = concession.DateSentForApproval.GetValueOrDefault(DateTime.Now),
                    Segment = marketSegment.Description
                });
            }

            return approvedConcessionDetails;
        }
    }
}
