﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Login.ViewModel
{
    /// <summary>
    /// ログインビューモデル
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [DisplayName("パスワード")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// お知らせ
        /// </summary>
        [DisplayName("お知らせ")]
        public string Infomation { get; set; }
    }
}