using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;

namespace StandardBank.ConcessionManagement.UI.Controllers.Inbox
{
    /// <summary>
    /// Concessions summary controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/inbox/[controller]")]
    public class ConcessionsSummaryController : Controller
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ConcessionsSummary Get()
        {
            var concessionsSummary = new ConcessionsSummary();

            return concessionsSummary;
        }
    }
}
