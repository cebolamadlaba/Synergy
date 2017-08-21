using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

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
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="concessionManager"></param>
        /// <param name="lookupTableManager"></param>
        /// <param name="siteHelper"></param>
        public ConcessionController(IConcessionManager concessionManager, ILookupTableManager lookupTableManager, ISiteHelper siteHelper)
        {
            _concessionManager = concessionManager;
            _lookupTableManager = lookupTableManager;
            _siteHelper = siteHelper;
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

        /// <summary>
        /// Gets the client accounts for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("ClientAccounts/{riskGroupNumber}")]
        public IActionResult ClientAccounts(int riskGroupNumber)
        {
            return Ok(_concessionManager.GetClientAccounts(riskGroupNumber));
        }

        /// <summary>
        /// Gets the users approved concessions
        /// </summary>
        /// <returns></returns>
        [Route("UserApprovedConcessions")]
        public IActionResult UserApprovedConcessions()
        {
            var user = _siteHelper.LoggedInUser(this);
            return Ok(_concessionManager.GetApprovedConcessionsForUser(user.Id));
        }
    }
}