using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateLendingConcessionDetail;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateTransactionalConcessionDetail;
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
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="transactionalManager">The transactional manager.</param>
        /// <param name="mediator">The mediator.</param>
        public TransactionalController(ISiteHelper siteHelper, IPricingManager pricingManager,
            ITransactionalManager transactionalManager, IMediator mediator)
        {
            _siteHelper = siteHelper;
            _pricingManager = pricingManager;
            _transactionalManager = transactionalManager;
            _mediator = mediator;
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
                TransactionalConcessions = _transactionalManager.GetTransactionalConcessionsForRiskGroupNumber(riskGroupNumber)
            };

            return Ok(transactionalView);
        }

        /// <summary>
        /// Gets the transactional concession data.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("TransactionalConcessionData/{concessionReferenceId}")]
        public IActionResult TransactionalConcessionData(string concessionReferenceId)
        {
            return Ok(_transactionalManager.GetTransactionalConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        /// <summary>
        /// Creates a new transactional concession
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("NewTransactional")]
        public async Task<IActionResult> NewTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            transactionalConcession.Concession.ConcessionType = "Transactional";
            transactionalConcession.Concession.Type = "New";

            var concession = await _mediator.Send(new AddConcession(transactionalConcession.Concession, user));

            foreach (var transactionalConcessionDetail in transactionalConcession.TransactionalConcessionDetails)
                await _mediator.Send(
                    new AddOrUpdateTransactionalConcessionDetail(transactionalConcessionDetail, user, concession));

            if (transactionalConcession.ConcessionConditions != null &&
                transactionalConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in transactionalConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            return Ok(transactionalConcession);
        }

        /// <summary>
        /// Updates the transactional concession.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("UpdateTransactional")]
        public IActionResult UpdateTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            //TODO:
            throw new NotImplementedException();
        }
    }
}
