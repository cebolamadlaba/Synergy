using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.CashConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
   /// <summary>
    /// Cash controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Bol")]
    public class BolController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IBolManager _bolManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

     
        public BolController(ISiteHelper siteHelper, IBolManager bolManager, IMediator mediator)
        {
            _siteHelper = siteHelper;
            _bolManager = bolManager;
            _mediator = mediator;
        }
     
        /// <returns></returns>
        [Route("BolView/{riskGroupNumber}")]
        public IActionResult BolView(int riskGroupNumber)
        {
            return Ok(_bolManager.GetBolViewData(riskGroupNumber));
        }
    }
}
