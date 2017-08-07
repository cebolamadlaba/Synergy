﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Lending
{
    /// <summary>
    /// New lending controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Lending/[controller]")]
    public class NewLendingController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewLendingController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="mediator">The mediator.</param>
        public NewLendingController(ISiteHelper siteHelper, IMediator mediator)
        {
            _siteHelper = siteHelper;
            _mediator = mediator;
        }

        /// <summary>
        /// Posts the specified lending concession.
        /// </summary>
        /// <param name="lendingConcession">The lending concession.</param>
        [HttpPost]
        public async Task<Model.UserInterface.Concession> Post([FromBody]LendingConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);
            var concession = lendingConcession.Concession;
            concession.ConcessionType = "Lending";

            return await _mediator.Send(new AddConcessionCommand(concession, user));
        }
    }
}