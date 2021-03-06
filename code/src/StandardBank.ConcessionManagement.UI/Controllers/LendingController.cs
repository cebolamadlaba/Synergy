using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.LendingConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.UI.Validation;
using static StandardBank.ConcessionManagement.Model.BusinessLogic.Constants;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Lending")]
    public class LendingController : Controller
    {
        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        private readonly IBusinessCentreManager _bcmManager;

        private readonly ILookupTableManager _lookupTableManager;

        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="lendingManager"></param>
        /// <param name="siteHelper"></param>
        /// <param name="mediator"></param>
        public LendingController(ILendingManager lendingManager, ISiteHelper siteHelper, IMediator mediator, IBusinessCentreManager businessCentreManager, ILookupTableManager lookupTableManager,
            IConfigurationData configurationData)
        {
            _lendingManager = lendingManager;
            _siteHelper = siteHelper;
            _mediator = mediator;
            _bcmManager = businessCentreManager;
            _lookupTableManager = lookupTableManager;
            _configurationData = configurationData;
        }


        /// <summary>
        /// Gets the lending view data
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("LendingView/{riskGroupNumber}")]
        public IActionResult LendingView(int riskGroupNumber)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_lendingManager.GetLendingViewData(riskGroupNumber, user));
        }

        [Route("LendingViewBySAPBPID/{sapbpid}")]
        public IActionResult LendingViewBySAPBPID(int sapbpid)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_lendingManager.GetLendingViewDataBySAPBPID(sapbpid, user));
        }

        /// <summary>
        /// Saves the new lending
        /// </summary>
        /// <param name="lendingConcession"></param>
        /// <returns></returns>
        [Route("NewLending")]
        [ValidateModel]
        public async Task<IActionResult> NewLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            lendingConcession.Concession.ConcessionType = Constants.ConcessionType.Lending;
            lendingConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(lendingConcession.Concession, user));

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

            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.NewSubmission);

            if (!string.IsNullOrWhiteSpace(lendingConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, bcmPendingStatusId, lendingConcession.Concession.Comments, user));
            }

            return Ok(lendingConcession);
        }

        /// <summary>
        /// Creates a new transactional concession
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("ForwardLendingPCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardLendingPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            LendingConcession lendingConcession = _lendingManager.GetLendingConcession(detail.ReferenceNumber, user);

            lendingConcession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            lendingConcession.Concession.BcmUserId = _bcmManager.GetBusinessCentreManager(lendingConcession.Concession.CentreId).BusinessCentreManagerId;
            lendingConcession.Concession.Comments = "Manually forwarded by PCM";
            lendingConcession.Concession.IsInProgressForwarding = true;

            await _lendingManager.ForwardLendingConcession(lendingConcession, user);

            return Ok(_lendingManager.GetLendingConcession(detail.ReferenceNumber, user));
        }

        /// <summary>
        /// Updates the lending
        /// </summary>
        /// <param name="lendingConcession"></param>
        /// <returns></returns>
        [Route("UpdateLending")]
        [ValidateModel]
        public async Task<IActionResult> UpdateLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            await UpdateLendingConcession(lendingConcession, user);

            return Ok(_lendingManager.GetLendingConcession(lendingConcession.Concession.ReferenceNumber, user));
        }

        /// <summary>
        /// Updates the lending concession.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private async Task UpdateLendingConcession(LendingConcession lendingConcession, User user)
        {
            var databaseLendingConcession =
                _lendingManager.GetLendingConcession(lendingConcession.Concession.ReferenceNumber, user);

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
                if (lendingConcession.LendingConcessionDetails.All(_ => _.LendingConcessionDetailId != lendingConcessionDetail.LendingConcessionDetailId))
                {
                    await _mediator.Send(new DeleteLendingConcessionDetail(lendingConcessionDetail, user));
                }
            }

            if (!lendingConcession.Concession.AENumberUserId.HasValue)
            {
                lendingConcession.Concession.AENumberUserId = databaseLendingConcession.Concession.AENumberUserId;
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
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseLendingConcession.Concession.SubStatusId, lendingConcession.Concession.Comments, user));
            }

            if ((lendingConcession.Concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges || lendingConcession.Concession.SubStatus == Constants.ConcessionSubStatus.HoApprovedWithChanges) && lendingConcession.Concession.ConcessionComments != null)
            {
                if (lendingConcession.Concession.ConcessionComments.Count() > 0 && lendingConcession.Concession.ConcessionComments.First().UserDescription == "LogChanges")
                {
                    await _mediator.Send(new AddConcessionComment(concession.Id, databaseLendingConcession.Concession.SubStatusId, "LogChanges:" + lendingConcession.Concession.ConcessionComments.First().Comment, user));
                }
            }
        }

        /// <summary>
        /// Extends the concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("ExtendConcession/{extensionFee:decimal}")]
        public async Task<IActionResult> ExtendConcession([FromBody] ExtendConcessionModel extendConcession, decimal extensionFee)
        {
            var lendingConcession = await CreateChildConcession(extendConcession, Constants.RelationshipType.Extension, extensionFee);

            return Ok(lendingConcession);
        }

        /// <summary>
        /// Creates the child concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="relationshipType">Type of the relationship.</param>
        /// <returns></returns>
        private async Task<LendingConcession> CreateChildConcession(ExtendConcessionModel extendConcession, string relationshipType, decimal? extensionFee = null)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the lending concession details
            var lendingConcession = _lendingManager.GetLendingConcession(extendConcession.ConcessionReferenceId, user);

            var parentConcessionId = lendingConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = lendingConcession.Concession;
            newConcession.Id = 0;
            newConcession.Status = Constants.ConcessionStatus.Pending;
            newConcession.BcmUserId = null;
            newConcession.DateOpened = DateTime.Now;
            newConcession.DateSentForApproval = DateTime.Now;
            newConcession.HoUserId = null;
            newConcession.PcmUserId = null;
            newConcession.ReferenceNumber = string.Empty;
            newConcession.SubStatus = Constants.ConcessionSubStatus.BcmPending;
            newConcession.Type = Constants.ReferenceType.Existing;
            newConcession.Motivation = extendConcession.Motivation;

            var concession = await _mediator.Send(new AddConcession(newConcession, user));

            lendingConcession.Concession = concession;

            //decimal extensionFee = _lendingManager.GetExtensionFee();

            //add all the new conditions and lending details
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
            {
                lendingConcessionDetail.DateApproved = null;

                if (relationshipType != Constants.RelationshipType.Extension)
                {
                    lendingConcessionDetail.ExpiryDate = null;
                }
                else if (relationshipType == Constants.RelationshipType.Extension && lendingConcessionDetail.ProductType == Lending.ProductType.Overdraft)
                {
                    lendingConcessionDetail.ExtensionFee = extensionFee.Value;
                    if (lendingConcessionDetail.ExtensionFee > 0)
                    {
                        lendingConcessionDetail.InitiationFee = 0;
                        lendingConcessionDetail.ReviewFee = 0;
                    }
                }

                lendingConcessionDetail.LendingConcessionDetailId = 0;
                await _mediator.Send(new AddOrUpdateLendingConcessionDetail(lendingConcessionDetail, user, concession));
            }

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                {
                    concessionCondition.ConcessionConditionId = 0;
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            //link the new concession to the old concession
            var concessionRelationship = new ConcessionRelationship
            {
                CreationDate = DateTime.Now,
                UserId = user.Id,
                RelationshipDescription = relationshipType,
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };

            await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));

            var returnConcession = _lendingManager.GetLendingConcession(extendConcession.ConcessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;

            return returnConcession;
        }

        /// <summary>
        /// Gets the lending concession data for the concession reference id specified
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <returns></returns>
        [Route("LendingConcessionData/{concessionReferenceId}")]
        public IActionResult LendingConcessionData(string concessionReferenceId)
        {
            return Ok(_lendingManager.GetLendingConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        /// <summary>
        /// Renews the lending.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <returns></returns>
        [Route("RenewLending")]
        [ValidateModel]
        public async Task<IActionResult> RenewLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(lendingConcession, user, Constants.RelationshipType.Renewal);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Resubmits the lending.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <returns></returns>
        [Route("ResubmitLending")]
        [ValidateModel]
        public async Task<IActionResult> ResubmitLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(lendingConcession, user, Constants.RelationshipType.Resubmit);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Updates the approved lending.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <returns></returns>
        [Route("UpdateApprovedLending")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);
            string originalConcessionRef = lendingConcession.Concession.ReferenceNumber;

            var returnConcession = await CreateChildConcession(lendingConcession, user, Constants.RelationshipType.Update);

            //update original concession details.
            var NewlendingConcession = lendingConcession;
            UpdateLendingConcessionDetail(NewlendingConcession, user, originalConcessionRef);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Creates the child concession.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <param name="user">The user.</param>
        /// <param name="relationship">The relationship.</param>
        /// <returns></returns>
        private async Task<LendingConcession> CreateChildConcession(LendingConcession lendingConcession, User user, string relationship)
        {
            //get the parent lending concession details
            var parentLendingConcession = _lendingManager.GetLendingConcession(lendingConcession.Concession.ReferenceNumber, user);


            var parentConcessionId = parentLendingConcession.Concession.Id;

            lendingConcession.Concession.ReferenceNumber = string.Empty;
            lendingConcession.Concession.ConcessionType = Constants.ConcessionType.Lending;
            lendingConcession.Concession.Type = Constants.ReferenceType.Existing;
            var concession = new Concession();

             concession = await _mediator.Send(new AddConcession(lendingConcession.Concession, user));
          
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
            {
                if (relationship != Constants.RelationshipType.Extension)
                {
                    lendingConcessionDetail.ExpiryDate = null;
                }
                else
                {

                    if (lendingConcessionDetail.ExpiryDate != null && lendingConcessionDetail.ProductTypeId== (int?)Constants.Lending.ProductType.OverdraftId
                        && lendingConcessionDetail.ProductType == Constants.Lending.ProductType.BusinessRCP)
                    {
                        var dateExp = Convert.ToDateTime(lendingConcessionDetail.ExpiryDate);
                        lendingConcessionDetail.ExpiryDate = dateExp.AddMonths(_configurationData.MonthOfExpiry);
                    }
                }
                await _mediator.Send(new AddOrUpdateLendingConcessionDetail(lendingConcessionDetail, user, concession));
            }

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            //link the new concession to the old concession
            var concessionRelationship = new ConcessionRelationship
            {
                CreationDate = DateTime.Now,
                UserId = user.Id,
                RelationshipDescription = relationship,
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };

            await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));

            var returnConcession = _lendingManager.GetLendingConcession(parentLendingConcession.Concession.ReferenceNumber, user);

            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }

        /// <summary>
        /// Updates the recalled lending.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <returns></returns>
        [Route("UpdateRecalledLending")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
            await _mediator.Send(new ActivateConcession(lendingConcession.Concession.ReferenceNumber, user));

            //update the concession accordingly
            await UpdateLendingConcession(lendingConcession, user);

            return Ok(_lendingManager.GetLendingConcession(lendingConcession.Concession.ReferenceNumber, user));
        }

        /// <summary>
        /// Latests the CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("LatestCrsOrMrs/{riskGroupNumber}")]
        public IActionResult LatestCrsOrMrs(int riskGroupNumber)
        {
            return Ok(_lendingManager.GetLatestCrsOrMrs(riskGroupNumber));
        }

        /// <summary>
        /// Lendings the financial.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("LendingFinancial/{riskGroupNumber}")]
        public IActionResult LendingFinancial(int riskGroupNumber)
        {
            return Ok(_lendingManager.GetLendingFinancialForRiskGroupNumber(riskGroupNumber));
        }

        /// <summary>
        /// Updates the lending concession.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private void UpdateLendingConcessionDetail(LendingConcession lendingConcession, User user,string originalConcessionRef)
        {
            //get the parent lending concession details
            var parentLendingConcession = _lendingManager.GetLendingConcession(originalConcessionRef, user);

            //get concession lending
            var concessionLending = _lendingManager.GetConcessionLendingByConcessionId(parentLendingConcession.Concession.Id).ToList();

            int lendingDetailCount = 0;

            //update original concession after creating a child concession
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
            {
                lendingConcessionDetail.LendingConcessionDetailId = concessionLending[lendingDetailCount].Id;
                _lendingManager.UpdateMarginToPrime(lendingConcessionDetail.LendingConcessionDetailId,lendingConcessionDetail.MarginAgainstPrime, lendingConcessionDetail.MarginAgainstPrime);
                lendingDetailCount++;
            }

        }

    }
}

