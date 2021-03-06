using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using GlmsTierData = StandardBank.ConcessionManagement.Model.UserInterface.GlmsTierData;
using GlmsTierDataView = StandardBank.ConcessionManagement.Model.UserInterface.GlmsTierDataView;
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

        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        private readonly IMediator _mediator;

        private readonly IGlmsTierDataRepository _glmsTierDataRepository;

        private readonly IRuleManager _ruleManager;

        public GlmsManager(IConcessionManager concessionManager, IConcessionGlmsRepository concessionGlmsRepository,
            IMapper mapper, ILookupTableManager lookupTableManager, IMiscPerformanceRepository miscPerformanceRepository,
            IMediator mediator, IGlmsTierDataRepository glmsTierDataRepository, IRuleManager ruleManager)
        {
            _concessionManager = concessionManager;
            _concessionGlmsRepository = concessionGlmsRepository;
            _mapper = mapper;
            _lookupTableManager = lookupTableManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _mediator = mediator;
            _glmsTierDataRepository = glmsTierDataRepository;
            _ruleManager = ruleManager;
        }

        public ConcessionGlms CreateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession)
        {
            var concessionGlms = MapGlms(glmsConcessionDetail);
            concessionGlms.ConcessionId = concession.Id;
            return _concessionGlmsRepository.Create(concessionGlms);
        }

        public ConcessionGlms UpdateConcessionGlms(GlmsConcessionDetail glmsConcessionDetail, Concession concession, int? archiveType = null)
        {
            var concessionGlms = MapGlms(glmsConcessionDetail);

            concessionGlms.ConcessionId = concession.Id;

            if (archiveType.HasValue)
            {
                concessionGlms.ArchiveTypeId = archiveType;
                concessionGlms.Archived = DateTime.Now;
            }


            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                _ruleManager.UpdateBaseFieldsOnApproval(concessionGlms);
            }

            _concessionGlmsRepository.Update(concessionGlms);

            return concessionGlms;
        }

        public GlmsConcession GetGlmsConcession(string concessionReferenceId, User user)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId, user);
            var glmsConcessionDetails = _miscPerformanceRepository.GetGlmsConcessionDetails(concession.Id);

            foreach (var glmsConcessionDetail in glmsConcessionDetails)
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

            DeleteGlmsTierData(concessionGlms.Id);
            _concessionGlmsRepository.Delete(concessionGlms);
            return concessionGlms;
        }

        public GlmsView GetGlmsViewData(int riskGroupNumber, int sapbpid, User currentUser)
        {
            var GlmsConcessions = new List<GlmsConcession>();
            RiskGroup riskGroup = null;
            Model.UserInterface.LegalEntity legalEntity = null;
            IEnumerable<Concession> concessions = null;
            IEnumerable<Model.UserInterface.Glms.GlmsProduct> GlmsProducts = null;
            if (riskGroupNumber > 0)
            {
                riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
                concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Glms, currentUser);
                GlmsProducts = GetGlmsProducts(riskGroup);
            }
            if (sapbpid > 0)
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

            GlmsConcessions.ForEach(x =>
            {
                foreach (var detail in x.GlmsConcessionDetails)
                {
                    detail.GlmsTierDataView = _mapper.Map<IEnumerable<GlmsTierDataView>>(_glmsTierDataRepository.GetGlmsTierDataViewById(detail.GlmsConcessionDetailId));
                }
            });

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

                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.GlmsProducts != null)
                    {
                        productgrouping.GlmsProducts = productgrouping.GlmsProducts.OrderBy(o => o.GroupNumber).ThenBy(o => o.GroupType).ToList();
                    }
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
            ConcessionGlms glmsConcession = new ConcessionGlms()
            {
                InterestPricingCategoryId = glmsConcessionDetail.InterestPricingCategoryId,
                SlabTypeId = glmsConcessionDetail.SlabTypeId,
                ConcessionDetailId = glmsConcessionDetail.ConcessionDetailId,
                GlmsGroupId = glmsConcessionDetail.GlmsGroupId,
                InterestTypeId = glmsConcessionDetail.InterestTypeId,
                ExpiryDate = glmsConcessionDetail.ExpiryDate,
                DateApproved = glmsConcessionDetail.DateApproved
            };

            return glmsConcession;
        }

        public void AddGlmsTierData(IEnumerable<GlmsTierData> tierData, int concessionDetailId)
        {
            tierData.ToList().ForEach(x =>
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
            {
                if (glmsConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                {
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));
                }
            }

            //if there are any lending concession details that have been removed delete them
            foreach (var glmsConcessionDetail in databaseGlmsConcession.GlmsConcessionDetails)
            {
                if (glmsConcession.GlmsConcessionDetails.All(_ => _.GlmsConcessionDetailId != glmsConcessionDetail.GlmsConcessionDetailId))
                {
                    await _mediator.Send(new DeleteGlmsConcessionDetail(glmsConcessionDetail, user));
                }
            }

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(glmsConcession.Concession, user));

            //add all the new conditions and lending details and comments
            foreach (var glmsConcessionDetail in glmsConcession.GlmsConcessionDetails)
            {
                await _mediator.Send(new AddOrUpdateGlmsConcessionDetail(glmsConcessionDetail, user, concession));
            }

            if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in glmsConcession.ConcessionConditions)
                {
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            if (!string.IsNullOrWhiteSpace(glmsConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseGlmsConcession.Concession.SubStatusId,
                    glmsConcession.Concession.Comments, user));
            }

            //send the notification email
            await _mediator.Send(new ForwardConcession(glmsConcession.Concession, user));
        }
    }
}