using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

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
        /// Initializes a new instance of the <see cref="CashController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        public CashController(ISiteHelper siteHelper)
        {
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets the cash view data
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("CashView/{riskGroupNumber}")]
        public IActionResult CashView(int riskGroupNumber)
        {
            var cashView = new CashView
            {
                RiskGroup = new RiskGroup {Id = 1, Name = "Test Risk Group", Number = riskGroupNumber},
                CashConcessions = new[]
                {
                    new CashConcession
                    {
                        Concession = new Concession(),
                        ConcessionConditions = new[] {new ConcessionCondition()},
                        CurrentUser = _siteHelper.LoggedInUser(this),
                        CashConcessionDetails = new[] {new CashConcessionDetail()}
                    }
                }
            };

            return Ok(cashView);
        }
    }
}
