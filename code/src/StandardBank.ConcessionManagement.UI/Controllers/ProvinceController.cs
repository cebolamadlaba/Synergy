using MediatR;
using Microsoft.AspNetCore.Mvc;

using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;
using System.Threading.Tasks;

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
        /// Initializes the controller
        /// </summary>
        /// <param name="pricingManager"></param>
        public ProvinceController(IProvinceManager provinceManager, IMediator mediator)
        {
            _provinceManager = provinceManager;
            _mediator = mediator;
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


        [Route("UpdateProvince")]
        public async Task<IActionResult> UpdateProvince([FromBody] Province province)
        {
            var result =  _provinceManager.MaintainProvince(province);
            

            return Ok(result);
        }
    }
}