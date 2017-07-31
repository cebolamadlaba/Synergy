using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Concession
{
    /// <summary>
    /// Review fee types controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Concession/[controller]")]
    public class ReviewFeeTypesController : Controller
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewFeeTypesController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public ReviewFeeTypesController(ILookupTableManager lookupTableManager)
        {
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ReviewFeeType> Get()
        {
            return _lookupTableManager.GetReviewFeeTypes();
        }
    }
}
