using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

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
        /// Initializes the controller
        /// </summary>
        /// <param name="pricingManager"></param>
        public ProvinceController(IProvinceManager provinceManager)
        {
            _provinceManager = provinceManager;
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
    }
}