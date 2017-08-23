using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddCashConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
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
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

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
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="cashManager">The cash manager.</param>
        /// <param name="mediator">The mediator.</param>
        public CashController(ISiteHelper siteHelper, IPricingManager pricingManager, ICashManager cashManager,
            IMediator mediator)
        {
            _siteHelper = siteHelper;
            _pricingManager = pricingManager;
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
            //TODO: Eventually need to get source system product data from source systems (i.e. cash specific source system product data)
            var cashView = new CashView
            {
                RiskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber),
                CashConcessions = _cashManager.GetCashConcessionsForRiskGroupNumber(riskGroupNumber)
            };

            return Ok(cashView);
        }

        /// <summary>
        /// Creates a new cash concession.
        /// </summary>
        /// <param name="cashConcession">The cash concession.</param>
        /// <returns></returns>
        [Route("NewCash")]
        public async Task<IActionResult> NewCash([FromBody] CashConcession cashConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            cashConcession.Concession.ConcessionType = "Cash";
            cashConcession.Concession.Type = "New";

            var concession = await _mediator.Send(new AddConcessionCommand(cashConcession.Concession, user));

            foreach (var cashConcessionDetail in cashConcession.CashConcessionDetails)
                await _mediator.Send(new AddCashConcessionDetailCommand(cashConcessionDetail, user, concession));

            if (cashConcession.ConcessionConditions != null && cashConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in cashConcession.ConcessionConditions)
                    await _mediator.Send(new AddConcessionConditionCommand(concessionCondition, user, concession));

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
    }
}
