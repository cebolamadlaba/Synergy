using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
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
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionController"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="letterGeneratorManager">The letter generator manager.</param>
        /// <param name="mediator">The mediator.</param>
        public ConcessionController(IConcessionManager concessionManager, ILookupTableManager lookupTableManager,
            ISiteHelper siteHelper, ILetterGeneratorManager letterGeneratorManager, IMediator mediator)
        {
            _concessionManager = concessionManager;
            _lookupTableManager = lookupTableManager;
            _siteHelper = siteHelper;
            _letterGeneratorManager = letterGeneratorManager;
            _mediator = mediator;
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

        [Route("AllChannelTypes")]
        public IActionResult AllChannelTypes()
        {
            return Ok(_lookupTableManager.GetAllChannelTypes());
        }

        /// <summary>
        /// Gets the client accounts for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("ClientAccounts/{riskGroupNumber}")]
        public IActionResult ClientAccounts(int riskGroupNumber)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_concessionManager.GetClientAccounts(riskGroupNumber, user));
        }

        /// <summary>
        /// Searches the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        [Route("SearchClientAccounts/{riskGroupNumber}/{accountNumber}")]
        public IActionResult SearchClientAccounts(int riskGroupNumber, string accountNumber)
        {
            return Ok(_concessionManager.SearchClientAccounts(riskGroupNumber, accountNumber));
        }

        [Route("PrimeRate/{datefilter}")]
        public IActionResult PrimeRate(DateTime datefilter)
        {
            return Ok(_concessionManager.PrimeRate(datefilter));
        } 


        [Route("SearchConsessions")]
        public IActionResult SearchConsessions()
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            return Ok(_concessionManager.SearchConsessions(userId));
        }

        [Route("SearchConsessions/{region}/{centre}/{status}/{datefilter}")]
        public IActionResult SearchConsessions(int region,int centre,string status, DateTime datefilter)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            return Ok(_concessionManager.SearchConsessions(region,centre,status,datefilter, userId));
        }

        /// <summary>
        /// Gets the users approved concessions
        /// </summary>
        /// <returns></returns>
        [Route("UserApprovedConcessions")]
        public IActionResult UserApprovedConcessions()
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            return Ok(_concessionManager.GetApprovedConcessionsForUser(userId));
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

        [Route("GetTransactionTypes/{isActive}")]
        public IActionResult GetTransactionTypes(bool isActive)
        {
            return Ok(_lookupTableManager.GetTransactionalTransactionTypes(isActive));
        }     

        [Route("ConcessionTypes/{isActive}")]
        public IActionResult GetConcessionTypes(bool isActive)
        {
            return Ok(_lookupTableManager.GetConcessionTypes(isActive));
        }


        [Route("ActiveTableNumbers/{isActive}")]
        public IActionResult TableNumbers(bool isActive)
        {
            return Ok(_lookupTableManager.GetTableNumbers(isActive));
        }

        /// <summary>
        /// Tables the numbers.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        [Route("TableNumbers/{concessionType}")]
        public IActionResult TableNumbers(string concessionType)
        {
            return Ok(_lookupTableManager.GetTableNumbers(concessionType));
        }

        [Route("TableNumbersAll/{concessionType}")]
        public IActionResult TableNumbersAll(string concessionType)
        {
            return Ok(_lookupTableManager.GetTableNumbersAll(concessionType));
        }


        /// <summary>
        /// Deactivates the concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("DeactivateConcession/{concessionReferenceId}")]
        public async Task<IActionResult> DeactivateConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            await _mediator.Send(new DeactivateConcession(concessionReferenceId, user));

            return Ok(true);
        }

        /// <summary>
        /// Generates the concession letter.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        //[Route("GenerateConcessionLetter/{concessionReferenceId}")]
        //public FileResult GenerateConcessionLetter(string concessionReferenceId)
        //{
        //    HttpContext.Response.ContentType = "application/pdf";

        //    var result = new FileContentResult(_letterGeneratorManager.GenerateLetters(concessionReferenceId),
        //        "application/pdf")
        //    {
        //        FileDownloadName = $"ConcessionLetter_{concessionReferenceId}.pdf"
        //    };

        //    return result;
        //}

        /// <summary>
        /// Generates the concession letter for legal entity. --used
        /// </summary>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <returns></returns>
        [Route("GenerateConcessionLetterForLegalEntity/{legalEntityId}")]
        public FileResult GenerateConcessionLetterForLegalEntity(int legalEntityId)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            HttpContext.Response.ContentType = "application/pdf";

            var result = new FileContentResult(_letterGeneratorManager.GenerateLettersForLegalEntity(legalEntityId, userId),
                "application/pdf")
            {
                FileDownloadName = $"ConcessionLetter_{legalEntityId}.pdf"
            };

            return result;
        }

        /// <summary>
        /// Generates the concession letter for concession details.-- Used.
        /// </summary>
        /// <param name="concessionDetailIds">The concession detail ids.</param>
        /// <returns></returns>
        [Route("GenerateConcessionLetterForConcessionDetails/{concessionDetailIds}")]
        public FileResult GenerateConcessionLetterForConcessionDetails(string concessionDetailIds)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            HttpContext.Response.ContentType = "application/pdf";

            var convertedConcessionDetailIds = from concessionDetailId in concessionDetailIds.Split(',')
                select Convert.ToInt32(concessionDetailId);

            var result = new FileContentResult(
                _letterGeneratorManager.GenerateLettersForConcessionDetails(convertedConcessionDetailIds, userId),
                "application/pdf")
            {
                FileDownloadName = $"ConcessionLetter_{concessionDetailIds.Replace(",", "_")}.pdf"
            };

            return result;
        }
    }
}
