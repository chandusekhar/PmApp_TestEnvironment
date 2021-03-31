using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PM_Audit.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(


                     "~/Content/bower_components/jquery/dist/jquery.min.js",
                       "~/Content/bower_components/jquery/dist/jquery-migrate-1.4.1.min",
                          "~/Scripts/jquery-1.10.2.min.js",
                             "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Content/bower_components/jquery/dist/jquery.datetimepicker.js",
                        "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/Content/plugins/iCheck/icheck.min.js",
                        "~/Scripts/ui.js",
                        "~/Scripts/select2.full.min.js",
                           "~/Scripts/jquery.mask.min.js",
                                "~/Scripts/moment.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/GlobalScripts").Include(
                        "~/Scripts/ApiCallService.js",
                           "~/Scripts/Blob.js",
                           "~/Scripts/ajax-native.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/ExcelUpload").Include(
                     "~/Scripts/xlsx.full.min.js",
                       "~/Scripts/jszip.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/DataTableScript").Include(
                      "~/Scripts/jquery.dataTables.min.js",
                       "~/Scripts/jszip.js",
                      "~/Scripts/dataTables.responsive.js",
                      "~/Scripts/dataTables.buttons.min.js",
                      "~/Scripts/buttons.flash.min.js",
                      "~/Scripts/buttons.html5.min.js",
                      "~/Scripts/buttons.print.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                      "~/scripts/jquery.fullscreen.min.js",
                       "~/scripts/jquery.showLoading.min.js",
                       //"~/scripts/footable.js",
                       "~/scripts/demo-rows.js",
                      //   "~/scripts/jquery.dataTables.min.js",
                      //"~/scripts/bootstrap.js",
                      //"~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      "~/scripts/respond.js",
                      "~/scripts/fastclick.js",
                      "~/scripts/chart-2.7.1.min.js",
                      "~/scripts/chart.piecelabel.min.js",
                      "~/scripts/dataTables.bootstrap.min.js",
                      "~/scripts/main.js",
                      "~/scripts/accordion.js",
                       "~/scripts/BoxRefresh.js",
                        "~/scripts/BoxWidget.js",
                        "~/scripts/Chart.js",
                        "~/scripts/Chart.bundle.js",
                        "~/scripts/jquery.mtz.monthpicker.js",
                        "~/scripts/alertify.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/footable").Include("~/scripts/footable.js"));
            bundles.Add(new StyleBundle("~/bundles/Theme").Include(
                      "~/Content/Css/jquery-ui.css",
                      //"~/Content/bootstrap.css",
                      "~/Content/fontawesome-all.min.css",
                      "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/Content/bower_components/Ionicons/css/ionicons.min.css",
                      "~/Content/css/AdminLTE.min.css",
                      "~/Content/plugins/iCheck/square/blue.css",
                       "~/Content/plugins/iCheck/square/red.css",
                      "~/Content/Css/Site.css",
                       "~/Content/Css/select2.min.css",
                       "~/Content/Css/jquery.datetimepicker.css",
                       "~/Content/Css/simple-sidebar.css",
                      "~/Content/plugins/iCheck/flat/red.css",
                      "~/Content/dataTables.bootstrap.css",
                       "~/Content/simple-sidebar.css",
                      "~/Content/css/jquery.dataTables.min.css",
                      "~/Content/css/buttons.dataTables.min.css",
                      "~/Content/css/responsive.dataTables.css",
                       "~/Content/css/footable.bootstrap.min.css",
                        "~/Content/css/alertify.min.css"
                        //"~/Content/css/bootstrap-datetimepicker.min.css"
                        ));

           
        }
    }
}