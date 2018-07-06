using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using System;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Inbox controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Inbox")]
    public class InboxController : Controller
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        private IMemoryCache _cache;

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="concessionManager"></param>
        /// <param name="siteHelper"></param>
        public InboxController(IConcessionManager concessionManager, ISiteHelper siteHelper, IMemoryCache memoryCache)
        {
            _concessionManager = concessionManager;
            _siteHelper = siteHelper;
            _cache = memoryCache;
        }

        //[Route("NewTransactional")]
        //[ValidateModel]
        //public async Task<IActionResult> NewTransactional([FromBody] TransactionalConcession transactionalConcession)


        [Route("CacheAEUser")]
        public IActionResult CacheAEUser([FromBody] int accountExecutiveUserId)
        {
            var usr = _siteHelper.LoggedInUser(this);


            int cacheEntry;

            // Look for cache key.
           // if (!_cache.TryGetValue(usr.ANumber.ToLower() +  "_accountExecutiveUserId", out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = accountExecutiveUserId;//DateTime.Now;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromHours(24));

                // Save data in cache.
                _cache.Set(usr.ANumber.ToLower() + "_accountExecutiveUserId", cacheEntry, cacheEntryOptions);
            }

            return Ok(cacheEntry);
        }

        /// <summary>
        /// Gets the user concessions
        /// </summary>
        /// <returns></returns>
        [Route("UserConcessions")]
        public IActionResult UserConcessions()
        {
            return Ok(_concessionManager.GetUserConcessions(_siteHelper.LoggedInUser(this)));
        }

        [Route("ActionedConcessions")]
        public IActionResult ActionedConcessions()
        {
            return Ok(_concessionManager.GetActionedConcessionsForUser(_siteHelper.LoggedInUser(this)));
        }
    }
}
