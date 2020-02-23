using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// セッション管理
    /// </summary>
    public class SessionManager : ISessionManager
    {
        /// <summary>
        /// ユーザー情報モデル取得
        /// </summary>
        /// <returns>ユーザー情報</returns>
        public UserInfoModel GetUserInfoModel()
        {
            HttpSessionState session = HttpContext.Current.Session;
            // ユーザー情報をセッションから取得する
            UserInfoModel userInfoModel = (UserInfoModel)session["UserInfoModel"];
            if (userInfoModel == null)
            {
                userInfoModel = new UserInfoModel();
            }
            return userInfoModel;
        }

        /// <summary>
        /// ユーザー情報モデル破棄
        /// </summary>
        public void ClearUserInfoModel()
        {
            HttpSessionState session = HttpContext.Current.Session;
            session.Remove("UserInfoModel");

            // 認証
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// ユーザー情報モデル設定
        /// </summary>
        /// <param name="session">セッション</param>
        /// <param name="userInfoModel">ユーザー情報</param>
        public void SetUserInfoModel(UserInfoModel userInfoModel)
        {
            HttpSessionState session = HttpContext.Current.Session;

            // ユーザー情報をセッションに格納する
            session.Add("UserInfoModel", userInfoModel);

            // 認証
            FormsAuthentication.SetAuthCookie(userInfoModel.UserId, false);

        }
    }
}