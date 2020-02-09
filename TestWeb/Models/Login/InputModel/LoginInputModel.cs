using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestWeb.Properties;

namespace TestWeb.Models.Login.InputModel
{
    /// <summary>
    /// ログイン入力モデル
    /// </summary>
    public class LoginInputModel
    {
        [DisplayName("ユーザーID")]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(200, ErrorMessageResourceName = "MEI0002", ErrorMessageResourceType = typeof(Resources))]
        public string UserId { get; set; }

        [DisplayName("パスワード")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(50, ErrorMessageResourceName = "MEI0002", ErrorMessageResourceType = typeof(Resources))]
        public string Password { get; set; }
    }
}