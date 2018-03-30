using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{

    public class BolManager : IBolManager
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

     
        private readonly IConcessionBolRepository _concessionBolRepository;
    
        private readonly IMapper _mapper;
     
        private readonly IFinancialBolRepository _financialBolRepository;

     
        private readonly ILookupTableManager _lookupTableManager;
      
        private readonly IRuleManager _ruleManager;
        /// <summary>
        /// The misc performance repository
        /// </summary>
        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

       
        public BolManager(IConcessionManager concessionManager, IConcessionBolRepository concessionBolRepository,
            IMapper mapper, IFinancialBolRepository financialBolRepository, ILookupTableManager lookupTableManager, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository)
        {
            _concessionManager = concessionManager;
            _concessionBolRepository = concessionBolRepository;
            _mapper = mapper;
            _financialBolRepository = financialBolRepository;
            _lookupTableManager = lookupTableManager;          
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
        }
              
        public ConcessionBol CreateConcessionBol(BolConcessionDetail bolConcessionDetail, Concession concession)
        {
            var concessionBol = _mapper.Map<ConcessionBol>(bolConcessionDetail);
            concessionBol.ConcessionId = concession.Id;
            return _concessionBolRepository.Create(concessionBol);
          
        }

       
        public BolConcession GetBolConcession(string concessionReferenceId, User user)
        {
            //var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);

            //var cashConcessionDetails = _miscPerformanceRepository.GetCashConcessionDetails(concession.Id);

            //return new BolConcession
            //{
            //    Concession = concession,
            //    CashConcessionDetails = cashConcessionDetails,
            //    ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
            //    CurrentUser = user
            //};
            return null;
        }

        public ConcessionCash DeleteConcessionBol(BolConcessionDetail cashConcessionDetail)
        {
            //var concessionCash = _concessionCashRepository.ReadById(cashConcessionDetail.CashConcessionDetailId);

            //_concessionCashRepository.Delete(concessionCash);

            //return concessionCash;
            return null;
        }


        public ConcessionCash UpdateConcessionBol(BolConcessionDetail cashConcessionDetail, Concession concession)
        {
            //var mappedConcessionCash = _mapper.Map<ConcessionCash>(cashConcessionDetail);
            //mappedConcessionCash.ConcessionId = concession.Id;

            //if (concession.Status == Constants.ConcessionStatus.Approved ||
            //    concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            //{
            //    UpdateApprovedTableNumber(mappedConcessionCash);
            //    UpdateIsMismatched(mappedConcessionCash);

            //    _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionCash);
            //}
            //else if (concession.Status == Constants.ConcessionStatus.Pending &&
            //         concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
            //{
            //    UpdateApprovedTableNumber(mappedConcessionCash);
            //}

            //_concessionCashRepository.Update(mappedConcessionCash);

            //return mappedConcessionCash;

            return null;
        }


      
        public BolView GetBolViewData(int riskGroupNumber)
        {
            var bolConcessions = new List<BolConcession>();
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
            var concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.BusinessOnline);

            foreach (var concession in concessions)
            {
                bolConcessions.Add(new BolConcession
                {
                    BolConcessionDetails = _miscPerformanceRepository.GetBolConcessionDetails(concession.Id),
                    Concession = concession
                });
            }

            var bolFinancial =
                _mapper.Map<BolFinancial>(_financialBolRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                                           new FinancialBol());

            var bolProducts = GetBolProducts(riskGroup);

            return new BolView
            {
                RiskGroup = riskGroup,
                BolConcessions = bolConcessions.OrderByDescending(_ => _.Concession.DateOpened),
                BolFinancial = bolFinancial,
                BolProducts = bolProducts
            };
        }


        private IEnumerable<BolProduct> GetBolProducts(RiskGroup riskGroup)
        {
            return _miscPerformanceRepository.GetBolProducts(riskGroup.Id, riskGroup.Name);
        }


       
        public BolFinancial GetBolFinancialForRiskGroupNumber(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            return _mapper.Map<BolFinancial>(
                _financialBolRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialBol());
        }
    }
}