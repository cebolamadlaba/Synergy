using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Concession controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Concession")]
    public class ConcessionController : Controller
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="concessionManager"></param>
        /// <param name="lookupTableManager"></param>
        public ConcessionController(IConcessionManager concessionManager, ILookupTableManager lookupTableManager)
        {
            _concessionManager = concessionManager;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the concession conditions for the concession id
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        [Route("ConcessionConditions/{concessionId}")]
        public IActionResult ConcessionConditions(int concessionId)
        {
            return Ok(_concessionManager.GetConcessionConditions(concessionId));
        }

        /// <summary>
        /// Gets the product types for the concession type
        /// </summary>
        /// <param name="concessionType"></param>
        /// <returns></returns>
        [Route("ProductTypes/{concessionType}")]
        public IActionResult ProductTypes(string concessionType)
        {
            return Ok(_lookupTableManager.GetProductTypesForConcessionType(concessionType));
        }

        /// <summary>
        /// Gets the review fee types
        /// </summary>
        /// <returns></returns>
        [Route("ReviewFeeTypes")]
        public IActionResult ReviewFeeTypes()
        {
            return Ok(_lookupTableManager.GetReviewFeeTypes());
        }
    }
}