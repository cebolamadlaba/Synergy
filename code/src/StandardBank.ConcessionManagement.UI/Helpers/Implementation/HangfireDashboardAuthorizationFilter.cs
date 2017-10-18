using Hangfire.Dashboard;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Helpers.Implementation
{
    /// <summary>
    /// Hangfire dashboard authorisation filter
    /// </summary>
    /// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter" />
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Authorizes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();
            //return httpContext.User.Identity.IsAuthenticated;
            return true;
        }
    }
}
