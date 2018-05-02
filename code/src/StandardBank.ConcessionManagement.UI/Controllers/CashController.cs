using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.CashConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Cash controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Cash")]
    public class CashController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ICashManager _cashManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="cashManager">The cash manager.</param>
        /// <param name="mediator">The mediator.</param>
        public CashController(ISiteHelper siteHelper, ICashManager cashManager, IMediator mediator)
        {
            _siteHelper = siteHelper;
            _cashManager = cashManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the cash view data
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("CashView/{riskGroupNumber}")]
        public IActionResult CashView(int riskGroupNumber)
        {
            return Ok(_cashManager.GetCashViewData(riskGroupNumber));
        }

        /// <summary>
        /// Creates a new cash concession.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("NewCash")]
        [ValidateModel]
        public async Task<IActionResult> NewCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            cashConcession.Concession.ConcessionType = Constants.ConcessionType.Cash;
            cashConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(cashConcession.Concession, user));

            foreach (var cashConcessionDetail in cashConcession.CashConcessionDetails)
                await _mediator.Send(new AddOrUpdateCashConcessionDetail(cashConcessionDetail, user, concession));

            if (cashConcession.ConcessionConditions != null && cashConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in cashConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            return Ok(cashConcession);
        }

        /// <summary>
        /// Gets the cash concession data for the concession reference id specified.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("CashConcessionData/{concessionReferenceId}")]
        public IActionResult CashConcessionData(string concessionReferenceId)
        {
            return Ok(_cashManager.GetCashConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        /// <summary>
        /// Updates the cash concession.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("UpdateCash")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);
            
            await UpdateCashConcession(cashConcession, user);

            return Ok(_cashManager.GetCashConcession(cashConcession.Concession.ReferenceNumber, user));
        }

        /// <summary>
        /// Creates a new transactional concession
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("ForwardCashPCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardCashPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            var cashconsession =_cashManager.GetCashConcession(detail.ReferenceNumber, user);

            cashconsession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            cashconsession.Concession.BcmUserId = user.Id;
            cashconsession.Concession.Comments = "Manually forwarded by PCM";
            cashconsession.Concession.IsInProgressForwarding = true;

            await _cashManager.ForwardCashConcession(cashconsession, user);

            return Ok(_cashManager.GetCashConcession(detail.ReferenceNumber, user));
        }     


        /// <summary>
        /// Updates the cash concession.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private async Task UpdateCashConcession(CashConcession cashConcession, User user)
        {
            var databaseCashConcession =
                _cashManager.GetCashConcession(cashConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseCashConcession.ConcessionConditions)
                if (cashConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any cash concession details that have been removed delete them
            foreach (var cashConcessionDetail in databaseCashConcession.CashConcessionDetails)
                if (cashConcession.CashConcessionDetails.All(_ => _.CashConcessionDetailId !=
                                                                  cashConcessionDetail.CashConcessionDetailId))
                    await _mediator.Send(new DeleteCashConcessionDetail(cashConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(cashConcession.Concession, user));

            //add all the new conditions and cash details and comments
            foreach (var cashConcessionDetail in cashConcession.CashConcessionDetails)
                await _mediator.Send(new AddOrUpdateCashConcessionDetail(cashConcessionDetail, user, concession));

            if (cashConcession.ConcessionConditions != null && cashConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in cashConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(cashConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseCashConcession.Concession.SubStatusId,
                    cashConcession.Concession.Comments, user));
        }

        /// <summary>
        /// Extends the concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the cash concession details
            var cashConcession =
                _cashManager.GetCashConcession(concessionReferenceId, user);

            var parentConcessionId = cashConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = cashConcession.Concession;
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

            cashConcession.Concession = concession;

            //add all the new conditions and cash details
            foreach (var cashConcessionDetail in cashConcession.CashConcessionDetails)
            {
                cashConcessionDetail.DateApproved = null;
                cashConcessionDetail.CashConcessionDetailId = 0;
                await _mediator.Send(new AddOrUpdateCashConcessionDetail(cashConcessionDetail, user, concession));
            }

            if (cashConcession.ConcessionConditions != null && cashConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in cashConcession.ConcessionConditions)
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

            var returnConcession = _cashManager.GetCashConcession(concessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return Ok(returnConcession);
        }

        /// <summary>
        /// Renews the cash.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("RenewCash")]
        [ValidateModel]
        public async Task<IActionResult> RenewCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(cashConcession, user, Constants.RelationshipType.Renewal);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Resubmits the cash.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("ResubmitCash")]
        [ValidateModel]
        public async Task<IActionResult> ResubmitCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(cashConcession, user, Constants.RelationshipType.Resubmit);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Updates the approved cash.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("UpdateApprovedCash")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(cashConcession, user, Constants.RelationshipType.Update);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Creates the child concession.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <param name="user">The user.</param>
        /// <param name="relationship">The relationship.</param>
        /// <returns></returns>
        private async Task<CashConcession> CreateChildConcession(CashConcession cashConcession, User user, string relationship)
        {
            //get the parent cash concession details
            var parentCashConcession = _cashManager.GetCashConcession(cashConcession.Concession.ReferenceNumber, user);

            var parentConcessionId = parentCashConcession.Concession.Id;

            cashConcession.Concession.ReferenceNumber = string.Empty;
            cashConcession.Concession.ConcessionType = Constants.ConcessionType.Cash;
            cashConcession.Concession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(cashConcession.Concession, user));

            foreach (var cashConcessionDetail in cashConcession.CashConcessionDetails)
                await _mediator.Send(new AddOrUpdateCashConcessionDetail(cashConcessionDetail, user, concession));

            if (cashConcession.ConcessionConditions != null && cashConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in cashConcession.ConcessionConditions)
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

            var returnConcession = _cashManager.GetCashConcession(parentCashConcession.Concession.ReferenceNumber, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }

        /// <summary>
        /// Updates the recalled cash.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("UpdateRecalledCash")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
            await _mediator.Send(new ActivateConcession(cashConcession.Concession.ReferenceNumber, user));

            //update the concession accordingly
            await UpdateCashConcession(cashConcession, user);

            return Ok(_cashManager.GetCashConcession(cashConcession.Concession.ReferenceNumber, user));
        }

        /// <summary>
        /// Latests the CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("LatestCrsOrMrs/{riskGroupNumber}")]
        public IActionResult LatestCrsOrMrs(int riskGroupNumber)
        {
            return Ok(_cashManager.GetLatestCrsOrMrs(riskGroupNumber));
        }

        /// <summary>
        /// Cashes the financial.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("CashFinancial/{riskGroupNumber}")]
        public IActionResult CashFinancial(int riskGroupNumber)
        {
            return Ok(_cashManager.GetCashFinancialForRiskGroupNumber(riskGroupNumber));
        }


        [Route("CreateChannelType")]
        public async Task<IActionResult> CreateChannelType([FromBody] ChannelType channelType)
        {
            var model = new Model.Repository.ChannelType() { Description = channelType.Description, IsActive = true };

            return Ok(_cashManager.CreateChannelType(model));
        }


        [Route("CreateupdateTableNumber")]
        public async Task<IActionResult> CreateupdateTableNumber([FromBody] TableNumber transactionTableNumber)
        {

            return Ok(_cashManager.CreateupdateTableNumber(transactionTableNumber));
        }
    }
}
