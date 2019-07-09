using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Condition")]
    public class ConditionController : Controller
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="siteHelper">The site helper.</param>
        public ConditionController(ILookupTableManager lookupTableManager, IConcessionManager concessionManager,
            IMediator mediator, ISiteHelper siteHelper)
        {
            _lookupTableManager = lookupTableManager;
            _concessionManager = concessionManager;
            _mediator = mediator;
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets the condition types
        /// </summary>
        /// <returns></returns>
        [Route("ConditionTypes")]
        public IActionResult ConditionTypes()
        {
            return Ok(_lookupTableManager.GetConditionTypes());
        }

        [Route("BOLChargeCodes/{riskGroupNumber}")]
        public IActionResult GetBOLChargeCodes(int riskGroupNumber)
        {

            var codes = _lookupTableManager.GetBOLChargeCodes(riskGroupNumber);
            return Ok(codes);
        }


        [Route("BOLChargeCodesAll")]
        public IActionResult GetAllBOLChargeCodes()
        {

            var codes = _lookupTableManager.GetBOLChargeCodesAll();
            return Ok(codes);
        }

        [Route("BOLChargeCodeTypes")]
        public IActionResult GetBOLChargeCodeTypes()
        {
            return Ok(_lookupTableManager.GetBOLChargeCodeTypes());
        }


        [Route("TradeProductTypes")]
        public IActionResult GetTradeProductTypes()
        {
            return Ok(_lookupTableManager.GetTradeProductTypes());
        }

        [Route("TradeProducts")]
        public IActionResult TradeProducts()
        {
            return Ok(_lookupTableManager.GetTradeProducts());
        }


        [Route("LegalEntityBOLUsers/{riskGroupNumber}")]
        public IActionResult GetLegalEntityBOLUsers(int riskGroupNumber)
        {
            return Ok(_lookupTableManager.GetLegalEntityBOLUsers(riskGroupNumber));
        }
        //GetLegalEntityBOLUsersByLegalEntityId
        [Route("GetLegalEntityBOLUsersBySAPBPID/{sapbpid}")]
        public IActionResult GetLegalEntityBOLUsersBySAPBPID(int sapbpid)
        {
            LegalEntity legalEntity = this._lookupTableManager.GetLegalEntity(sapbpid);
            return Ok(_lookupTableManager.GetLegalEntityBOLUsersByLegalEntityId(legalEntity.Id));
        }

        [Route("LegalEntityGBBNumbers/{riskGroupNumber}")]
        public IActionResult GetLegalEntityGBBNumbers(int riskGroupNumber)
        {
            return Ok(_lookupTableManager.GetLegalEntityGBBNumbers(riskGroupNumber));
        }

        [Route("LegalEntityGBBNumbersBySAPBPID/{sapbpid}")]
        public IActionResult GetLegalEntityGBBNumbersBySAPBPID(int sapbpid)
        {
            return Ok(_lookupTableManager.GetLegalEntityGBBNumbersBySAPBPID(sapbpid));
        }

        /// <summary>
        /// Gets the periods
        /// </summary>
        /// <returns></returns>
        [Route("Periods")]
        public IActionResult Periods()
        {
            return Ok(_lookupTableManager.GetPeriods());
        }

        /// <summary>
        /// Gets the period types
        /// </summary>
        /// <returns></returns>
        [Route("PeriodTypes")]
        public IActionResult PeriodTypes()
        {
            return Ok(_lookupTableManager.GetPeriodTypes());
        }

        /// <summary>
        /// Gets the users conditions.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <param name="periodType">Type of the period.</param>
        /// <returns></returns>
        [Route("MyConditions/{period}/{periodType}")]
        public IActionResult MyConditions([FromRoute] string period = Constants.Period.ThreeMonths,
            [FromRoute] string periodType = Constants.PeriodType.Standard)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);

            return Ok(_concessionManager.GetConditions(periodType, period, userId));
        }

        /// <summary>
        /// Gets the condition counts.
        /// </summary>
        /// <returns></returns>
        [Route("ConditionCounts")]
        public IActionResult ConditionCounts()
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);

            return Ok(_concessionManager.GetConditionCounts(userId));
        }

        /// <summary>
        /// Updates the condition.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <returns></returns>
        [Route("UpdateCondition")]
        public async Task<IActionResult> UpdateCondition([FromBody] ConcessionCondition concessionCondition)
        {
            var user = _siteHelper.LoggedInUser(this);
            var concession =
                _concessionManager.GetConcessionForConcessionReferenceId(concessionCondition.ConcessionReferenceNumber, user);

            var result = await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            return Ok(result);
        }
    }
}
