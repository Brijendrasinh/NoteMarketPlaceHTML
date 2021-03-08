using System.Web;
using System.Web.Optimization;

namespace NoteMarketPlace
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/js/jquery-3.5.1.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/js/bootstrap/bootstrap.min.js",
                        "~/Scripts/js/dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js",
                      
                      "~/Scripts/js/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap/bootstrap.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/css/font-awesome/css/font-awesome.min.css",
                      "~/Content/css/custom.css",
                      "~/Content/css/responsive-layout.min.css"));
        }
    }
}
