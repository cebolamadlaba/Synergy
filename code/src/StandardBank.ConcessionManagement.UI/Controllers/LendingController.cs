using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ActivateConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionComment;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionRelationship;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateLendingConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteLendingConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.UpdateConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Lending")]
    public class LendingController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

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

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="pricingManager"></param>
        /// <param name="lendingManager"></param>
        /// <param name="siteHelper"></param>
        /// <param name="mediator"></param>
        public LendingController(IPricingManager pricingManager, ILendingManager lendingManager, ISiteHelper siteHelper,
            IMediator mediator)
        {
            _pricingManager = pricingManager;
            _lendingManager = lendingManager;
            _siteHelper = siteHelper;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the lending view data
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("LendingView/{riskGroupNumber}")]
        public IActionResult LendingView(int riskGroupNumber)
        {
            return Ok(_lendingManager.GetLendingViewData(riskGroupNumber));
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

            lendingConcession.Concession.ConcessionType = "Lending";
            lendingConcession.Concession.Type = "New";

            var concession = await _mediator.Send(new AddConcession(lendingConcession.Concession, user));

            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
                await _mediator.Send(new AddOrUpdateLendingConcessionDetail(lendingConcessionDetail, user, concession));

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            return Ok(lendingConcession);
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

            return Ok(lendingConcession);
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
                if (lendingConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any lending concession details that have been removed delete them
            foreach (var lendingConcessionDetail in databaseLendingConcession.LendingConcessionDetails)
                if (lendingConcession.LendingConcessionDetails.All(_ => _.LendingConcessionDetailId !=
                                                                        lendingConcessionDetail
                                                                            .LendingConcessionDetailId))
                    await _mediator.Send(new DeleteLendingConcessionDetail(lendingConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(lendingConcession.Concession, user));

            //add all the new conditions and lending details and comments
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
                await _mediator.Send(new AddOrUpdateLendingConcessionDetail(lendingConcessionDetail, user, concession));

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(lendingConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, concession.SubStatusId.Value,
                    lendingConcession.Concession.Comments, user));
        }

        /// <summary>
        /// Extends the concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var lendingConcession = await CreateChildConcession(concessionReferenceId, "Extension");

            return Ok(lendingConcession);
        }

        /// <summary>
        /// Creates the child concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <param name="relationshipType">Type of the relationship.</param>
        /// <returns></returns>
        private async Task<LendingConcession> CreateChildConcession(string concessionReferenceId, string relationshipType)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the lending concession details
            var lendingConcession =
                _lendingManager.GetLendingConcession(concessionReferenceId, user);

            var parentConcessionId = lendingConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = lendingConcession.Concession;
            newConcession.Id = 0;
            newConcession.Status = "Pending";
            newConcession.BcmUserId = null;
            newConcession.DateOpened = DateTime.Now;
            newConcession.DateSentForApproval = DateTime.Now;
            newConcession.HoUserId = null;
            newConcession.PcmUserId = null;
            newConcession.ReferenceNumber = string.Empty;
            newConcession.SubStatus = "BCM Pending";
            newConcession.SubStatusId = null;
            newConcession.Type = "Existing";

            var concession = await _mediator.Send(new AddConcession(newConcession, user));

            lendingConcession.Concession = concession;

            //add all the new conditions and lending details
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
            {
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
            return lendingConcession;
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

            //get the parent lending concession details
            var parentLendingConcession = _lendingManager.GetLendingConcession(lendingConcession.Concession.ReferenceNumber, user);

            var parentConcessionId = parentLendingConcession.Concession.Id;

            lendingConcession.Concession.ReferenceNumber = string.Empty;
            lendingConcession.Concession.ConcessionType = "Lending";
            lendingConcession.Concession.Type = "New";

            var concession = await _mediator.Send(new AddConcession(lendingConcession.Concession, user));

            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
                await _mediator.Send(new AddOrUpdateLendingConcessionDetail(lendingConcessionDetail, user, concession));

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            //link the new concession to the old concession
            var concessionRelationship = new ConcessionRelationship
            {
                CreationDate = DateTime.Now,
                UserId = user.Id,
                RelationshipDescription = "Renewal",
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };

            await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));

            return Ok(lendingConcession);
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

            return Ok(lendingConcession);
        }
    }
}
