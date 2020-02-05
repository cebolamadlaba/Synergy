using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.TradeConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{

    [Produces("application/json")]
    [Route("api/Trade")]
    public class TradeController : Controller
    {

        private readonly ISiteHelper _siteHelper;

        private readonly ITradeManager _tradeManager;

        private readonly IMediator _mediator;

        private readonly IBusinessCentreManager _bcmManager;

        private readonly ILookupTableManager _lookupTableManager;


        public TradeController(ISiteHelper siteHelper, ITradeManager tradeManager, IMediator mediator, IBusinessCentreManager businessCentreManager, ILookupTableManager lookupTableManager)
        {
            _siteHelper = siteHelper;
            _tradeManager = tradeManager;
            _mediator = mediator;
            _bcmManager = businessCentreManager;
            _lookupTableManager = lookupTableManager;
        }

        /// <returns></returns>
        [Route("TradeView/{riskGroupNumber}/{sapbpid}")]
        public IActionResult TradeView(int riskGroupNumber, int sapbpid)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_tradeManager.GetTradeViewData(riskGroupNumber, sapbpid, user));
        }


        [Route("NewTrade")]
        [ValidateModel]
        public async Task<IActionResult> NewTrade([FromBody] TradeConcession tradeConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            tradeConcession.Concession.ConcessionType = Constants.ConcessionType.Trade;
            tradeConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(tradeConcession.Concession, user));

            foreach (var tradeConcessionDetail in tradeConcession.TradeConcessionDetails)
            {
                await _mediator.Send(new BusinessLogic.Features.TradeConcession.AddOrUpdateTradeConcessionDetail(tradeConcessionDetail, user, concession));
            }

            if (tradeConcession.ConcessionConditions != null && tradeConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in tradeConcession.ConcessionConditions)
                {
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.NewSubmission);

            if (!string.IsNullOrWhiteSpace(tradeConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, bcmPendingStatusId, tradeConcession.Concession.Comments, user));
            }

            return Ok(tradeConcession);
        }

        [Route("UpdateTrade")]
        [ValidateModel]
        public async Task<IActionResult> UpdateTrade([FromBody] TradeConcession tradeConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            await UpdateTradeConcession(tradeConcession, user);

            return Ok(_tradeManager.GetTradeConcession(tradeConcession.Concession.ReferenceNumber, user));
        }

        [Route("TradeConcessionData/{concessionReferenceId}")]
        public IActionResult TradeConcessionData(string concessionReferenceId)
        {
            return Ok(_tradeManager.GetTradeConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        private async Task UpdateTradeConcession(TradeConcession tradeConcession, User user)
        {
            var databaseTradeConcession = _tradeManager.GetTradeConcession(tradeConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseTradeConcession.ConcessionConditions)
            { 
                if (tradeConcession.ConcessionConditions == null ||
                    tradeConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                { 
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));
                }
            }

            //if there are any concession details that have been removed delete them
            foreach (var tradeConcessionDetail in databaseTradeConcession.TradeConcessionDetails)
            {
                if (tradeConcession.TradeConcessionDetails.All(_ => _.TradeConcessionDetailId != tradeConcessionDetail.TradeConcessionDetailId))
                {
                    await _mediator.Send(new DeleteTradeConcessionDetail(tradeConcessionDetail, user));
                }
            }

            if (!tradeConcession.Concession.AENumberUserId.HasValue)
            {
                tradeConcession.Concession.AENumberUserId = databaseTradeConcession.Concession.AENumberUserId;
            }

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(tradeConcession.Concession, user));

            //add all the new conditions and details and comments
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
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseTradeConcession.Concession.SubStatusId, tradeConcession.Concession.Comments, user));
            }

            if ((tradeConcession.Concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges || tradeConcession.Concession.SubStatus == Constants.ConcessionSubStatus.HoApprovedWithChanges) && tradeConcession.Concession.ConcessionComments != null)
            {
                if (tradeConcession.Concession.ConcessionComments.Count() > 0 && tradeConcession.Concession.ConcessionComments.First().UserDescription == "LogChanges")
                {
                    await _mediator.Send(new AddConcessionComment(concession.Id, databaseTradeConcession.Concession.SubStatusId, "LogChanges:" + tradeConcession.Concession.ConcessionComments.First().Comment, user));
                }
            }
        }


        [Route("UpdateRecalledTrade")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledTrade([FromBody] TradeConcession tradeConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
            await _mediator.Send(new ActivateConcession(tradeConcession.Concession.ReferenceNumber, user));

            //update the concession accordingly
            await UpdateTradeConcession(tradeConcession, user);

            return Ok(_tradeManager.GetTradeConcession(tradeConcession.Concession.ReferenceNumber, user));
        }

        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the concession details
            var tradeConcession = _tradeManager.GetTradeConcession(concessionReferenceId, user);

            var parentConcessionId = tradeConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = tradeConcession.Concession;
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

            tradeConcession.Concession = concession;

            //add all the new conditions and details
            foreach (var tradeConcessionDetail in tradeConcession.TradeConcessionDetails)
            {
                tradeConcessionDetail.DateApproved = null;
                tradeConcessionDetail.TradeConcessionDetailId = 0;
                await _mediator.Send(new AddOrUpdateTradeConcessionDetail(tradeConcessionDetail, user, concession));
            }

            if (tradeConcession.ConcessionConditions != null && tradeConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in tradeConcession.ConcessionConditions)
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

            var returnConcession = _tradeManager.GetTradeConcession(concessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return Ok(returnConcession);
        }

        [Route("RenewTrade")]
        [ValidateModel]
        public async Task<IActionResult> RenewBol([FromBody] TradeConcession tradeConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(tradeConcession, user, Constants.RelationshipType.Renewal);

            return Ok(returnConcession);
        }


        [Route("ResubmitTrade")]
        [ValidateModel]
        public async Task<IActionResult> ResubmitBol([FromBody] TradeConcession tradeConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(tradeConcession, user, Constants.RelationshipType.Resubmit);

            return Ok(returnConcession);
        }


        [Route("UpdateApprovedTrade")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedBol([FromBody] TradeConcession tradeConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(tradeConcession, user, Constants.RelationshipType.Update);

            return Ok(returnConcession);
        }

        private async Task<TradeConcession> CreateChildConcession(TradeConcession tradeConcession, User user, string relationship)
        {
            //get the parent trade concession details
            var parentTradeConcession = _tradeManager.GetTradeConcession(tradeConcession.Concession.ReferenceNumber, user);

            var parentConcessionId = parentTradeConcession.Concession.Id;

            tradeConcession.Concession.ReferenceNumber = string.Empty;
            tradeConcession.Concession.ConcessionType = Constants.ConcessionType.Trade;
            tradeConcession.Concession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(tradeConcession.Concession, user));

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

            var returnConcession = _tradeManager.GetTradeConcession(parentTradeConcession.Concession.ReferenceNumber, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }

        [Route("ForwardTradePCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardBolPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            var tradeconsession = _tradeManager.GetTradeConcession(detail.ReferenceNumber, user);

            tradeconsession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            tradeconsession.Concession.BcmUserId = _bcmManager.GetBusinessCentreManager(tradeconsession.Concession.CentreId).BusinessCentreManagerId;

            tradeconsession.Concession.Comments = "Manually forwarded by PCM";
            tradeconsession.Concession.IsInProgressForwarding = true;

            await _tradeManager.ForwardTradeConcession(tradeconsession, user);

            return Ok(_tradeManager.GetTradeConcession(detail.ReferenceNumber, user));
        }
    }
}
