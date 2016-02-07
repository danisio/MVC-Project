namespace MySurveys.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap")
                   .Include("~/Content/bootstrap.journal.css"));

            bundles.Add(new StyleBundle("~/Content/kendo")
                   .Include("~/Content/kendo/kendo.common.min.css",
                            "~/Content/kendo/kendo.common-bootstrap.min.css",
                            "~/Content/kendo/kendo.flat.min.css"));

            bundles.Add(new StyleBundle("~/Content/custom")
                   .Include("~/Content/site.css"));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                   .Include("~/Scripts/kendo/jquery.min.js"));
            //.Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                   .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                   .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                   .Include("~/Scripts/bootstrap.js",
                         "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo")
                   .Include("~/Scripts/kendo/kendo.all.min.js",
                            "~/Scripts/kendo/kendo.aspnetmvc.min.js"));
        }
    }
}
