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
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
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

     
        [Route("NewBol")]
        [ValidateModel]
        public async Task<IActionResult> NewBol([FromBody] BolConcession bolConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            bolConcession.Concession.ConcessionType = Constants.ConcessionType.BusinessOnline;
            bolConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(bolConcession.Concession, user));

            foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
                await _mediator.Send(new BusinessLogic.Features.BolConcession.AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));

            if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in bolConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            return Ok(bolConcession);
        }
    }
}
