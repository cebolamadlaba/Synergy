using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
 
    [Produces("application/json")]
    [Route("api/Investment")]
    public class InvestmentController : Controller    {
      
        private readonly ISiteHelper _siteHelper;
      
        private readonly IInvestmentManager _investmentManager;
      
        private readonly IMediator _mediator;

        private readonly IBusinessCentreManager _bcmManager;

        private readonly ILookupTableManager _lookupTableManager;

        public InvestmentController(ISiteHelper siteHelper, IInvestmentManager investmentManager, IMediator mediator,  IBusinessCentreManager businessCentreManager, ILookupTableManager lookupTableManager)
        {
            _siteHelper = siteHelper;
            _investmentManager = investmentManager;
            _mediator = mediator;
            _bcmManager = businessCentreManager;
            _lookupTableManager = lookupTableManager;
        }
     
        /// <returns></returns>
        [Route("InvestmentView/{riskGroupNumber}")]
        public IActionResult InvestmentView(int riskGroupNumber)
        {
            return Ok(_investmentManager.GetInvestmentViewData(riskGroupNumber));
        }


        [Route("NewInvestment")]
        [ValidateModel]
        public async Task<IActionResult> NewInvestment([FromBody] InvestmentConcession investmentConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            investmentConcession.Concession.ConcessionType = Constants.ConcessionType.Investment;
            investmentConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(investmentConcession.Concession, user));

            foreach (var investmentConcessionDetail in investmentConcession.InvestmentConcessionDetails)
                await _mediator.Send(new BusinessLogic.Features.InvestmentConcession.AddOrUpdateInvestmentConcessionDetail(investmentConcessionDetail, user, concession));

            if (investmentConcession.ConcessionConditions != null && investmentConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in investmentConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));


            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.NewSubmission);

            if (!string.IsNullOrWhiteSpace(investmentConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, bcmPendingStatusId,
                    investmentConcession.Concession.Comments, user));

            return Ok(investmentConcession);
        }

        [Route("UpdateInvestment")]
        [ValidateModel]
        public async Task<IActionResult> UpdateInvestment([FromBody] InvestmentConcession investmentConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            await UpdateInvestmentConcession(investmentConcession, user);
       
            return Ok(_investmentManager.GetInvestmentConcession(investmentConcession.Concession.ReferenceNumber, user));
        }

        [Route("InvestmentConcessionData/{concessionReferenceId}")]
        public IActionResult InvestmentConcessionData(string concessionReferenceId)
        {
            return Ok(_investmentManager.GetInvestmentConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        private async Task UpdateInvestmentConcession(InvestmentConcession investmentConcession, User user)
        {
            var databaseInvestmentConcession =
                _investmentManager.GetInvestmentConcession(investmentConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseInvestmentConcession.ConcessionConditions)
                if (investmentConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any concession details that have been removed delete them
            foreach (var investmentConcessionDetail in databaseInvestmentConcession.InvestmentConcessionDetails)
                if (investmentConcession.InvestmentConcessionDetails.All(_ => _.InvestmentConcessionDetailId !=
                                                                  investmentConcessionDetail.InvestmentConcessionDetailId))
                    await _mediator.Send(new DeleteInvestmentConcessionDetail(investmentConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(investmentConcession.Concession, user));

            //add all the new conditions and details and comments
            foreach (var investmentConcessionDetail in investmentConcession.InvestmentConcessionDetails)
                await _mediator.Send(new AddOrUpdateInvestmentConcessionDetail(investmentConcessionDetail, user, concession));

            if (investmentConcession.ConcessionConditions != null && investmentConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in investmentConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(investmentConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseInvestmentConcession.Concession.SubStatusId,
                    investmentConcession.Concession.Comments, user));
                             
            }

            if ((investmentConcession.Concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges || investmentConcession.Concession.SubStatus == Constants.ConcessionSubStatus.HoApprovedWithChanges) && investmentConcession.Concession.ConcessionComments != null)
            {
                if (investmentConcession.Concession.ConcessionComments.Count() > 0 && investmentConcession.Concession.ConcessionComments.First().UserDescription == "LogChanges")
                {
                    await _mediator.Send(new AddConcessionComment(concession.Id, databaseInvestmentConcession.Concession.SubStatusId, "LogChanges:" + investmentConcession.Concession.ConcessionComments.First().Comment, user));

                }
            }

        }


        [Route("UpdateRecalledInvestment")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledInvestment([FromBody] InvestmentConcession investmentConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
            await _mediator.Send(new ActivateConcession(investmentConcession.Concession.ReferenceNumber, user));

            //update the concession accordingly
            await UpdateInvestmentConcession(investmentConcession, user);

            return Ok(_investmentManager.GetInvestmentConcession(investmentConcession.Concession.ReferenceNumber, user));
        }

        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the concession details
            var investmentConcession =
                _investmentManager.GetInvestmentConcession(concessionReferenceId, user);

            var parentConcessionId = investmentConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = investmentConcession.Concession;
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

            var concession = await _mediator.Send(new AddConcession(newConcession, user));

            investmentConcession.Concession = concession;

            //add all the new conditions and details
            foreach (var investmentConcessionDetail in investmentConcession.InvestmentConcessionDetails)
            {
                investmentConcessionDetail.DateApproved = null;
                investmentConcessionDetail.InvestmentConcessionDetailId = 0;
                await _mediator.Send(new AddOrUpdateInvestmentConcessionDetail(investmentConcessionDetail, user, concession));
            }

            if (investmentConcession.ConcessionConditions != null && investmentConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in investmentConcession.ConcessionConditions)
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
                RelationshipDescription = Constants.RelationshipType.Extension,
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };

            await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));

            var returnConcession = _investmentManager.GetInvestmentConcession(concessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return Ok(returnConcession);
        }

        [Route("RenewInvestment")]
        [ValidateModel]
        public async Task<IActionResult> RenewBol([FromBody] InvestmentConcession investmentConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(investmentConcession, user, Constants.RelationshipType.Renewal);

            return Ok(returnConcession);
        }


        [Route("ResubmitInvestment")]
        [ValidateModel]
        public async Task<IActionResult> ResubmitInvestment([FromBody] InvestmentConcession investmentConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(investmentConcession, user, Constants.RelationshipType.Resubmit);

            return Ok(returnConcession);
        }


        [Route("UpdateApprovedInvestment")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedInvestment([FromBody] InvestmentConcession investmentConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(investmentConcession, user, Constants.RelationshipType.Update);

            return Ok(returnConcession);
        }

        private async Task<InvestmentConcession> CreateChildConcession(InvestmentConcession investmentConcession, User user, string relationship)
        {
            //get the parent investment concession details
            var parentInvestmentConcession = _investmentManager.GetInvestmentConcession(investmentConcession.Concession.ReferenceNumber, user);

            var parentConcessionId = parentInvestmentConcession.Concession.Id;

            investmentConcession.Concession.ReferenceNumber = string.Empty;
            investmentConcession.Concession.ConcessionType = Constants.ConcessionType.Investment;
            investmentConcession.Concession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(investmentConcession.Concession, user));

            foreach (var investmentConcessionDetail in investmentConcession.InvestmentConcessionDetails)
                await _mediator.Send(new AddOrUpdateInvestmentConcessionDetail(investmentConcessionDetail, user, concession));

            if (investmentConcession.ConcessionConditions != null && investmentConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in investmentConcession.ConcessionConditions)
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

            var returnConcession = _investmentManager.GetInvestmentConcession(parentInvestmentConcession.Concession.ReferenceNumber, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }

        [Route("ForwardInvestmentPCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardBolPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            var investmentconsession = _investmentManager.GetInvestmentConcession(detail.ReferenceNumber, user);

            investmentconsession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            investmentconsession.Concession.BcmUserId = _bcmManager.GetBusinessCentreManager(investmentconsession.Concession.CentreId).BusinessCentreManagerId;

            investmentconsession.Concession.Comments = "Manually forwarded by PCM";
            investmentconsession.Concession.IsInProgressForwarding = true;

            await _investmentManager.ForwardInvestmentConcession(investmentconsession, user);

            return Ok(_investmentManager.GetInvestmentConcession(detail.ReferenceNumber, user));
        }




    }
}
