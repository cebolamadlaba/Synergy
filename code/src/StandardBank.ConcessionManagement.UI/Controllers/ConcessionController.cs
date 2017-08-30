using System.Collections.Generic;
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
        /// The letter generator manager
        /// </summary>
        private readonly ILetterGeneratorManager _letterGeneratorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionController"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="letterGeneratorManager">The letter generator manager.</param>
        public ConcessionController(IConcessionManager concessionManager, ILookupTableManager lookupTableManager,
            ISiteHelper siteHelper, ILetterGeneratorManager letterGeneratorManager)
        {
            _concessionManager = concessionManager;
            _lookupTableManager = lookupTableManager;
            _siteHelper = siteHelper;
            _letterGeneratorManager = letterGeneratorManager;
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
        /// Gets the accrual types.
        /// </summary>
        /// <returns></returns>
        [Route("AccrualTypes")]
        public IActionResult AccrualTypes()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }

        /// <summary>
        /// Gets the channel types.
        /// </summary>
        /// <returns></returns>
        [Route("ChannelTypes")]
        public IActionResult ChannelTypes()
        {
            return Ok(_lookupTableManager.GetChannelTypes());
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

        /// <summary>
        /// Prints the concession letters.
        /// </summary>
        /// <param name="concessionIds">The concession ids.</param>
        /// <returns></returns>
        [Route("PrintConcessionLetters")]
        public IActionResult PrintConcessionLetters([FromBody]IEnumerable<int> concessionIds)
        {
            return Ok(_letterGeneratorManager.GenerateLetters(concessionIds));
        }

        /// <summary>
        /// Gets the transaction types for the concession type specified.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        [Route("TransactionTypes/{concessionType}")]
        public IActionResult TransactionTypes(string concessionType)
        {
            return Ok(_lookupTableManager.GetTransactionTypesForConcessionType(concessionType));
        }

        /// <summary>
        /// Gets the table numbers.
        /// </summary>
        /// <returns></returns>
        [Route("TableNumbers")]
        public IActionResult TableNumbers()
        {
            return Ok(_lookupTableManager.GetTableNumbers());
        }
    }
}
