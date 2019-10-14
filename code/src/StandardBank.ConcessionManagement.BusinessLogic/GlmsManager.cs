using System;
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
using GlmsTierData = StandardBank.ConcessionManagement.Model.UserInterface.GlmsTierData;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;

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

        private readonly IGlmsTierDataRepository _glmsTierDataRepository;
        private GlmsConcessionDetail glmsConcessionDetail;

        public GlmsManager(IConcessionManager concessionManager, IConcessionGlmsRepository concessionGlmsRepository,
            IMapper mapper,ILookupTableManager lookupTableManager, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IMediator mediator, IGlmsTierDataRepository glmsTierDataRepository)
        {
            _concessionManager = concessionManager;
            _concessionGlmsRepository = concessionGlmsRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;
            _glmsTierDataRepository = glmsTierDataRepository;
        }

        public ConcessionGlms CreateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession)
        {
             var concessionGlms = MapGlms(glmsConcessionDetail);
             concessionGlms.ConcessionId = concession.Id;
             return _concessionGlmsRepository.Create(concessionGlms);
        }

        public ConcessionGlms UpdateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession)
        {
             var concessionGlms = MapGlms(glmsConcessionDetail);
             concessionGlms.ConcessionId = concession.Id;
             _concessionGlmsRepository.Update(concessionGlms);

            return concessionGlms;
        }

        public GlmsConcession GetGlmsConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId, user);
            var glmsConcessionDetails = _miscPerformanceRepository.GetGlmsConcessionDetails(concession.Id);

            foreach(var glmsConcessionDetail in  glmsConcessionDetails)
            {
                
                glmsConcessionDetail.GlmsTierData = _mapper.Map<IEnumerable<GlmsTierData>>(_glmsTierDataRepository.ReadAllById(glmsConcessionDetail.GlmsConcessionDetailId));
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
                 InterestTypeId=glmsConcessionDetail.InterestTypeId,
                 LegalEntityAccountId= glmsConcessionDetail.LegalEntityAccountId,
                 LegalEntityId= glmsConcessionDetail.LegalEntityId,
                 ExpiryDate= glmsConcessionDetail.ExpiryDate,
                 ProductTypeId = 1
            };

            return glmsConcession;
        }

        public void AddGlmsTierData(IEnumerable<GlmsTierData> tierData, int concessionDetailId)
        {
            tierData.ToList().ForEach( x=> 
            {
                x.GlmsConcessionId = concessionDetailId;
                _glmsTierDataRepository.Create(_mapper.Map<Model.Repository.GlmsTierData>(x));

            });        
        }

        public void DeleteGlmsTierData(int concessionDetailId)
        {
           _glmsTierDataRepository.Delete(concessionDetailId);
        }

        public async Task ForwardGlmsConcession(GlmsConcession glmsConcession, User user)
        {
            var databaseGlmsConcession =
               this.GetGlmsConcession(glmsConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseGlmsConcession.ConcessionConditions)
                if (glmsConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any lending concession details that have been removed delete them
            foreach (var glmsConcessionDetail in databaseGlmsConcession.GlmsConcessionDetails)
                if (glmsConcession.GlmsConcessionDetails.All(_ => _.GlmsConcessionDetailId !=
                                                                        glmsConcessionDetail
                                                                            .GlmsConcessionDetailId))
                    await _mediator.Send(new DeleteGlmsConcessionDetail(glmsConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(glmsConcession.Concession, user));

            //add all the new conditions and lending details and comments
            foreach (var glmsConcessionDetail in glmsConcession.GlmsConcessionDetails)
                await _mediator.Send(new AddOrUpdateGlmsConcessionDetail(glmsConcessionDetail, user, concession));

            if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in glmsConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(glmsConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseGlmsConcession.Concession.SubStatusId,
                    glmsConcession.Concession.Comments, user));

            //send the notification email
            await _mediator.Send(new ForwardConcession(glmsConcession.Concession, user));

        }

    }
}