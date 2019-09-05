using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{

    [Produces("application/json")]
    [Route("api/Glms")]
    public class GlmsController : Controller
    {
        private readonly ISiteHelper _siteHelper;

        private readonly IGlmsManager _glmsManager;


        public GlmsController(ISiteHelper siteHelper, IGlmsManager glmsManager, IMediator mediator, IBusinessCentreManager businessCentreManager, ILookupTableManager lookupTableManager)
        {
            _siteHelper = siteHelper;
            _glmsManager = glmsManager;

        }

        /// <returns></returns>
        [Route("GlmsView/{riskGroupNumber}/{sapbpid}")]
        public IActionResult GlmsView(int riskGroupNumber, int sapbpid)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_glmsManager.GetGlmsViewData(riskGroupNumber, sapbpid, user));
        }

    }
}
