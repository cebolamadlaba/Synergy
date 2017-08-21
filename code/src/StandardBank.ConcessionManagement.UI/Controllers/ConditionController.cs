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
        /// Initializes the controller
        /// </summary>
        /// <param name="lookupTableManager"></param>
        public ConditionController(ILookupTableManager lookupTableManager)
        {
            _lookupTableManager = lookupTableManager;
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
    }
}