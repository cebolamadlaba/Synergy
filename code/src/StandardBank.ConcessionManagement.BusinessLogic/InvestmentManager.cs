using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// The Investment Concession Manager
    /// </summary>
    public class InvestmentManager : IInvestmentManager
    {
        private readonly IConcessionManager _concessionManager;

        private readonly IConcessionInvestmentRepository _concessionInvestmentRpository;

        private readonly IMapper _mapper;

        private readonly IFinancialInvestmentRepository _financialInvestmentRepository;

        private readonly ILookupTableManager _lookupTableManager;

        private readonly IRuleManager _ruleManager;

        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        private readonly IMediator _mediator;

        private readonly IPrimeRateRepository _primeRateRepository;

        public InvestmentManager(IConcessionManager concessionManager, IConcessionInvestmentRepository concessionInvestmentRpository,
            IMapper mapper, IFinancialInvestmentRepository financialInvestmentRepository, ILookupTableManager lookupTableManager, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IMediator mediator, IPrimeRateRepository primeRateRepository)
        {
            _concessionManager = concessionManager;
            _concessionInvestmentRpository = concessionInvestmentRpository;
            _mapper = mapper;
            _financialInvestmentRepository = financialInvestmentRepository;
            _lookupTableManager = lookupTableManager;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;
            _primeRateRepository = primeRateRepository;
        }

        public ConcessionInvestment CreateConcessionInvestment(InvestmentConcessionDetail investmentConcessionDetail, Concession concession)
        {
            var concessionInvestment = _mapper.Map<ConcessionInvestment>(investmentConcessionDetail);
            concessionInvestment.ConcessionId = concession.Id;

            return _concessionInvestmentRpository.Create(concessionInvestment);
        }

        public InvestmentConcession GetInvestmentConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId, user);
            var investmentConcessionDetails = _miscPerformanceRepository.GetInvestmentConcessionDetails(concession.Id);

            var primerate = _primeRateRepository.PrimeRate(concession.DateOpened);

            return new InvestmentConcession
            {
                Concession = concession,
                InvestmentConcessionDetails = investmentConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user,
                PrimeRate = primerate
            };
        }

        public ConcessionInvestment DeleteConcessionInvestment(InvestmentConcessionDetail investmentConcessionDetail)
        {
            var concessionInvestment = _concessionInvestmentRpository.ReadById(investmentConcessionDetail.InvestmentConcessionDetailId);
            _concessionInvestmentRpository.Delete(concessionInvestment);

            return concessionInvestment;
        }

        public ConcessionInvestment UpdateConcessionInvestment(InvestmentConcessionDetail investmentConcessionDetail, Concession concession)
        {
            var mappedConcessionInvestment = _mapper.Map<ConcessionInvestment>(investmentConcessionDetail);
            mappedConcessionInvestment.ConcessionId = concession.Id;
            mappedConcessionInvestment.Id = investmentConcessionDetail.InvestmentConcessionDetailId;

            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                //Loaded rate becomes approved rate
                mappedConcessionInvestment.ApprovedRate = mappedConcessionInvestment.LoadedRate;

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionInvestment);

                if (!investmentConcessionDetail.ExpiryDate.HasValue)
                {
                    var productType = _lookupTableManager.GetProductTypeName(investmentConcessionDetail.ProductTypeId.Value);

                    if (productType == Constants.Investment.ProductType.NoticeDeposit)
                        mappedConcessionInvestment.ExpiryDate = System.DateTime.Now.AddMonths(investmentConcessionDetail.Term);
                }
            }
            else if (concession.Status == Constants.ConcessionStatus.Pending &&
                     concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
            {
                //Loaded rate becomes approved rate
                mappedConcessionInvestment.ApprovedRate = mappedConcessionInvestment.LoadedRate;

                if (!investmentConcessionDetail.ExpiryDate.HasValue)
                {
                    var productType = _lookupTableManager.GetProductTypeName(investmentConcessionDetail.ProductTypeId.Value);

                    if (productType == Constants.Investment.ProductType.NoticeDeposit)
                        mappedConcessionInvestment.ExpiryDate = System.DateTime.Now.AddMonths(investmentConcessionDetail.Term);
                }
            }

            _concessionInvestmentRpository.Update(mappedConcessionInvestment);

            return mappedConcessionInvestment;
        }

        public InvestmentView GetInvestmentViewData(int riskGroupNumber, int sapbpid, User currentUser)
        {
            bool hasOnlySapBpId = riskGroupNumber < 1 && sapbpid > 0;

            var investmentConcessions = new List<InvestmentConcession>();
            RiskGroup riskGroup = null;
            Model.UserInterface.LegalEntity legalEntity = null;
            IEnumerable<Concession> concessions = null;
            InvestmentFinancial investmentFinancial = null;
            IEnumerable<Model.UserInterface.Investment.InvestmentProduct> investmentProducts = null;
            if (!hasOnlySapBpId)
            {
                riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
                concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Investment, currentUser);
                investmentFinancial = _mapper.Map<InvestmentFinancial>(_financialInvestmentRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialInvestment());
                investmentProducts = GetInvestmentProducts(riskGroup);
            }
            else
            {
                legalEntity = _lookupTableManager.GetLegalEntity(sapbpid);
                concessions = _concessionManager.GetApprovedConcessionsForLegalEntityId(legalEntity.Id, Constants.ConcessionType.Investment, currentUser);
                investmentFinancial = new InvestmentFinancial()
                {
                    TotalLiabilityBalances = 0,
                    WeightedAverageMTP = 0,
                    WeightedAverageNetMargin = 0
                };
                investmentProducts = GetInvestmentProductsByLegalEntity(legalEntity);
            }

            foreach (var concession in concessions)
            {
                investmentConcessions.Add(new InvestmentConcession
                {
                    InvestmentConcessionDetails = _miscPerformanceRepository.GetInvestmentConcessionDetails(concession.Id),
                    Concession = concession
                });
            }

            //grouping of products
            var groupedinfo = new List<InvestmentProductGroup>();
            if (investmentProducts != null)
            {
                foreach (var product in investmentProducts)
                {
                    var productgrouping = groupedinfo.Where(g => g.LegalEntity == product.LegalEntity).FirstOrDefault();
                    if (productgrouping == null)
                    {
                        InvestmentProductGroup newgroup = new InvestmentProductGroup
                        {
                            LegalEntity = product.LegalEntity,
                            RiskGroupName = product.RiskGroupName,
                            InvestmentProducts = new List<Model.UserInterface.Investment.InvestmentProduct>()
                        };
                        newgroup.InvestmentProducts.Add(product);

                        groupedinfo.Add(newgroup);
                    }
                    else
                    {
                        productgrouping.InvestmentProducts.Add(product);
                    }
                }
                //sort
                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.InvestmentProducts != null)
                    {
                        productgrouping.InvestmentProducts = productgrouping.InvestmentProducts.OrderBy(o => o.AccountNumber).ThenBy(o => o.InvestmentProductType).ToList();
                    }
                }
            }

            return new InvestmentView
            {
                RiskGroup = riskGroup,
                LegalEntity = legalEntity,
                InvestmentConcessions = investmentConcessions.OrderBy(_ => _.Concession.AccountNumber),
                InvestmentFinancial = investmentFinancial,
                InvestmentProductGroups = groupedinfo.OrderBy(o => o.LegalEntity)
            };
        }

        private IEnumerable<Model.UserInterface.Investment.InvestmentProduct> GetInvestmentProducts(RiskGroup riskGroup)
        {
            return _miscPerformanceRepository.GetInvestmentProducts(riskGroup.Id, riskGroup.Name);
        }

        private IEnumerable<Model.UserInterface.Investment.InvestmentProduct> GetInvestmentProductsByLegalEntity(Model.UserInterface.LegalEntity legalEntity)
        {
            return _miscPerformanceRepository.GetInvestmentProductsByLegalEntity(legalEntity.Id, legalEntity.CustomerName);
        }

        public async Task ForwardInvestmentConcession(InvestmentConcession investmentConcession, User user)
        {
            var databaseInvestmentConcession =
              this.GetInvestmentConcession(investmentConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseInvestmentConcession.ConcessionConditions)
            {
                if (investmentConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                {
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));
                }
            }

            //if there are any concession details that have been removed delete them
            foreach (var investmentConcessionDetail in databaseInvestmentConcession.InvestmentConcessionDetails)
            {
                if (investmentConcession.InvestmentConcessionDetails.All(_ => _.InvestmentConcessionDetailId !=
                                                                  investmentConcessionDetail.InvestmentConcessionDetailId))
                {
                    await _mediator.Send(new DeleteInvestmentConcessionDetail(investmentConcessionDetail, user));
                }
            }

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(investmentConcession.Concession, user));

            //add all the new conditions and details and comments
            foreach (var investmentConcessionDetail in investmentConcession.InvestmentConcessionDetails)
            {
                await _mediator.Send(new AddOrUpdateInvestmentConcessionDetail(investmentConcessionDetail, user, concession));
            }

            if (investmentConcession.ConcessionConditions != null && investmentConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in investmentConcession.ConcessionConditions)
                {
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            if (!string.IsNullOrWhiteSpace(investmentConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseInvestmentConcession.Concession.SubStatusId,
                    investmentConcession.Concession.Comments, user));
            }

            //send the notification email
            await _mediator.Send(new ForwardConcession(investmentConcession.Concession, user));
        }
    }
}