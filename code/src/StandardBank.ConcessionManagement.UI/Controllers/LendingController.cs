using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Lending")]
    public class LendingController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="pricingManager"></param>
        /// <param name="lendingManager"></param>
        /// <param name="siteHelper"></param>
        /// <param name="mediator"></param>
        public LendingController(IPricingManager pricingManager, ILendingManager lendingManager, ISiteHelper siteHelper, IMediator mediator)
        {
            _pricingManager = pricingManager;
            _lendingManager = lendingManager;
            _siteHelper = siteHelper;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the lending view data
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("LendingView/{riskGroupNumber}")]
        public IActionResult LendingView(int riskGroupNumber)
        {
            //TODO: Eventually need to get source system product data from source systems 
            return Ok(new LendingView
            {
                RiskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber),
                LendingConcessions = _lendingManager.GetLendingConcessionsForRiskGroupNumber(riskGroupNumber)
            });
        }

        /// <summary>
        /// Saves the new lending
        /// </summary>
        /// <param name="lendingConcession"></param>
        /// <returns></returns>
        [Route("NewLending")]
        public async Task<IActionResult> NewLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var tempLendingConcession = new LendingConcession
            {
                Concession = new Concession
                {
                    AccountNumber = "001122334455",
                    ConcessionType = "Lending",
                    CustomerName = "Test Customer",
                    DateOpened = DateTime.Now,
                    Motivation = "Testing the system",
                    MrsCrs = 32.53m,
                    RiskGroupName = "EDCON",
                    RiskGroupNumber = 2006,
                    SmtDealNumber = "SMT00001",
                    Type = "New"
                },
                ConcessionConditions = new List<ConcessionCondition>(),
                LendingConcessionDetails = new List<LendingConcessionDetail>()
            };

            var concession = tempLendingConcession.Concession;
            concession.ConcessionType = "Lending";

            return Ok(await _mediator.Send(new AddConcessionCommand(concession, user)));
        }
    }
}