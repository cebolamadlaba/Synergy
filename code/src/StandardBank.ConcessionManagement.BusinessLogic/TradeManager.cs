using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.TradeConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class TradeManager : ITradeManager
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        private readonly IConcessionTradeRepository _concessionTradeRpository;

        private readonly IMapper _mapper;

        private readonly IFinancialTradeRepository _financialTradeRepository;

        private readonly ILookupTableManager _lookupTableManager;

        private readonly IRuleManager _ruleManager;

        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        private readonly IMediator _mediator;

        public TradeManager(IConcessionManager concessionManager, IConcessionTradeRepository concessionTradeRpository,
            IMapper mapper, IFinancialTradeRepository financialTradeRepository, ILookupTableManager lookupTableManager, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IMediator mediator)
        {
            _concessionManager = concessionManager;
            _concessionTradeRpository = concessionTradeRpository;
            _mapper = mapper;
            _financialTradeRepository = financialTradeRepository;
            _lookupTableManager = lookupTableManager;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;
        }

        public ConcessionTrade CreateConcessionTrade(TradeConcessionDetail tradeConcessionDetail, Concession concession)
        {
            var concessionTrade = _mapper.Map<ConcessionTrade>(tradeConcessionDetail);
            concessionTrade.ConcessionId = concession.Id;
            return _concessionTradeRpository.Create(concessionTrade);
        }

        public TradeConcession GetTradeConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId, user);
            var tradeConcessionDetails = _miscPerformanceRepository.GetTradeConcessionDetails(concession.Id).Where(x => x.TradeProductType != null);

            return new TradeConcession
            {
                Concession = concession,
                TradeConcessionDetails = tradeConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user
            };
        }

        public ConcessionTrade DeleteConcessionTrade(TradeConcessionDetail tradeConcessionDetail)
        {
            var concessionBol = _concessionTradeRpository.ReadById(tradeConcessionDetail.TradeConcessionDetailId);

            _concessionTradeRpository.Delete(concessionBol);
            return concessionBol;
        }

        public ConcessionTrade UpdateConcessionTrade(TradeConcessionDetail tradeConcessionDetail, Concession concession)
        {
            var mappedConcessionTrade = _mapper.Map<ConcessionTrade>(tradeConcessionDetail);
            mappedConcessionTrade.ConcessionId = concession.Id;
            mappedConcessionTrade.Id = tradeConcessionDetail.TradeConcessionDetailId;

            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                //Rate becomes approved rate
                mappedConcessionTrade.ApprovedRate = mappedConcessionTrade.Rate;

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionTrade);

                var productType = _lookupTableManager.GetTradeProducTypeName(tradeConcessionDetail.fkTradeProductId.Value);

                // Set Expiry Date for Local Guarantee.
                switch (productType)
                {
                    case Constants.Trade.TradeProductType.LocalGuarantee:
                        mappedConcessionTrade.ExpiryDate = DateTime.Now.AddMonths(mappedConcessionTrade.term.Value);
                        break;

                    case Constants.Trade.TradeProductType.InwardTT:
                    case Constants.Trade.TradeProductType.OutwardTT:
                        if (mappedConcessionTrade.AdValorem.HasValue)
                            mappedConcessionTrade.ApprovedRate = mappedConcessionTrade.AdValorem.Value;
                        else if (mappedConcessionTrade.FlatFee.HasValue)
                            mappedConcessionTrade.ApprovedRate = mappedConcessionTrade.FlatFee.Value;
                        break;
                }
            }
            else if (concession.Status == Constants.ConcessionStatus.Pending &&
                     concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
            {
                //Rate becomes approved rate
                mappedConcessionTrade.ApprovedRate = mappedConcessionTrade.Rate;
            }

            _concessionTradeRpository.Update(mappedConcessionTrade);

            return mappedConcessionTrade;
        }

        public TradeView GetTradeViewData(int riskGroupNumber, int sapbpid, User currentUser)
        {
            bool hasOnlySapBpId = riskGroupNumber < 1 && sapbpid > 0;

            List<TradeConcession> tradeConcessions = new List<TradeConcession>();
            RiskGroup riskGroup = null;
            Model.UserInterface.LegalEntity legalEntity = null;
            IEnumerable<Concession> concessions = null;
            TradeFinancial tradeFinancial = null;
            IEnumerable<Model.UserInterface.Trade.TradeProduct> tradeProducts = null;

            if (!hasOnlySapBpId)
            {
                riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
                concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Trade, currentUser);
                tradeFinancial = _mapper.Map<TradeFinancial>(_financialTradeRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialTrade());
                tradeProducts = GetTradeProducts(riskGroup);
            }
            else
            {
                legalEntity = _lookupTableManager.GetLegalEntity(sapbpid);
                concessions = _concessionManager.GetApprovedConcessionsForLegalEntityId(legalEntity.Id, Constants.ConcessionType.Trade, currentUser);
                tradeFinancial = new TradeFinancial()
                {
                    AvgFee = 0,
                    OverallForexMargin = 0,
                    TotalAccounts = 0
                };
                tradeProducts = GetTradeProductsBySAPBPID(legalEntity);
            }

            foreach (var concession in concessions)
            {
                tradeConcessions.Add(new TradeConcession
                {
                    TradeConcessionDetails = _miscPerformanceRepository.GetTradeConcessionDetails(concession.Id),
                    Concession = concession
                });
            }

            //grouping of products
            var groupedinfo = new List<TradeProductGroup>();
            if (tradeProducts != null)
            {
                foreach (var product in tradeProducts)
                {
                    var productgrouping = groupedinfo.Where(g => g.LegalEntity == product.LegalEntity).FirstOrDefault();
                    if (productgrouping == null)
                    {
                        TradeProductGroup newgroup = new TradeProductGroup
                        {
                            LegalEntity = product.LegalEntity,
                            RiskGroupName = product.RiskGroupName,
                            TradeProducts = new List<Model.UserInterface.Trade.TradeProduct>()
                        };
                        newgroup.TradeProducts.Add(product);

                        groupedinfo.Add(newgroup);
                    }
                    else
                    {
                        productgrouping.TradeProducts.Add(product);
                    }
                }
                //sort
                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.TradeProducts != null)
                    {
                        productgrouping.TradeProducts = productgrouping.TradeProducts.OrderBy(o => o.AccountNumber).ThenBy(o => o.TradeProductType).ToList();
                    }
                }
            }

            return new TradeView
            {
                RiskGroup = riskGroup,
                LegalEntity = legalEntity,
                TradeConcessions = tradeConcessions.OrderBy(_ => _.Concession.AccountNumber),
                TradeFinancial = tradeFinancial,
                TradeProductGroups = groupedinfo.OrderBy(o => o.LegalEntity)
            };
        }

        private IEnumerable<Model.UserInterface.Trade.TradeProduct> GetTradeProducts(RiskGroup riskGroup)
        {
            return _miscPerformanceRepository.GetTradeProducts(riskGroup.Id, riskGroup.Name);
        }

        private IEnumerable<Model.UserInterface.Trade.TradeProduct> GetTradeProductsBySAPBPID(Model.UserInterface.LegalEntity legalEntity)
        {
            return _miscPerformanceRepository.GetTradeProductsBySAPBPID(legalEntity.Id, legalEntity.CustomerName);
        }

        public async Task ForwardTradeConcession(TradeConcession tradeConcession, User user)
        {
            var databaseTradeConcession =
              this.GetTradeConcession(tradeConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseTradeConcession.ConcessionConditions)
            {
                if (tradeConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                {
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));
                }
            }

            //if there are any bol concession details that have been removed delete them
            foreach (var tradeConcessionDetail in databaseTradeConcession.TradeConcessionDetails)
            {
                if (tradeConcession.TradeConcessionDetails.All(_ => _.TradeConcessionDetailId !=
                                                                                  tradeConcessionDetail.TradeConcessionDetailId))
                {
                    await _mediator.Send(new DeleteTradeConcessionDetail(tradeConcessionDetail, user));
                }
            }

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(tradeConcession.Concession, user));

            //add all the new conditions and bol details and comments
            foreach (var tradeConcessionDetail in tradeConcession.TradeConcessionDetails)
            {
                await _mediator.Send(new AddOrUpdateTradeConcessionDetail(tradeConcessionDetail, user, concession));
            }

            if (tradeConcession.ConcessionConditions != null && tradeConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in tradeConcession.ConcessionConditions)
                {
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            if (!string.IsNullOrWhiteSpace(tradeConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseTradeConcession.Concession.SubStatusId,
                                   tradeConcession.Concession.Comments, user));
            }

            //send the notification email
            await _mediator.Send(new ForwardConcession(tradeConcession.Concession, user));
        }
    }
}