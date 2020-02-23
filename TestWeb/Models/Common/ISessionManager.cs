using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// セッション管理インタフェース
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// ユーザー情報モデル取得
        /// </summary>
        /// <returns>ユーザー情報</returns>
        UserInfoModel GetUserInfoModel();

        /// <summary>
        /// ユーザー情報モデル設定
        /// </summary>
        /// <param name="session">セッション</param>
        /// <param name="userInfoModel">ユーザー情報</param>
        void SetUserInfoModel(UserInfoModel userInfoModel);

        /// <summary>
        /// ユーザー情報モデル破棄
        /// </summary>
        void ClearUserInfoModel();

    }
}
