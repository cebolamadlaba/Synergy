using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Concession
{
    /// <summary>
    /// Concession conditions controller
    /// </summary>
    [Route("api/Concession/[controller]")]
    public class ConcessionConditionsController : Controller
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="concessionManager"></param>
        public ConcessionConditionsController(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Gets the concession conditions
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        [HttpGet("{concessionId}")]
        public IEnumerable<ConcessionCondition> Get(int concessionId)
        {
            return _concessionManager.GetConcessionConditions(concessionId);
        }
    }
}
