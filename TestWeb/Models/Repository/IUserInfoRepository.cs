using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.Models.Common;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// ユーザー情報リポジトリ
    /// </summary>
    public interface IUserInfoRepository
    {
        /// <summary>
        /// ユーザー情報取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>ユーザー情報（該当なし時はnullを返却する）</returns>
        UserInfoModel GetUserInfo(string userId);

        /// <summary>
        /// 最終ログイン情報更新
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="lastLoginDttm">最終ログイン日時</param>
        /// <returns></returns>
        string UpdateLastLoginInfo(string userId, DateTime lastLoginDttm);
    }
}
