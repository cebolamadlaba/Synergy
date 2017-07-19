using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PricingConcessionsTool.Utils
{
    public static class Util
    {
        public static string GetUserName(ApiController controller)
        {
            var name = controller.ControllerContext.RequestContext.Principal.Identity.Name;

            var aNumber = controller.ControllerContext.RequestContext.Principal.Identity.Name.Substring(name.LastIndexOf("\\") + 1);

            return aNumber;
        }


        public static bool IsDebug()
        {

#if (DEBUG)
            return true;
#else
            return false;
#endif

        }

    }
}