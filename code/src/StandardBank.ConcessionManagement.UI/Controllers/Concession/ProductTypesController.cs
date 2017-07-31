using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Concession
{
    /// <summary>
    /// Product types controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Concession/[controller]")]
    public class ProductTypesController : Controller
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypesController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public ProductTypesController(ILookupTableManager lookupTableManager)
        {
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the specified concession type.
        /// </summary>
        /// <param name="concessionType">Type of the concession.</param>
        /// <returns></returns>
        [HttpGet("{concessionType}")]
        public IEnumerable<ProductType> Get(string concessionType)
        {
            return _lookupTableManager.GetProductTypesForConcessionType(concessionType);
        }
    }
}
