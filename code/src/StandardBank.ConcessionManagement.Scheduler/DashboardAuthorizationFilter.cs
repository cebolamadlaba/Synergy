using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Annotations;

namespace StandardBank.ConcessionManagement.Scheduler
{
    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context) => true;
        
    }
}
