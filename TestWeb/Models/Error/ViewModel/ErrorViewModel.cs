using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Error.ViewModel
{
    /// <summary>
    /// エラービューモデル
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        [DisplayName("エラーメッセージ")]
        public string ErrorMessage { get; set; }
    }
}