using System.Web.Optimization;

namespace XamStore.Application
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content").Include(
                         "~/Content/jquery.mmenu.all.css",
                         "~/Content/productviewgallery.css",
                         "~/Content/slider.css",
                         "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/bootstrap.css",
                         "~/Content/site.css",
                         "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                         "~/Scripts/jquery.min.js",
                         "~/Scripts/cloud-zoom.1.0.3.min.js",
                         "~/Scripts/easing.js",
                         "~/Scripts/jquery.eislideshow.js",
                         "~/Scripts/jquery.fancybox.pack.js",
                         "~/Scripts/jquery.mmenu.js",
                         "~/Scripts/move-top.js",
                         "~/Scripts/productviewgallery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/facebook").Include(
                   "~/Scripts/facebook.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                   "~/Scripts/custom.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
        }
    }
}
