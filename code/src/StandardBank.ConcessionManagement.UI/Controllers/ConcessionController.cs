using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator;
using StandardBank.ConcessionManagement.Model.Repository;
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

        private readonly ILegalEntityAddressManager _legalEntityAddressManager;

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
        public ConcessionController(IConcessionManager concessionManager, ILookupTableManager lookupTableManager, ILegalEntityAddressManager legalEntityAddressManager,
            ISiteHelper siteHelper, ILetterGeneratorManager letterGeneratorManager, IMediator mediator)
        {
            _concessionManager = concessionManager;
            _lookupTableManager = lookupTableManager;
            _legalEntityAddressManager = legalEntityAddressManager;
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

            return Ok(_concessionManager.GetClientAccounts(riskGroupNumber, user, ""));
        }

        [Route("ClientAccountsConcessionType/{riskGroupNumber}/{sapbpid}/{concessiontype}")]
        public IActionResult ClientAccountsConcessionType(int riskGroupNumber, int sapbpid, string concessiontype)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_concessionManager.GetClientAccounts(riskGroupNumber, user, concessiontype, sapbpid: sapbpid));
        }

        /// <summary>
        /// Searches the client accounts.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <returns></returns>
        //[Route("SearchClientAccounts/{riskGroupNumber}/{accountNumber}")]
        //public IActionResult SearchClientAccounts(int riskGroupNumber, string accountNumber)
        //{
        //    return Ok(_concessionManager.SearchClientAccounts(riskGroupNumber, accountNumber));
        //}

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
        public IActionResult SearchConsessions(int region, int centre, string status, DateTime datefilter)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            return Ok(_concessionManager.SearchConsessions(region, centre, status, datefilter, userId));
        }

        [Route("GetLegalEntityAddress/{legalEntityId}")]
        public IActionResult GetLegalEntityAddress(int legalEntityId)
        {
            if (legalEntityId < 1)
                return NoContent();

            LegalEntityAddress legalEntityAddress = this._legalEntityAddressManager.GetLegalEntityAddressByLegalEntityId(legalEntityId);

            if (legalEntityAddress == null)
                legalEntityAddress = this._legalEntityAddressManager.GetLegalEntityAddressFromLegalEntityRepository(legalEntityId);

            return Ok(legalEntityAddress);
        }

        /// <summary>
        /// Gets the users approved concessions
        /// </summary>
        /// <returns></returns>
        [Route("UserApprovedConcessions")]
        public IActionResult UserApprovedConcessions()
        {
            var userAEId = _siteHelper.GetUserIdForFiltering(this);
            var currentUser= _siteHelper.LoggedInUser(this);

            return Ok(_concessionManager.GetApprovedConcessionsForUser(userAEId,currentUser));
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

            await _mediator.Send(new DeactivateConcession(concessionReferenceId, false, user));

            return Ok(true);
        }

        [Route("RecallConcession/{concessionReferenceId}")]
        public async Task<IActionResult> RecallConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            await _mediator.Send(new DeactivateConcession(concessionReferenceId, true, user));

            return Ok(true);
        }

        [Route("DeactivateConcessionDetailed/{concessionReferenceDetailedId}")]
        public async Task<IActionResult> DeactivateConcessionDetailed(int concessionReferenceDetailedId)
        {
            var user = _siteHelper.LoggedInUser(this);

            await _mediator.Send(new DeactivateConcessionDetailed(concessionReferenceDetailedId, user));

            return Ok(true);
        }

        /// <summary>
        /// Generates the concession letter for legal entity. --used
        /// </summary>
        /// <param name="legalEntityId">The legal entity identifier.</param>
        /// <returns></returns>
        [Route("GenerateConcessionLetterForLegalEntity/{legalEntityId}")]
        public IActionResult GenerateConcessionLetterForLegalEntity(int legalEntityId, [FromBody] LegalEntityConcessionLetter legalEntityConcessionLetter)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);

            this.SetLegalEntityAddress(legalEntityId, legalEntityConcessionLetter);

            byte[] bytes = _letterGeneratorManager.GenerateLettersForLegalEntity(legalEntityId, userId, legalEntityConcessionLetter);

            var file = new
            {
                contentType = "application/pdf",
                bytes = bytes,
                filename = $"ConcessionLetter_{legalEntityId}.pdf"
            };

            return Ok(file);
        }

        [HttpPost, Route("UploadLetter")]
        public async Task<IActionResult> UploadLetter()
        {
            var files = Request.Form.Files; // now you have them

            var ConcessionDetailedId = Request.Form["ConcessionDetailedId"].ToString();

            if (ConcessionDetailedId != null && files != null)
            {
                foreach (var f in files)
                {
                    //save to disk
                    string FQDNLocation = string.Format(@"c:\cms\{0}.pdf", ConcessionDetailedId.ToString());

                    using (var fileStream = new FileStream(FQDNLocation, FileMode.Create))
                    {
                        await f.CopyToAsync(fileStream);
                    }

                    //save the entry to the db
                    _concessionManager.CreateConcessionLetter(new Model.Repository.ConcessionLetter { fkConcessionDetailId = int.Parse(ConcessionDetailedId), Location = FQDNLocation });


                }
            }

            return Ok(true);
        }

        [Route("DownloadLetter/{concessionDetailId}")]
        public FileResult DownloadLetter(string concessionDetailId)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);
            HttpContext.Response.ContentType = "application/pdf";

            var convertedConcessionDetailId = Convert.ToInt32(concessionDetailId);

            var result = new FileContentResult(

                //this will be repalced by the actual bytes retrieved from db or disk  - TBA
                _letterGeneratorManager.DownloadLetterForConcessionDetail(convertedConcessionDetailId, userId),


                "application/pdf")
            {
                FileDownloadName = $"ConcessionLetter_{concessionDetailId.Replace(",", "_")}.pdf"
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

        [Route("GenerateConcessionLetterForConcessions/{concessionIds}")]
        public IActionResult GenerateLettersForConcessions(string concessionIds, [FromBody] LegalEntityConcessionLetter legalEntityConcessionLetter)
        {
            var userId = _siteHelper.GetUserIdForFiltering(this);

            this.SetLegalEntityAddress(legalEntityConcessionLetter.LegalEntityId, legalEntityConcessionLetter);

            var convertedConcessionIds = from concessionDetailId in concessionIds.Split(',')
                                         select Convert.ToInt32(concessionDetailId);

            byte[] bytes = _letterGeneratorManager.GenerateLettersForConcessions(convertedConcessionIds, userId, legalEntityConcessionLetter);

            var file = new
            {
                contentType = "application/pdf",
                bytes = bytes,
                filename = $"ConcessionLetter_{concessionIds.Replace(",", "_")}.pdf"
            };

            return Ok(file);
            
        }


        private void SetLegalEntityAddress(int legalEntityId, LegalEntityConcessionLetter legalEntityConcessionLetter)
        {
            this._legalEntityAddressManager.UpdateLegalEntityAddress(
                new LegalEntityAddress()
                {
                    LegalEntityId = legalEntityId,
                    ContactPerson = legalEntityConcessionLetter.ClientContactPerson,
                    CustomerName = legalEntityConcessionLetter.ClientName,
                    PostalAddress = legalEntityConcessionLetter.ClientPostalAddress,
                    City = legalEntityConcessionLetter.ClientCity,
                    PostalCode = legalEntityConcessionLetter.ClientPostalCode,
                });
        }

        /// <summary>
        /// Gets the  type.
        /// </summary>
        /// <returns></returns>
        [Route("InterestType")]
        public IActionResult InterestType()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }

        /// <summary>
        /// Gets the Base Rate Code.
        /// </summary>
        /// <returns></returns>
        [Route("BaseRateCode")]
        public IActionResult BaseRateCode()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }

        /// <summary>
        /// Gets the Glms Group.
        /// </summary>
        /// <returns></returns>
        [Route("GlmsGroup")]
        public IActionResult GlmsGroup()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }

        /// <summary>
        /// Gets the Interest Pricing Category.
        /// </summary>
        /// <returns></returns>
        [Route("InterestPricingCategory")]
        public IActionResult InterestPricingCategory()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }

        /// <summary>
        /// Gets the Rate Type.
        /// </summary>
        /// <returns></returns>
        [Route("RateType")]
        public IActionResult RateType()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }

        /// <summary>
        /// Gets the Slab Type.
        /// </summary>
        /// <returns></returns>
        [Route("SlabType")]
        public IActionResult SlabType()
        {
            return Ok(_lookupTableManager.GetAccrualTypes());
        }
    }
}
