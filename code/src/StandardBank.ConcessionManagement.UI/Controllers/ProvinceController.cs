using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Pricing controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Province")]
    public class ProvinceController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IProvinceManager _provinceManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProvinceController"/> class.
        /// </summary>
        /// <param name="provinceManager">The province manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="siteHelper">The site helper.</param>
        public ProvinceController(IProvinceManager provinceManager, IMediator mediator, ISiteHelper siteHelper)
        {
            _provinceManager = provinceManager;
            _mediator = mediator;
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// returns all provinces
        /// </summary>
        /// <returns></returns>
        [Route("Provinces")]
        public IActionResult GetAllProvinces()
        {
            return Ok(_provinceManager.GetProvinces());
        }

        /// <summary>
        /// Updates the province.
        /// </summary>
        /// <param name="province">The province.</param>
        /// <returns></returns>
        [Route("UpdateProvince")]
        public async Task<IActionResult> UpdateProvince([FromBody] Province province)
        {
            var result =  await _mediator.Send(new AddOrUpdateProvinceDetail(province, _siteHelper.LoggedInUser(this)));

            return Ok(result);
        }
    }
}
