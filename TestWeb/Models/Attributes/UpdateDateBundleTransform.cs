using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TestWeb.Models.Attributes
{
    /// <summary>
    /// 更新日時付与 バンドルトランスフォーム
    /// </summary>
    public class UpdateDateBundleTransform : IBundleTransform
    {
        /// <summary>
        /// 更新日時付与
        /// </summary>
        /// <param name="context">コンテキスト</param>
        /// <param name="response">レスポンス</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            foreach (BundleFile file in response.Files)
            {
                // Cache Bustingとして、ファイル名にバージョンを付与する
                string version = System.IO.File.GetLastWriteTime(System.Web.Hosting.HostingEnvironment.MapPath(file.IncludedVirtualPath)).ToString("yyyyMMddHHmmssfff");
                file.IncludedVirtualPath = string.Concat(file.IncludedVirtualPath, "?v=", version);
            }
        }
    }
}