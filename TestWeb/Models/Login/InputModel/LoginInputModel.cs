using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Login.InputModel
{
    /// <summary>
    /// ログイン入力モデル
    /// </summary>
    public class LoginInputModel
    {
        [DisplayName("ユーザーID")]
        [Required()]
        [MaxLength(200)]
        public string UserId { get; set; }

        [DisplayName("パスワード")]
        [DataType(DataType.Password)]
        [Required()]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}