﻿namespace MySurveys.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                   .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                   .Include("~/Scripts/bootstrap.js",
                         "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                   .Include("~/Content/bootstrap.journal.css",
                         "~/Content/site.css"));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                   .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                   .Include("~/Scripts/jquery.validate*"));
        }
    }
}
