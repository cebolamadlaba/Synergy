using AutoMapper;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.LendingConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Lending manager
    /// </summary>
    /// <seealso cref="ILendingManager" />
    public class LendingManager : ILendingManager
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The concession lending repository
        /// </summary>
        private readonly IConcessionLendingRepository _concessionLendingRepository;

        private readonly IConcessionLendingTieredRateRepository _concessionLendingTieredRateRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The financial lending repository
        /// </summary>
        private readonly IFinancialLendingRepository _financialLendingRepository;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The loaded price lending repository
        /// </summary>
        private readonly ILoadedPriceLendingRepository _loadedPriceLendingRepository;

        /// <summary>
        /// The rule manager
        /// </summary>
        private readonly IRuleManager _ruleManager;

        private readonly IMediator _mediator;

        /// <summary>
        /// The misc performance repository
        /// </summary>
        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        private readonly IPrimeRateRepository _primeRateRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendingManager"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="concessionLendingRepository">The concession lending repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="financialLendingRepository">The financial lending repository.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="loadedPriceLendingRepository">The loaded price lending repository.</param>
        /// <param name="ruleManager">The rule manager.</param>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        public LendingManager(IConcessionManager concessionManager,
            IConcessionLendingRepository concessionLendingRepository, IMapper mapper,
            IFinancialLendingRepository financialLendingRepository, ILookupTableManager lookupTableManager,
            ILoadedPriceLendingRepository loadedPriceLendingRepository, IRuleManager ruleManager,
            IMiscPerformanceRepository miscPerformanceRepository, IPrimeRateRepository primeRateRepository,
            IMediator mediator, IConcessionLendingTieredRateRepository concessionLendingTieredRateRepository)
        {
            _concessionManager = concessionManager;
            _concessionLendingRepository = concessionLendingRepository;
            _mapper = mapper;
            _financialLendingRepository = financialLendingRepository;
            _lookupTableManager = lookupTableManager;
            _loadedPriceLendingRepository = loadedPriceLendingRepository;
            _ruleManager = ruleManager;
            _miscPerformanceRepository = miscPerformanceRepository;
            _primeRateRepository = primeRateRepository;
            _mediator = mediator;
            _concessionLendingTieredRateRepository = concessionLendingTieredRateRepository;
        }

        /// <summary>
        /// Creates a concession lending
        /// </summary>
        /// <param name="lendingConcessionDetail"></param>
        /// <param name="concession"></param>
        /// <returns></returns>
        public ConcessionLending CreateConcessionLending(LendingConcessionDetail lendingConcessionDetail,
            Concession concession)
        {
            var concessionLending = _mapper.Map<ConcessionLending>(lendingConcessionDetail);

            concessionLending.ConcessionId = concession.Id;

            return _concessionLendingRepository.Create(concessionLending);
        }

        /// <summary>
        /// Gets the lending concession for the concession reference id specified
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public LendingConcession GetLendingConcession(string concessionReferenceId, User currentUser)
        {
            var concession = _concessionManager.GetConcessionForConcessionReferenceId(concessionReferenceId, currentUser);
            var lendingConcessionDetails = _miscPerformanceRepository.GetLendingConcessionDetails(concession.Id);

            this.GetRelatedLendingConcessionTieredRates(lendingConcessionDetails);

            var primerate = _primeRateRepository.PrimeRate(concession.DateOpened);

            //we are only allowed to extend or renew overdraft products
            if (concession.CanExtend || concession.CanRenew)
            {
                if (!lendingConcessionDetails.Any(_ => _.ProductType == Constants.Lending.ProductType.Overdraft || _.ProductType == Constants.Lending.ProductType.TempOverdraft || _.ProductType.StartsWith("VAF")))
                {
                    concession.CanExtend = false;
                    concession.CanRenew = false;

                    //if we can't extend or renew but it is approved, that means we can update it
                    if (concession.Status == Constants.ConcessionStatus.Approved ||
                        concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
                        concession.CanUpdate = true;
                }
            }

            return new LendingConcession
            {
                Concession = concession,
                LendingConcessionDetails = lendingConcessionDetails,
                ConcessionConditions = _concessionManager.GetConcessionConditions(concession.Id),
                CurrentUser = currentUser,
                PrimeRate = primerate
            };
        }

        /// <summary>
        /// Deletes the concession lending.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <returns></returns>
        public ConcessionLending DeleteConcessionLending(LendingConcessionDetail lendingConcessionDetail)
        {
            if (lendingConcessionDetail.LendingConcessionDetailTieredRates != null && lendingConcessionDetail.LendingConcessionDetailTieredRates.Count() > 0)
            {
                var concessionLendingTieredRates = this._mapper.Map<IEnumerable<ConcessionLendingTieredRate>>(lendingConcessionDetail.LendingConcessionDetailTieredRates);
                this.DeleteConcessionLendingTieredRates(concessionLendingTieredRates);
            }

            var concessionLending =
                _concessionLendingRepository.ReadById(lendingConcessionDetail.LendingConcessionDetailId);

            _concessionLendingRepository.Delete(concessionLending);

            return concessionLending;
        }

        /// <summary>
        /// Updates the concession lending.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <param name="concession">The concession.</param>
        /// <returns></returns>
        public ConcessionLending UpdateConcessionLending(LendingConcessionDetail lendingConcessionDetail,
            Concession concession)
        {
            this.UpdateConcessionLendingTieredRates(lendingConcessionDetail);

            var concessionLending = _mapper.Map<ConcessionLending>(lendingConcessionDetail);

            concessionLending.ConcessionId = concession.Id;

            if (concession.Status == Constants.ConcessionStatus.Approved ||
                concession.Status == Constants.ConcessionStatus.ApprovedWithChanges)
            {
                UpdateApprovedPrice(concessionLending);
                UpdateIsMismatched(concessionLending);

                _ruleManager.UpdateBaseFieldsOnApproval(concessionLending);

                if (!concessionLending.ExpiryDate.HasValue)
                {
                    var productType = _lookupTableManager.GetProductTypeName(concessionLending.ProductTypeId);

                    if (productType == Constants.Lending.ProductType.Overdraft)
                    {
                        concessionLending.ExpiryDate = DateTime.Now.AddMonths(concessionLending.Term.Value);
                    }
                    else if (productType == Constants.Lending.ProductType.TempOverdraft)
                    {
                        concessionLending.ExpiryDate = DateTime.Now.AddMonths(concessionLending.Term.Value);
                    }
                    else if (productType != Constants.Lending.ProductType.Overdraft && concessionLending.Term.HasValue)
                    {
                        concessionLending.ExpiryDate = DateTime.Now.AddMonths(concessionLending.Term.Value);
                    }
                }
            }
            else if (concession.Status == Constants.ConcessionStatus.Pending &&
                     concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges)
            {
                UpdateApprovedPrice(concessionLending);
            }

            _concessionLendingRepository.Update(concessionLending);

            return concessionLending;
        }

        /// <summary>
        /// Updates the approved price.
        /// </summary>
        /// <param name="concessionLending">The concession lending.</param>
        private void UpdateApprovedPrice(ConcessionLending concessionLending)
        {
            var databaseLendingConcession =
                _concessionLendingRepository.ReadById(concessionLending.Id);

            if (databaseLendingConcession.ApprovedMarginToPrime.HasValue)
            {
                concessionLending.ApprovedMarginToPrime = databaseLendingConcession.ApprovedMarginToPrime;
            }
            else
            {
                //the approved margin to prime is what has been captured when approved
                concessionLending.ApprovedMarginToPrime = concessionLending.MarginToPrime;

                //the margin to prime is what is in the database at the moment
                concessionLending.MarginToPrime = databaseLendingConcession.MarginToPrime;
            }
        }

        /// <summary>
        /// Updates the is mismatched.
        /// </summary>
        /// <param name="concessionLending">The concession lending.</param>
        private void UpdateIsMismatched(ConcessionLending concessionLending)
        {
            concessionLending.IsMismatched = false;

            // Bernard Cronje - 2018-11-26 - LegalEntityAccountId is now nullable as per client request.
            if (!concessionLending.LegalEntityAccountId.HasValue)
            {
                concessionLending.IsMismatched = true;
                return;
            }

            var loadedPriceLending =
                _loadedPriceLendingRepository.ReadByProductTypeIdLegalEntityAccountId(
                    concessionLending.ProductTypeId, concessionLending.LegalEntityAccountId.Value);

            if (loadedPriceLending != null)
            {
                concessionLending.LoadedMarginToPrime = loadedPriceLending.MarginToPrime;

                if (loadedPriceLending.MarginToPrime != concessionLending.ApprovedMarginToPrime)
                    concessionLending.IsMismatched = true;
            }
            else
            {
                concessionLending.IsMismatched = true;
            }
        }

        /// <summary>
        /// Gets the lending view data.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public LendingView GetLendingViewData(int riskGroupNumber, User currentUser)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
            var lendingConcessions = new List<LendingConcession>();
            var concessions = _concessionManager.GetApprovedConcessionsForRiskGroup(riskGroup.Id, Constants.ConcessionType.Lending, currentUser);

            IEnumerable<LendingConcessionDetail> lendingConcessionDetail = null;
            foreach (var concession in concessions)
            {
                lendingConcessionDetail = _miscPerformanceRepository.GetLendingConcessionDetails(concession.Id);
                this.GetRelatedLendingConcessionTieredRates(lendingConcessionDetail);
                lendingConcessions.Add(new LendingConcession
                {
                    Concession = concession,
                    LendingConcessionDetails = lendingConcessionDetail
                });
            }

            var lendingProducts = GetLendingProducts(riskGroup.Id, riskGroup.Name);

            var lendingFinancial = _mapper.Map<LendingFinancial>(
                _financialLendingRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialLending());

            //grouping of products
            var groupedinfo = new List<LendingProductGroup>();
            if (lendingProducts != null)
            {
                foreach (var product in lendingProducts)
                {
                    var productgrouping = groupedinfo.Where(g => g.CustomerName == product.CustomerName).FirstOrDefault();
                    if (productgrouping == null)
                    {
                        LendingProductGroup newgroup = new LendingProductGroup
                        {
                            CustomerName = product.CustomerName,
                            RiskGroupName = product.RiskGroupName,
                            LendingProducts = new List<LendingProduct>()
                        };
                        newgroup.LendingProducts.Add(product);

                        groupedinfo.Add(newgroup);
                    }
                    else
                    {
                        productgrouping.LendingProducts.Add(product);
                    }
                }

                //sort
                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.LendingProducts != null)
                    {
                        productgrouping.LendingProducts = productgrouping.LendingProducts.OrderBy(o => o.AccountNumber).ThenBy(o => o.Product).ToList();
                    }
                }
            }

            return new LendingView
            {
                RiskGroup = riskGroup,
                LendingConcessions = lendingConcessions.OrderBy(_ => _.Concession.AccountNumber),
                LendingProductGroups = groupedinfo.OrderBy(m => m.CustomerName),
                LendingFinancial = lendingFinancial
            };
        }

        /// <summary>
        /// Only to be used for SAPBPIDs which are not linked to RiskGroups
        /// </summary>
        /// <param name="sapbpid"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public LendingView GetLendingViewDataBySAPBPID(int sapbpid, User currentUser)
        {
            var legalEntity = _lookupTableManager.GetLegalEntity(sapbpid);
            var lendingConcessions = new List<LendingConcession>();
            var concessions = _concessionManager.GetApprovedConcessionsForLegalEntityId(legalEntity.Id, Constants.ConcessionType.Lending, currentUser);

            IEnumerable<LendingConcessionDetail> lendingConcessionDetail = null;
            foreach (var concession in concessions)
            {
                lendingConcessionDetail = _miscPerformanceRepository.GetLendingConcessionDetails(concession.Id);
                this.GetRelatedLendingConcessionTieredRates(lendingConcessionDetail);
                lendingConcessions.Add(new LendingConcession
                {
                    Concession = concession,
                    LendingConcessionDetails = lendingConcessionDetail
                });
            }

            var lendingProducts = GetLendingProductsByLegalEntityId(legalEntity.Id, legalEntity.CustomerName);

            // tblFinancialLending does not carry fkLegalEntityId...
            LendingFinancial lendingFinancial = new LendingFinancial()
            {
                LatestCrsOrMrs = 0,
                TotalExposure = 0,
                WeightedAverageMap = 0,
                WeightedCrsOrMrs = 0
            };

            //grouping of products
            var groupedinfo = new List<LendingProductGroup>();
            if (lendingProducts != null)
            {
                foreach (var product in lendingProducts)
                {
                    var productgrouping = groupedinfo.Where(g => g.CustomerName == product.CustomerName).FirstOrDefault();
                    if (productgrouping == null)
                    {
                        LendingProductGroup newgroup = new LendingProductGroup
                        {
                            CustomerName = product.CustomerName,
                            RiskGroupName = product.RiskGroupName,
                            LendingProducts = new List<LendingProduct>()
                        };
                        newgroup.LendingProducts.Add(product);

                        groupedinfo.Add(newgroup);
                    }
                    else
                    {
                        productgrouping.LendingProducts.Add(product);
                    }
                }
                //sort
                foreach (var productgrouping in groupedinfo)
                {
                    if (productgrouping != null && productgrouping.LendingProducts != null)
                    {
                        productgrouping.LendingProducts = productgrouping.LendingProducts.OrderBy(o => o.AccountNumber).ThenBy(o => o.Product).ToList();
                    }
                }
            }

            return new LendingView()
            {
                LendingConcessions = lendingConcessions.OrderBy(_ => _.Concession.AccountNumber),
                LendingFinancial = lendingFinancial,
                LendingProductGroups = groupedinfo.OrderBy(m => m.CustomerName),
                LegalEntity = legalEntity
            };
        }

        /// <summary>
        /// Gets the latest CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public decimal GetLatestCrsOrMrs(int riskGroupNumber)
        {
            return GetLendingFinancial(riskGroupNumber).LatestCrsOrMrs;
        }

        /// <summary>
        /// Gets the lending financial for risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        public LendingFinancial GetLendingFinancialForRiskGroupNumber(int riskGroupNumber)
        {
            return GetLendingFinancial(riskGroupNumber);
        }

        private LendingFinancial GetLendingFinancial(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);

            return _mapper.Map<LendingFinancial>(
                _financialLendingRepository.ReadByRiskGroupId(riskGroup.Id).FirstOrDefault() ?? new FinancialLending());
        }

        /// <summary>
        /// Gets the lending products.
        /// </summary>
        /// <param name="riskGroupId">The risk group identifier.</param>
        /// <param name="riskGroupName">Name of the risk group.</param>
        /// <returns></returns>
        private IEnumerable<LendingProduct> GetLendingProducts(int riskGroupId, string riskGroupName)
        {
            return _miscPerformanceRepository.GetLendingProducts(riskGroupId, riskGroupName);
        }

        private IEnumerable<LendingProduct> GetLendingProductsByLegalEntityId(int legalEntityId, string legalEntityName)
        {
            return _miscPerformanceRepository.GetLendingProductsByLegalEntity(legalEntityId, legalEntityName);
        }

        public async Task ForwardLendingConcession(LendingConcession lendingConcession, User user)
        {
            var databaseLendingConcession =
               this.GetLendingConcession(lendingConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseLendingConcession.ConcessionConditions)
            {
                if (lendingConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                {
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));
                }
            }

            //if there are any lending concession details that have been removed delete them
            foreach (var lendingConcessionDetail in databaseLendingConcession.LendingConcessionDetails)
            {
                if (lendingConcession.LendingConcessionDetails.All(_ => _.LendingConcessionDetailId !=
                                                                                        lendingConcessionDetail.LendingConcessionDetailId))
                {
                    await _mediator.Send(new DeleteLendingConcessionDetail(lendingConcessionDetail, user));
                }
            }

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(lendingConcession.Concession, user));

            //add all the new conditions and lending details and comments
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
            {
                await _mediator.Send(new AddOrUpdateLendingConcessionDetail(lendingConcessionDetail, user, concession));
            }

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                {
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            if (!string.IsNullOrWhiteSpace(lendingConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseLendingConcession.Concession.SubStatusId,
                                    lendingConcession.Concession.Comments, user));
            }

            //send the notification email
            await _mediator.Send(new ForwardConcession(lendingConcession.Concession, user));
        }

        #region Concession Lending Tiered Rate
        private IEnumerable<LendingConcessionDetailTieredRate> GetLendingConcessionTieredRates(int concessionLendingId)
        {
            var concessionLendingTieredRates = this._concessionLendingTieredRateRepository.ReadByConcessionLendingId(concessionLendingId);
            return this._mapper.Map<IEnumerable<LendingConcessionDetailTieredRate>>(concessionLendingTieredRates);
        }

        private IEnumerable<LendingConcessionDetail> GetRelatedLendingConcessionTieredRates(IEnumerable<LendingConcessionDetail> lendingConcessionDetails)
        {
            foreach (var lendingConcessionDetail in lendingConcessionDetails)
                lendingConcessionDetail.LendingConcessionDetailTieredRates = this.GetLendingConcessionTieredRates(lendingConcessionDetail.LendingConcessionDetailId);

            return lendingConcessionDetails;
        }

        public void CreateConcessionLendingTieredRates(IEnumerable<LendingConcessionDetailTieredRate> lendingConcessionDetailTieredRates)
        {
            if (lendingConcessionDetailTieredRates == null || lendingConcessionDetailTieredRates.Count() == 0)
                return;

            var concessionLendingTieredRates = this._mapper.Map<IEnumerable<ConcessionLendingTieredRate>>(lendingConcessionDetailTieredRates);
            foreach (var concessionLendingTieredRate in concessionLendingTieredRates)
            {
                this._concessionLendingTieredRateRepository.Create(concessionLendingTieredRate);
            }
        }

        public void CreateConcessionLendingTieredRates(IEnumerable<LendingConcessionDetail> lendingConcessionDetails)
        {
            if (lendingConcessionDetails == null || lendingConcessionDetails.Count() == 0)
                return;

            foreach (var lendingConcessionDetail in lendingConcessionDetails)
            {
                this.CreateConcessionLendingTieredRates(lendingConcessionDetail.LendingConcessionDetailTieredRates);
            }
        }

        public void UpdateConcessionLendingTieredRates(LendingConcessionDetail lendingConcessionDetail)
        {
            if (lendingConcessionDetail == null)
                return;
            if (lendingConcessionDetail.LendingConcessionDetailTieredRates == null || lendingConcessionDetail.LendingConcessionDetailTieredRates.Count() == 0)
                return;

            var concessionLendingTieredRates = this._mapper.Map<IEnumerable<ConcessionLendingTieredRate>>(lendingConcessionDetail.LendingConcessionDetailTieredRates);

            // remove tiered rates no longer present.
            var dbLendingConcessionTieredRates = this.GetLendingConcessionTieredRates(lendingConcessionDetail.LendingConcessionDetailId);
            foreach (var dbLendingConcessionTieredRate in dbLendingConcessionTieredRates)
            {
                if (!concessionLendingTieredRates.Any(a => a.Id == dbLendingConcessionTieredRate.Id))
                    this.DeleteConcessionLendingTieredRate(dbLendingConcessionTieredRate.Id);
            }

            foreach (var concessionLendingTieredRate in concessionLendingTieredRates)
            {
                if (concessionLendingTieredRate.Id == 0)
                    this._concessionLendingTieredRateRepository.Create(concessionLendingTieredRate);
                else
                    this._concessionLendingTieredRateRepository.Update(concessionLendingTieredRate);
            }
        }

        public void UpdateConcessionLendingTieredRates(IEnumerable<LendingConcessionDetail> lendingConcessionDetails)
        {
            if (lendingConcessionDetails == null || lendingConcessionDetails.Count() == 0)
                return;

            foreach (var lendingConcessionDetail in lendingConcessionDetails)
            {
                this.UpdateConcessionLendingTieredRates(lendingConcessionDetail);
            }
        }

        public void DeleteConcessionLendingTieredRate(int concessionLendingTieredRateId)
        {
            this._concessionLendingTieredRateRepository.Delete(concessionLendingTieredRateId);
        }

        public void DeleteConcessionLendingTieredRates(IEnumerable<ConcessionLendingTieredRate> concessionLendingTieredRates)
        {
            foreach (var concessionLendingTieredRate in concessionLendingTieredRates)
                this.DeleteConcessionLendingTieredRate(concessionLendingTieredRate.Id);
        }
        #endregion
    }
}