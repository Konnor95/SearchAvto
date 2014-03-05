using System.Web.Optimization;

namespace SearchAvto
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/MainScripts").Include(
                "~/Scripts/jquery.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/superfish.js",
                "~/Scripts/jquery.flexslider-min.js",
                "~/Scripts/jquery.kwicks.min.js",
                "~/Scripts/jquery.easing.min.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/touchTouch.jquery.js"));

            bundles.Add(new StyleBundle("~/MainStyles").Include(
                "~/Content/Main/bootstrap.css",
                "~/Content/Main/bootstrap-theme.min.css",
                "~/Content/Main/responsive.css",
                "~/Content/Main/style.css",
                "~/Content/Main/touchTouch.css",
                "~/Content/Main/kwicks-slider.css",
                "~/Content/Main/docs.css"));

            bundles.Add(new ScriptBundle("~/bundles/ControlPanelScripts").Include(
                "~/Scripts/Dashboard/jquery-{version}.js",
                "~/Scripts/Dashboard/bootstrap.min.js",
                "~/Scripts/Dashboard/jquery.metisMenu.js",
                "~/Scripts/Dashboard/sb-admin.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/ControlPanelStyles").Include(
                "~/Content/Dashboard/bootstrap.min.css",
                "~/Content/Dashboard/font-awesome.css",
                "~/Content/Dashboard/iconmoon.css",
                "~/Content/Dashboard/sb-admin.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/MorrisScripts").Include(
                "~/Plugins/Morris/*.js"));

            bundles.Add(new StyleBundle("~/bundles/MorrisStyles").Include(
                "~/Plugins/Morris/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/DataTablesScripts").Include(
                "~/Plugins/DataTables/jquery.dataTables.js",
                "~/Plugins/DataTables/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/DataTablesStyles").Include(
                "~/Plugins/DataTables/*.css"));
        }
    }
}
