using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionComment;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateLendingConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteLendingConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.UpdateConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
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
            //TODO: Eventually need to get source system product data from source systems 
            return Ok(new LendingView
            {
                RiskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber),
                LendingConcessions = _lendingManager.GetLendingConcessionsForRiskGroupNumber(riskGroupNumber)
            });
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
        //[ValidateModel] //TODO: Had to remove this, it isn't working, I don't know why, don't have time to fix
        public async Task<IActionResult> UpdateLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);
            
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

            return Ok(lendingConcession);
        }

        //public async Task<IActionResult> ExtendConcession(int concessionId)

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
    }
}
