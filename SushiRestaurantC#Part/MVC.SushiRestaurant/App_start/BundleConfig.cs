using System.Web.Optimization;

namespace MVC.SushiRestaurant.App_start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/popperCore").Include(
                        "~/lib/popperjs/core/dist/cjs/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

        }
    }
}
