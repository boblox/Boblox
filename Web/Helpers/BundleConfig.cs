using System.Web.Optimization;

namespace Web.Helpers
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var orderer = new NonOrderingBundleOrderer();
            var cssRewriteUrl = new CssRewriteUrlTransform();

            //Basic Js&Css============================================================
            var styleBundle = new StyleBundle("~/css/site.css")
                .Include("~/css/fonts.css") //should be before any other stylesheet!
                .Include("~/css/jquery-ui/jquery-ui.min.css", cssRewriteUrl)

                .Include("~/css/magnific-popup.css")
                .Include("~/css/owl-carousel/owl.carousel.css", cssRewriteUrl)
                .Include("~/css/owl-carousel/owl.transitions.css")
                .Include("~/css/owl-carousel/owl.theme.css", cssRewriteUrl)
                .Include("~/css/bootstrap-select.min.css", cssRewriteUrl)

                .Include("~/css/styles.min.css", cssRewriteUrl);
            styleBundle.Orderer = orderer;
            bundles.Add(styleBundle);

            var scriptBundle = new ScriptBundle("~/js/site.js")
                .Include("~/js/jquery/jquery-2.2.1.min.js")
                .Include("~/js/bootstrap/bootstrap.min.js")
                .Include("~/js/jquery/jquery-ui-1.11.4.min.js")
                .Include("~/js/jquery/jquery.validate.min.js")//JQuery validation
                .Include("~/js/jquery/jquery.validate.unobtrusive.min.js")//MVC client validation with html attributes
                .Include("~/js/jquery/jquery.unobtrusive-ajax.min.js")//Added support for ajax forms...
                .Include("~/js/jquery/datepicker-uk.js")//Localization for Jquery date picker

                //.Include("~/js/imagesloaded.pkgd.min.js")
                //.Include("~/js/masonry.pkgd.min.js")
                //.Include("~/js/jquery.unveil.js")
                .Include("~/js/magnific.popup.min.js")
                .Include("~/js/owl-carousel/owl.carousel.min.js")
                .Include("~/js/director.min.js")
                .Include("~/js/bootstrap/bootstrap-select.min.js") //Plugin for dropdowns

                .Include("~/scripts/utils.js")
                .Include("~/scripts/config.js");
            scriptBundle.Orderer = orderer;
            bundles.Add(scriptBundle);

            //BundleTable.EnableOptimizations = false;
        }
    }
}