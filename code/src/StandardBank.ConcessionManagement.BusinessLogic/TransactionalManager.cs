using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.TransactionalConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Transactional manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ITransactionalManager" />
    public class TransactionalManager : ITransactionalManager
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The concession transactional repository
        /// </summary>
        private readonly IConcessionTransactionalRepository _concessionTransactionalRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The financial transactional repository
        /// </summary>
        private readonly IFinancialTransactionalRepository _financialTransactionalRepository;

        /// <summary>
        /// The loaded price transactional repository
        /// </summary>
        private readonly ILoadedPriceTransactionalRepository _loadedPriceTransactionalRepository;

        /// <summary>
        /// The rule manager
        /// </summary>
        private readonly IRuleManager _ruleManager;

        private readonly IMediator _mediator;

        private readonly ITransactionTableNumberRepository _transactionTableNumberRepository;


        /// <summary>
        /// The misc performance repository
        /// </summary>
        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalManager"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionTransactionalRepository">The concession transactional repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="financialTransactionalRepository">The financial transactional repository.</param>
        /// <param name="loadedPriceTransactionalRepository">The loaded price transactional repository.</param>
        /// <param name="ruleManager">The rule manager.</param>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        public TransactionalManager(IConcessionManager concessionManager,
            IConcessionTransactionalRepository concessionTransactionalRepository, IMapper mapper,
            ILookupTableManager lookupTableManager, IFinancialTransactionalRepository financialTransactionalRepository,
            ILoadedPriceTransactionalRepository loadedPriceTransactionalRepository, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IMediator mediator, ITransactionTableNumberRepository transactionTableNumberRepository)
        {
            _concessionManager = concessionManager;
            _concessionTransactionalRepository = concessionTransactionalRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _financialTransactionalRepository = financialTransactionalRepository;
            _loadedPriceTransactionalRepository = loadedPriceTransactionalRepository;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;
            _transactionTableNumberRepository = transactionTableNumberRepository;

        }

        /// <summary>
        /// Gets the transactional concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public TransactionalConcession GetTransactionalConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
            var transactionalConcessionDetails =
                _miscPerformanceRepository.GetTransactionalConcessionDetails(concession.Id);

            return new TransactionalConcession
            {
                Concession = concession,
                TransactionalConcessionDetails = transactionalConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user
            };
        }

        /// <summary>
        /// Creates the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionTransactional CreateConcessionTransactional(
            TransactionalConcessionDetail transactionalConcessionDetail,
            Concession concession)
        {
            var concessionTransactional = _mapper.Map<ConcessionTransactional>(transactionalConcessionDetail);
            concessionTransactional.ConcessionId = concession.Id;
            return _concessionTransactionalRepository.Create(concessionTransactional);
        }

        /// <summary>
        /// Updates the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionTransactional UpdateConcessionTransactional(
            TransactionalConcessionDetail transactionalConcessionDetail,
            Concession concession)
        {
            var mappedConcessionTransactional = _mapper.Map<ConcessionTransactional>(transactionalConcessionDetail);
            mappedConcessionTransactional.ConcessionId = concession.Id;

            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                UpdateApprovedTransactionTableNumber(mappedConcessionTransactional);
                UpdateIsMismatched(mappedConcessionTransactional);

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionTransactional);
            }
            else if (concession.Status == Constants.ConcessionStatus.Pending &&
                     concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
            {
                UpdateApprovedTransactionTableNumber(mappedConcessionTransactional);
            }

            _concessionTransactionalRepository.Update(mappedConcessionTransactional);

            return mappedConcessionTransactional;
        }

        /// <summary>
        /// Updates the approved transaction table number.
        /// </summary>
        /// <param name="mappedConcessionTransactional">The mapped concession transactional.</param>
        private void UpdateApprovedTransactionTableNumber(ConcessionTransactional mappedConcessionTransactional)
        {
            var databaseTransactionalConcession =
                _concessionTransactionalRepository.ReadById(mappedConcessionTransactional.Id);

            if (databaseTransactionalConcession.ApprovedTransactionTableNumberId.HasValue)
            {
                mappedConcessionTransactional.ApprovedTransactionTableNumberId =
                    databaseTransactionalConcession.ApprovedTransactionTableNumberId;
            }
            else
            {
                //the approved table number is the table number that was captured when approving
                mappedConcessionTransactional.ApprovedTransactionTableNumberId =
                    mappedConcessionTransactional.TransactionTableNumberId;

                //the table number is what is currently in the database
                mappedConcessionTransactional.TransactionTableNumberId =
                    databaseTransactionalConcession.TransactionTableNumberId;
            }
        }


        public Model.Repository.TransactionTableNumber CreateupdateTransactionTableNumber(Model.UserInterface.Transactional.TransactionTableNumber transactionTableNumber)
        {           

            return _transactionTableNumberRepository.CreateupdateTransactionTableNumber(_mapper.Map<Model.Repository.TransactionTableNumber>(transactionTableNumber));
        }


        public TransactionType CreateTransactionType(TransactionType transactionType)
        {
           return _transactionTableNumberRepository.Create(transactionType);
        }

        public IEnumerable<Model.UserInterface.Transactional.TransactionTableNumber> GetTransactionTableNumbers(bool isActive)
        {
            return _mapper.Map<IEnumerable<Model.UserInterface.Transactional.TransactionTableNumber>>( _transactionTableNumberRepository.ReadAll());
        }

        /// <summary>
        /// Updates the is mismatched.
        /// </summary>
        /// <param name="mappedConcessionTransactional">The mapped concession transactional.</param>
        private void UpdateIsMismatched(ConcessionTransactional mappedConcessionTransactional)
        {
            mappedConcessionTransactional.IsMismatched = false;

            if (mappedConcessionTransactional.TransactionTypeId.HasValue)
            {
                var loadedPriceTransactional =
                    _loadedPriceTransactionalRepository.ReadByTransactionTypeIdLegalEntityAccountId(
                        mappedConcessionTransactional.TransactionTypeId.Value,
                        mappedConcessionTransactional.LegalEntityAccountId);

                if (loadedPriceTransactional != null)
                {
                    mappedConcessionTransactional.LoadedTransactionTableNumberId =
                        loadedPriceTransactional.TransactionTableNumberId;

                    if (loadedPriceTransactional.TransactionTableNumberId !=
                        mappedConcessionTransactional.ApprovedTransactionTableNumberId)
                        mappedConcessionTransactional.IsMismatched = true;
                }
                else
                {
                    mappedConcessionTransactional.IsMismatched = true;
                }
            }
            else
            {
                mappedConcessionTransactional.IsMismatched = true;
            }
        }

        /// <summary>
        /// Gets the transactional view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public TransactionalView GetTransactionalViewData(int riskGroupNumber)
        {
            var transactionalConcessions = new List<TransactionalConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
            var concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Transactional);

            foreach (var concession in concessions)
            {
                transactionalConcessions.Add(new TransactionalConcession
                {
                    Concession = concession,
                    TransactionalConcessionDetails =
                        _miscPerformanceRepository.GetTransactionalConcessionDetails(concession.Id)
                });
            }

            var transactionalFinancial =
                _mapper.Map<TransactionalFinancial>(
                    _financialTransactionalRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                    new FinancialTransactional());

            var transactionalProducts = GetTransactionalProducts(riskGroup);

            return new TransactionalView
            {
                RiskGroup = riskGroup,
                TransactionalConcessions = transactionalConcessions.OrderByDescending(_ => _.Concession.DateOpened),
                TransactionalFinancial = transactionalFinancial,
                TransactionalProducts = transactionalProducts
            };
        }

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public decimal GetLatestCrsOrMrs(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            var transactionFinancial =
                _financialTransactionalRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                new FinancialTransactional();

            return transactionFinancial.LatestCrsOrMrs;
        }

        /// <summary>
        /// Gets the transactional financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public TransactionalFinancial GetTransactionalFinancialForRiskGroupNumber(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            return _mapper.Map<TransactionalFinancial>(
                _financialTransactionalRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                new FinancialTransactional());
        }

        /// <summary>
        /// Deletes the concession transactional.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <returns></returns>
        public ConcessionTransactional DeleteConcessionTransactional(
            TransactionalConcessionDetail transactionalConcessionDetail)
        {
            var concessionTransactional =
                _concessionTransactionalRepository.ReadById(transactionalConcessionDetail
                    .TransactionalConcessionDetailId);

            _concessionTransactionalRepository.Delete(concessionTransactional);

            return concessionTransactional;
        }

        /// <summary>
        /// Gets the transactional products.
        /// </summary>
        /// <param name="riskGroup">The risk group.</param>
        /// <returns></returns>
        private IEnumerable<TransactionalProduct> GetTransactionalProducts(RiskGroup riskGroup)
        {
            return _miscPerformanceRepository.GetTransactionalProducts(riskGroup.Id, riskGroup.Name);
        }

        public async Task ForwardTransactionalConcession(TransactionalConcession transactionalConcession, User user)
        {
            var databaseTransactionalConcession =
                this.GetTransactionalConcession(transactionalConcession.Concession.ReferenceNumber,
                    user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseTransactionalConcession.ConcessionConditions)
                if (transactionalConcession.ConcessionConditions.All(
                    _ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any cash concession details that have been removed delete them
            foreach (var transactionalConcessionDetail in databaseTransactionalConcession
                .TransactionalConcessionDetails)
                if (transactionalConcession.TransactionalConcessionDetails.All(
                    _ => _.TransactionalConcessionDetailId !=
                         transactionalConcessionDetail.TransactionalConcessionDetailId))
                    await _mediator.Send(new DeleteTransactionalConcessionDetail(transactionalConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(transactionalConcession.Concession, user));

            //add all the new conditions and cash details and comments
            foreach (var transactionalConcessionDetail in transactionalConcession.TransactionalConcessionDetails)
                await _mediator.Send(
                    new AddOrUpdateTransactionalConcessionDetail(transactionalConcessionDetail, user, concession));

            if (transactionalConcession.ConcessionConditions != null &&
                transactionalConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in transactionalConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(transactionalConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id,
                    databaseTransactionalConcession.Concession.SubStatusId,
                    transactionalConcession.Concession.Comments, user));

            //send the notification email
            await _mediator.Send(new ForwardConcession(transactionalConcession.Concession, user));
        }
    }
}