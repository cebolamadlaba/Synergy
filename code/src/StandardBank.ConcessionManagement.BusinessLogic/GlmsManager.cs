﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{

    public class GlmsManager : IGlmsManager
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        private readonly IConcessionGlmsRepository _concessionGlmsRepository;

        private readonly IMapper _mapper;

        private readonly ILookupTableManager _lookupTableManager;

        private readonly IRuleManager _ruleManager;

        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        private readonly IMediator _mediator;

        private readonly IPrimeRateRepository _primeRateRepository;

        public GlmsManager(IConcessionManager concessionManager, IConcessionGlmsRepository concessionGlmsRepository,
            IMapper mapper,ILookupTableManager lookupTableManager, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IMediator mediator, IPrimeRateRepository primeRateRepository)
        {
            _concessionManager = concessionManager;
            _concessionGlmsRepository = concessionGlmsRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;
            _primeRateRepository = primeRateRepository;
        }

        public ConcessionGlms CreateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession)
        {
             var concessionGlms = MapGlms(glmsConcessionDetail);
             concessionGlms.ConcessionId = concession.Id;
             return _concessionGlmsRepository.Create(concessionGlms);
        }

        public GlmsConcession GetGlmsConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId, user);
            var glmsConcessionDetails = _miscPerformanceRepository.GetGlmsConcessionDetails(concession.Id);

            foreach(var glmsConcessionDetail in  glmsConcessionDetails)
            {
               // glmsConcessionDetail.GlmsTierData=
            }

            return new GlmsConcession
            {
                Concession = concession,
                GlmsConcessionDetails = glmsConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = user
            };

        }

        public ConcessionGlms DeleteConcessionGlms(GlmsConcessionDetail glmsConcessionDetail)
        {
            var concessionGlms = _concessionGlmsRepository.ReadById(glmsConcessionDetail.GlmsConcessionDetailId);

            _concessionGlmsRepository.Delete(concessionGlms);
            return concessionGlms;
        }

        public GlmsView GetGlmsViewData(int riskGroupNumber, int sapbpid, User currentUser)
        {
            bool hasOnlySapBpId = riskGroupNumber < 1 && sapbpid > 0;

            var GlmsConcessions = new List<GlmsConcession>();
            RiskGroup riskGroup = null;
            Model.UserInterface.LegalEntity legalEntity = null;
            IEnumerable<Concession> concessions = null;
            IEnumerable<Model.UserInterface.Glms.GlmsProduct> GlmsProducts = null;
            if (!hasOnlySapBpId)
            {
                riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
                concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Glms, currentUser);
                GlmsProducts = GetGlmsProducts(riskGroup);
            }
            else
            {
                legalEntity = _lookupTableManager.GetLegalEntity(sapbpid);
                concessions = _concessionManager.GetApprovedConcessionsForLegalEntityId(legalEntity.Id, Constants.ConcessionType.Glms, currentUser);
                GlmsProducts = GetGlmsProductsByLegalEntity(legalEntity);
            }

            foreach (var concession in concessions)
            {
                GlmsConcessions.Add(new GlmsConcession
                {
                   GlmsConcessionDetails = _miscPerformanceRepository.GetGlmsConcessionDetails(concession.Id),
                    Concession = concession
                });
            }

            ////grouping of products
            var groupedinfo = new List<GlmsProductGroup>();
            if (GlmsProducts != null)
            {
                foreach (var product in GlmsProducts)
                {
                    var productgrouping = groupedinfo.Where(g => g.LegalEntity == product.LegalEntity).FirstOrDefault();
                    if (productgrouping == null)
                    {
                        GlmsProductGroup newgroup = new GlmsProductGroup
                        {
                            LegalEntity = product.LegalEntity,
                            RiskGroupName = product.RiskGroupName,
                            GlmsProducts = new List<Model.UserInterface.Glms.GlmsProduct>()
                        };
                        newgroup.GlmsProducts.Add(product);

                        groupedinfo.Add(newgroup);
                    }
                    else
                    {
                        productgrouping.GlmsProducts.Add(product);
                    }

                }
                //sort
                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.GlmsProducts != null)
                        productgrouping.GlmsProducts = productgrouping.GlmsProducts.OrderBy(o => o.AccountNumber).ThenBy(o => o.GroupType).ToList();
                }
            }

            return new GlmsView
            {
                RiskGroup = riskGroup,
                LegalEntity = legalEntity,
                GlmsConcessions = GlmsConcessions.OrderBy(_ => _.Concession.AccountNumber),
                GlmsProductGroups = groupedinfo.OrderBy(o => o.LegalEntity)
            };
        }

        private IEnumerable<Model.UserInterface.Glms.GlmsProduct> GetGlmsProducts(RiskGroup riskGroup)
        {
            return _miscPerformanceRepository.GetGlmsProducts(riskGroup.Id, riskGroup.Name);
        }

        private IEnumerable<Model.UserInterface.Glms.GlmsProduct> GetGlmsProductsByLegalEntity(Model.UserInterface.LegalEntity legalEntity)
        {
            return _miscPerformanceRepository.GetGlmsProductsByLegalEntity(legalEntity.Id, legalEntity.CustomerName);
        }

        private ConcessionGlms MapGlms(GlmsConcessionDetail glmsConcessionDetail)
        {
            ConcessionGlms glmsConcession = new ConcessionGlms() {       
             InterestPricingCategoryId = glmsConcessionDetail.interestPricingCategoryId,
             SlabTypeId= glmsConcessionDetail.SlabTypeId,
             ConcessionDetailId= glmsConcessionDetail.ConcessionDetailId,
             GlmsGroupId= glmsConcessionDetail.GlmsGroupId,
             LegalEntityAccountId= 2022,
             ProductTypeId= 1
            };

            return glmsConcession;
        }

     
    }
}