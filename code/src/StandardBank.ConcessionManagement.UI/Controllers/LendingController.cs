using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddLendingConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.DeactivateConcession;
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

            var concession = await _mediator.Send(new AddConcessionCommand(lendingConcession.Concession, user));

            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
                await _mediator.Send(new AddLendingConcessionDetailCommand(lendingConcessionDetail, user, concession));

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                    await _mediator.Send(new AddConcessionConditionCommand(concessionCondition, user, concession));

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
            var databaseLendingConcession =
                _lendingManager.GetLendingConcession(lendingConcession.Concession.ReferenceNumber);

            //first delete all the conditions and the lending details
            foreach (var condition in databaseLendingConcession.ConcessionConditions)
                await _mediator.Send(new DeleteConcessionConditionCommand(condition, user));

            foreach (var lendingConcessionDetail in databaseLendingConcession.LendingConcessionDetails)
                await _mediator.Send(new DeleteLendingConcessionDetailCommand(lendingConcessionDetail, user));

            //second update the concession
            lendingConcession.Concession.ConcessionType = "Lending";
            lendingConcession.Concession.Type = "Existing";

            var concession = await _mediator.Send(new UpdateConcessionCommand(lendingConcession.Concession, user));

            //then add all the new conditions and lending details
            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
                await _mediator.Send(new AddLendingConcessionDetailCommand(lendingConcessionDetail, user, concession));

            if (lendingConcession.ConcessionConditions != null && lendingConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in lendingConcession.ConcessionConditions)
                    await _mediator.Send(new AddConcessionConditionCommand(concessionCondition, user, concession));

            return Ok(lendingConcession);
        }

        /// <summary>
        /// Gets the lending concession data for the concession reference id specified
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <returns></returns>
        [Route("LendingConcessionData/{concessionReferenceId}")]
        public IActionResult LendingConcessionData(string concessionReferenceId)
        {
            return Ok(_lendingManager.GetLendingConcession(concessionReferenceId));
        }
    }
}