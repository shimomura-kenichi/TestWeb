using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Login.ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }
        [DisplayName("パスワード")]
        public string Password { get; set; }
        [DisplayName("お知らせ")]
        public string Infomation { get; set; }
    }
}