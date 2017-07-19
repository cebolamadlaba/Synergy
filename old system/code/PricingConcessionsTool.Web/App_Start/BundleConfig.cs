using System.Web;
using System.Web.Optimization;

namespace PricingConcessionsTool.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/bower_components/metisMenu/dist/metisMenu.min.css",
                      "~/dist/css/sb-admin-2.css",
                      //"~/bower_components/font-awesome/css/font-awesome.min.css",
                      // "~/dist/css/timeline.css",
                      "~/Content/site.css",
                      "~/Content/funkyRadioButtons.css",
                      "~/Content/angular-toastr.css"));

                
              bundles.Add(new ScriptBundle("~/bundles/jquery")
                               .Include("~/Scripts/jquery-1.10.2.js"));       


            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-ui-router.js",              
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                 "~/Scripts/angular-animate.js",
                 "~/Scripts/angular-toastr.tpls.js",
                 "~/Scripts/datetime-picker.min.js",
                 "~/Scripts/angular-messages.min.js",
                 "~/Scripts/angular-bootstrap-multiselect.min.js",
                 "~/Scripts/angular-sanitize.min.js",
                 "~/Scripts/ngStorage.min.js")
                .Include("~/app/Main.js",
                         "~/app/Config.js")
                //.IncludeDirectory("~/app/models", "*.js")
                .IncludeDirectory("~/app/services", "*.js",true)
                .IncludeDirectory("~/app/directives", "*.js")
                .IncludeDirectory("~/app/controllers", "*.js",true)
                );




            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                                    .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/templateScripts")
                                    .Include("~/bower_components/metisMenu/dist/metisMenu.js")
                                    .Include("~/dist/js/sb-admin-2.js"));



        }
    }
}
