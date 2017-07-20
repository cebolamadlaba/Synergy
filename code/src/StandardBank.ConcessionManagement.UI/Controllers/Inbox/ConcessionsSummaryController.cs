using System;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;

namespace StandardBank.ConcessionManagement.UI.Controllers.Inbox
{
    /// <summary>
    /// Concessions summary controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/inbox/[controller]")]
    public class ConcessionsSummaryController : Controller
    {
        /// <summary>
        /// The concession count repository
        /// </summary>
        private readonly IConcessionCountRepository _concessionCountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionsSummaryController"/> class.
        /// </summary>
        /// <param name="concessionCountRepository">The concession count repository.</param>
        public ConcessionsSummaryController(IConcessionCountRepository concessionCountRepository)
        {
            _concessionCountRepository = concessionCountRepository;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ConcessionsSummary Get()
        {
            var concessionsSummary = new ConcessionsSummary();
            var concessionCounts = _concessionCountRepository.ReadAll();

            foreach (var concessionCount in concessionCounts)
            {
                switch (concessionCount.Status)
                {
                    case "Pending":
                        concessionsSummary.PendingConcessions = concessionCount.Count;
                        break;
                    case "Declined":
                        concessionsSummary.DeclinedConcessions = concessionCount.Count;
                        break;
                    case "DueForExpiry":
                        concessionsSummary.DueForExpiryConcessions = concessionCount.Count;
                        break;
                    case "Expired":
                        concessionsSummary.ExpiredConcessions = concessionCount.Count;
                        break;
                    case "Mismatched":
                        concessionsSummary.MismatchedConcessions = concessionCount.Count;
                        break;
                }
            }

            return concessionsSummary;
        }
    }
}
