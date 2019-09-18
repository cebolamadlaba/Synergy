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
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

namespace StandardBank.ConcessionManagement.UI.Controllers
{

    [Produces("application/json")]
    [Route("api/Glms")]
    public class GlmsController : Controller
    {
        private readonly ISiteHelper _siteHelper;

        private readonly IGlmsManager _glmsManager;

        private readonly IMediator _mediator;

        private readonly ILookupTableManager _lookupTableManager;


        public GlmsController(ISiteHelper siteHelper, IGlmsManager glmsManager, IMediator mediator,ILookupTableManager lookupTableManager)
        {
            _siteHelper = siteHelper;
            _glmsManager = glmsManager;
            _mediator = mediator;
            _lookupTableManager = lookupTableManager;
        }

        /// <returns></returns>
        [Route("GlmsView/{riskGroupNumber}/{sapbpid}")]
        public IActionResult GlmsView(int riskGroupNumber, int sapbpid)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_glmsManager.GetGlmsViewData(riskGroupNumber, sapbpid, user));
        }

        [Route("NewGmls")]
        [ValidateModel]
        public async Task<IActionResult> NewGmls([FromBody] GlmsConcession glmsConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            glmsConcession.Concession.ConcessionType = Constants.ConcessionType.Glms;
            glmsConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(glmsConcession.Concession, user));

            //foreach (var investmentConcessionDetail in glmsConcession.GlmsConcessionDetails)
            //    await _mediator.Send(new BusinessLogic.Features.InvestmentConcession.AddOrUpdateInvestmentConcessionDetail(investmentConcessionDetail, user, concession));

            //if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
            //    foreach (var concessionCondition in investmentConcession.ConcessionConditions)
            //        await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));


            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.NewSubmission);

            if (!string.IsNullOrWhiteSpace(glmsConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, bcmPendingStatusId,
                    glmsConcession.Concession.Comments, user));

            return Ok(glmsConcession);
        }

    }
}
