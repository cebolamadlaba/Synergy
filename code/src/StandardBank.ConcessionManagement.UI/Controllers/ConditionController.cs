using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

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
        /// Initializes a new instance of the <see cref="ConditionController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="concessionManager">The concession manager.</param>
        public ConditionController(ILookupTableManager lookupTableManager, IConcessionManager concessionManager)
        {
            _lookupTableManager = lookupTableManager;
            _concessionManager = concessionManager;
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
        public IActionResult MyConditions([FromRoute] string period = "3 Months",
            [FromRoute] string periodType = "Standard")
        {
            return Ok(_concessionManager.GetConditions(periodType, period));
        }

        /// <summary>
        /// Gets the condition counts.
        /// </summary>
        /// <returns></returns>
        [Route("ConditionCounts")]
        public IActionResult ConditionCounts()
        {
            return Ok(_concessionManager.GetConditionCounts());
        }
    }
}
