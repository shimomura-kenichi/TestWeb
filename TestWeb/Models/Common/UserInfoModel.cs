using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// ユーザー情報モデル
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 最終ログイン日時
        /// </summary>
        public Nullable<System.DateTime> LastLoginDttm { get; set; }

        /// <summary>
        /// 選択部署コード
        /// </summary>
        public string CurrentDepartmentCd { get; set; }

        /// <summary>
        /// 選択役職コード
        /// </summary>
        public string CurrentPositionCd { get; set; }

        /// <summary>
        /// 部署一覧
        /// </summary>
        public List<UserDepartmentModel> UserDepartmentList { get; set; }

    }
}