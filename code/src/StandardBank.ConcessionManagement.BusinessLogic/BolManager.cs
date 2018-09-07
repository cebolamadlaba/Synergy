﻿using System.Collections.Generic;
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

        private readonly IMediator _mediator;


        public BolManager(IConcessionManager concessionManager, IConcessionBolRepository concessionBolRepository,
            IMapper mapper, IFinancialBolRepository financialBolRepository, ILookupTableManager lookupTableManager, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IMediator mediator)
        {
            _concessionManager = concessionManager;
            _concessionBolRepository = concessionBolRepository;
            _mapper = mapper;
            _financialBolRepository = financialBolRepository;
            _lookupTableManager = lookupTableManager;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;


        }

        public ConcessionBol CreateConcessionBol(BolConcessionDetail bolConcessionDetail, Concession concession)
        {
            var concessionBol = _mapper.Map<ConcessionBol>(bolConcessionDetail);
            concessionBol.ConcessionId = concession.Id;
            return _concessionBolRepository.Create(concessionBol);

        }


        public BolConcession GetBolConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId);

            var bolConcessionDetails = _miscPerformanceRepository.GetBolConcessionDetails(concession.Id);

            return new BolConcession
            {
                Concession = concession,
                BolConcessionDetails = bolConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user
            };

        }

        public ConcessionBol DeleteConcessionBol(BolConcessionDetail cashConcessionDetail)
        {
            var concessionBol = _concessionBolRepository.ReadById(cashConcessionDetail.BolConcessionDetailId);

            _concessionBolRepository.Delete(concessionBol);

            return concessionBol;

        }

        public Model.UserInterface.Bol.BOLChargeCode CreateUpdateBOLChargeCode(Model.UserInterface.Bol.BOLChargeCode bolchargecode)
        {
            var mappedbol = _mapper.Map<Model.Repository.BOLChargeCode>(bolchargecode);
            var returned = _concessionBolRepository.CreateUpdate(mappedbol);

            bolchargecode.pkChargeCodeId = returned.pkChargeCodeId;
            return bolchargecode;

        }

        public Model.UserInterface.Bol.BOLChargeCodeType CreateBOLChargeCodeType(Model.UserInterface.Bol.BOLChargeCodeType bolchargecodetype)
        {
            var mappedboltype = _mapper.Map<Model.Repository.BOLChargeCodeType>(bolchargecodetype);
            var returned = _concessionBolRepository.Create(mappedboltype);

            bolchargecodetype.pkChargeCodeTypeId = returned.pkChargeCodeTypeId;
            return bolchargecodetype;

        }

        public ConcessionBol UpdateConcessionBol(BolConcessionDetail bolConcessionDetail, Concession concession)
        {
            var mappedConcessionBol = _mapper.Map<ConcessionBol>(bolConcessionDetail);
            mappedConcessionBol.ConcessionId = concession.Id;
            mappedConcessionBol.Id = bolConcessionDetail.BolConcessionDetailId;

            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                //Loaded rate becomes approved rate
                mappedConcessionBol.ApprovedRate = mappedConcessionBol.LoadedRate;

                _ruleManager.UpdateBaseFieldsOnApproval(mappedConcessionBol);
            }
            else if (concession.Status == Constants.ConcessionStatus.Pending &&
                     concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
            {

                //Loaded rate becomes approved rate
                mappedConcessionBol.ApprovedRate = mappedConcessionBol.LoadedRate;
            }

            _concessionBolRepository.Update(mappedConcessionBol);

            return mappedConcessionBol;
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
                    BolConcessionDetails = _miscPerformanceRepository.GetBolConcessionDetails(concession.Id).OrderBy(o => o.BolUserID),
                    Concession = concession
                });
            }

            var bolFinancial =
                _mapper.Map<BolFinancial>(_financialBolRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ??
                                           new FinancialBol());

            var bolProducts = GetBolProducts(riskGroup).OrderBy(o => o.BOLUserId);

            //grouping of products
            var groupedinfo = new List<BolProductGroup>();
            if (bolProducts != null)
            {
                foreach (var product in bolProducts)
                {
                    var productgrouping = groupedinfo.Where(g => g.LegalEntity == product.LegalEntity).FirstOrDefault();
                    if (productgrouping == null)
                    {
                        BolProductGroup newgroup = new BolProductGroup();
                        newgroup.LegalEntity = product.LegalEntity;
                        newgroup.RiskGroupName = product.RiskGroupName;
                        newgroup.BolProducts = new List<BolProduct>();
                        newgroup.BolProducts.Add(product);

                        groupedinfo.Add(newgroup);
                    }
                    else
                    {
                        productgrouping.BolProducts.Add(product);
                    }
                }
                //sort
                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.BolProducts != null)
                        productgrouping.BolProducts = productgrouping.BolProducts.OrderBy(o => o.BOLUserId).ThenBy(o => o.BolProductType).ToList();


                }
            }

            return new BolView
            {
                RiskGroup = riskGroup,
                BolConcessions = bolConcessions, //.OrderBy(_ => _.Concession.AccountNumber),
                BolFinancial = bolFinancial,
                BolProductGroups = groupedinfo.OrderBy(o => o.LegalEntity)
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

        public async Task ForwardBolConcession(BolConcession bolConcession, User user)
        {
            var databaseBolConcession =
              this.GetBolConcession(bolConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseBolConcession.ConcessionConditions)
                if (bolConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any bol concession details that have been removed delete them
            foreach (var bolConcessionDetail in databaseBolConcession.BolConcessionDetails)
                if (bolConcession.BolConcessionDetails.All(_ => _.BolConcessionDetailId !=
                                                                  bolConcessionDetail.BolConcessionDetailId))
                    await _mediator.Send(new DeleteBolConcessionDetail(bolConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(bolConcession.Concession, user));

            //add all the new conditions and bol details and comments
            foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
                await _mediator.Send(new AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));

            if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in bolConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(bolConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseBolConcession.Concession.SubStatusId,
                    bolConcession.Concession.Comments, user));

            //send the notification email
            await _mediator.Send(new ForwardConcession(bolConcession.Concession, user));
        }
    }
}