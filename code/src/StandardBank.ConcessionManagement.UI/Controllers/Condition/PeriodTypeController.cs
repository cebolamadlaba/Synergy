using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Condition
{
    /// <summary>
    /// Period type controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Condition/[controller]")]
    public class PeriodTypeController : Controller
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodTypeController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public PeriodTypeController(ILookupTableManager lookupTableManager)
        {
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PeriodType> Get()
        {
            return _lookupTableManager.GetPeriodTypes();
        }
    }
}
