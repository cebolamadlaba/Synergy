using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Transactional")]
    public class TransactionalController : Controller
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
        /// The transactional manager
        /// </summary>
        private readonly ITransactionalManager _transactionalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="transactionalManager">The transactional manager.</param>
        public TransactionalController(ISiteHelper siteHelper, IPricingManager pricingManager,
            ITransactionalManager transactionalManager)
        {
            _siteHelper = siteHelper;
            _pricingManager = pricingManager;
            _transactionalManager = transactionalManager;
        }

        /// <summary>
        /// Transactionals the view.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("TransactionalView/{riskGroupNumber}")]
        public IActionResult TransactionalView(int riskGroupNumber)
        {
            //TODO: Eventually need to get source system product data from source systems (i.e. cash specific source system product data)
            var transactionalView = new TransactionalView
            {
                RiskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber),
                TransactionalConcessions = _transactionalManager.GetCashConcessionsForRiskGroupNumber(riskGroupNumber)
            };

            return Ok(transactionalView);
        }
    }
}
