using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Login.InputModel
{
    /// <summary>
    /// ログイン画面初期表示入力モデル
    /// </summary>
    public class LoginInitInputModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }
    }
}