using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.BolConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;
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

        //public ConcessionBol CreateConcessionTrade(TradeConcessionDetail tradeConcessionDetail, Concession concession)
        //{
        //    //var concessionBol = _mapper.Map<ConcessionBol>(tradeConcessionDetail);
        //    //concessionBol.ConcessionId = concession.Id;
        //    //return _concessionBolRepository.Create(concessionBol);
        //}

        //public BolConcession GetBolConcession(string concessionReferenceId, User user)
        //{
        //    var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);
        //    var bolConcessionDetails = _miscPerformanceRepository.GetBolConcessionDetails(concession.Id);

        //    return new BolConcession
        //    {
        //        Concession = concession,
        //        BolConcessionDetails = bolConcessionDetails,
        //        ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
        //        CurrentUser = user
        //    };

        //}

        //public ConcessionBol DeleteConcessionBol(BolConcessionDetail cashConcessionDetail)
        //{
        //    var concessionBol = _concessionBolRepository.ReadById(cashConcessionDetail.BolConcessionDetailId);

        //    _concessionBolRepository.Delete(concessionBol);
        //    return concessionBol;

        //}

        //public Model.UserInterface.Bol.BOLChargeCode CreateUpdateBOLChargeCode(Model.UserInterface.Bol.BOLChargeCode bolchargecode)
        //{
        //    var mappedbol = _mapper.Map<Model.Repository.BOLChargeCode>(bolchargecode);
        //    var returned = _concessionBolRepository.CreateUpdate(mappedbol);

        //    bolchargecode.pkChargeCodeId = returned.pkChargeCodeId;
        //    return bolchargecode;

        //}

        //public Model.UserInterface.Bol.BOLChargeCodeType CreateBOLChargeCodeType(Model.UserInterface.Bol.BOLChargeCodeType bolchargecodetype)
        //{
        //    var mappedboltype = _mapper.Map<Model.Repository.BOLChargeCodeType>(bolchargecodetype);
        //    var returned = _concessionBolRepository.Create(mappedboltype);

        //    bolchargecodetype.pkChargeCodeTypeId = returned.pkChargeCodeTypeId;
        //    return bolchargecodetype;

        //}

        //public ConcessionBol UpdateConcessionBol(BolConcessionDetail bolConcessionDetail, Concession concession)
        //{
        //    var mappedConcessionBol = _mapper.Map<ConcessionBol>(bolConcessionDetail);
        //    mappedConcessionBol.ConcessionId = concession.Id;
        //    mappedConcessionBol.Id = bolConcessionDetail.BolConcessionDetailId;

        //    if (concession.Status == Constants.ConcessionStatus.Approved ||
        //        concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
        //    {
        //        //Loaded rate becomes approved rate
        //        mappedConcessionBol.ApprovedRate = mappedConcessionBol.LoadedRate;

        //        _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionBol);
        //    }
        //    else if (concession.Status == Constants.ConcessionStatus.Pending &&
        //             concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
        //    {

        //        //Loaded rate becomes approved rate
        //        mappedConcessionBol.ApprovedRate = mappedConcessionBol.LoadedRate;
        //    }

        //    _concessionBolRepository.Update(mappedConcessionBol);

        //    return mappedConcessionBol;
        //}



        public TradeView GetTradeViewData(int riskGroupNumber)
        {
            var tradeConcessions = new List<TradeConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            var concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Trade);

            foreach (var concession in concessions)
            {
                tradeConcessions.Add(new TradeConcession
                {
                    TradeConcessionDetails = _miscPerformanceRepository.GetTradeConcessionDetails(concession.Id),
                    Concession = concession
                });
            }

            var bolFinancial = _mapper.Map<TradeFinancial>(_financialTradeRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialTrade());
                      

            var tradeProducts = GetTradeProducts(riskGroup);

            return new TradeView
            {
                RiskGroup = riskGroup,
                TradeConcessions = tradeConcessions.OrderByDescending(_ => _.Concession.DateOpened),
                TradeFinancial = bolFinancial,
                TradeProducts = tradeProducts
            };
        }


        private IEnumerable<TradeProduct> GetTradeProducts(RiskGroup riskGroup)
        {
            return _miscPerformanceRepository.GetTradeProducts(riskGroup.Id, riskGroup.Name);
        }



       // public BolFinancial GetBolFinancialForRiskGroupNumber(int riskGroupNumber)
       // {
            //var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            //return _mapper.Map<BolFinancial>(
            //    _financialBolRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialBol());
       // }

        //public async Task ForwardBolConcession(BolConcession bolConcession, User user)
       // {
            //var databaseBolConcession =
            //  this.GetBolConcession(bolConcession.Concession.ReferenceNumber, user);

            ////if there are any conditions that have been removed, delete them
            //foreach (var condition in databaseBolConcession.ConcessionConditions)
            //    if (bolConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
            //        await _mediator.Send(new DeleteConcessionCondition(condition, user));

            ////if there are any bol concession details that have been removed delete them
            //foreach (var bolConcessionDetail in databaseBolConcession.BolConcessionDetails)
            //    if (bolConcession.BolConcessionDetails.All(_ => _.BolConcessionDetailId !=
            //                                                      bolConcessionDetail.BolConcessionDetailId))
            //        await _mediator.Send(new DeleteBolConcessionDetail(bolConcessionDetail, user));

            ////update the concession
            //var concession = await _mediator.Send(new UpdateConcession(bolConcession.Concession, user));

            ////add all the new conditions and bol details and comments
            //foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
            //    await _mediator.Send(new AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));

            //if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
            //    foreach (var concessionCondition in bolConcession.ConcessionConditions)
            //        await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            //if (!string.IsNullOrWhiteSpace(bolConcession.Concession.Comments))
            //    await _mediator.Send(new AddConcessionComment(concession.Id, databaseBolConcession.Concession.SubStatusId,
            //        bolConcession.Concession.Comments, user));

            ////send the notification email
            //await _mediator.Send(new ForwardConcession(bolConcession.Concession, user));
       // }
    }
}