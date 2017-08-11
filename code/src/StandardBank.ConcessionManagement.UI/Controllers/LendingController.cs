﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddLendingConcessionDetail;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.UI.Validation;

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
        [ValidateModel]
        public async Task<IActionResult> NewLending([FromBody] LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            lendingConcession.Concession.ConcessionType = "Lending";
            lendingConcession.Concession.Type = "New";

            var concession = await _mediator.Send(new AddConcessionCommand(lendingConcession.Concession, user));

            foreach (var lendingConcessionDetail in lendingConcession.LendingConcessionDetails)
                await _mediator.Send(new AddLendingConcessionDetailCommand(lendingConcessionDetail, user, concession));

            return Ok(lendingConcession);
        }
    }
}