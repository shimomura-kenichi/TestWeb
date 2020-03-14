using System.Web;
using System.Web.Optimization;
using TestWeb.Models.Attributes;

namespace TestWeb
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CacheBusting用バージョン付与
            UpdateDateBundleTransform transform = new UpdateDateBundleTransform();

            AddBundle(bundles, new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", "~/Scripts/jquery.unobtrusive-ajax.js"), transform);

            AddBundle(bundles, new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"), transform);

            AddBundle(bundles, new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui.js"), transform);

            // 開発と学習には、Modernizr の開発バージョンを使用します。次に、実稼働の準備が
            // 運用の準備が完了したら、https://modernizr.com のビルド ツールを使用し、必要なテストのみを選択します。
            AddBundle(bundles, new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"), transform);

            AddBundle(bundles, new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"), transform);

            AddBundle(bundles, new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.structure.css",
                      "~/Content/jquery-ui.theme.css"
                      ), transform);

            // 画面単位のバンドル
            
            // ログイン
            AddBundle(bundles, new StyleBundle("~/Content/login").Include(
                      "~/Content/Site-Login.css"), transform);

            // 所属選択
            AddBundle(bundles, new StyleBundle("~/Content/Site-Department").Include(
                      "~/Content/Site-Department.css"), transform);
            AddBundle(bundles, new ScriptBundle("~/bundles/app-Department").Include(
                      "~/Scripts/app-Department.js"), transform);

            // 出退勤時間
            AddBundle(bundles, new StyleBundle("~/Content/Site-AttendanceTime").Include(
                      "~/Content/Site-AttendanceTime.css"), transform);
            AddBundle(bundles, new ScriptBundle("~/bundles/app-AttendanceTime").Include(
                      "~/Scripts/app-AttendanceTime.js"), transform);


        }
        /// <summary>
        /// バンドル追加
        /// </summary>
        /// <param name="bundles">バンドル一覧</param>
        /// <param name="bundle">バンドル</param>
        /// <param name="transform">CacheBusting用</param>
        private static Bundle AddBundle(BundleCollection bundles, Bundle bundle, IBundleTransform transform)
        {
            bundle.Transforms.Add(transform);
            bundles.Add(bundle);
            return bundle;
        }
    }
}
