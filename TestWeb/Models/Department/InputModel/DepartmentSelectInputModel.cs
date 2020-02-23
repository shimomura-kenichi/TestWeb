using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using TestWeb.Properties;

namespace TestWeb.Models.Department.InputModel
{
    /// <summary>
    /// 所属選択入力モデル
    /// </summary>
    public class DepartmentSelectInputModel
    {
        /// <summary>
        /// 選択No.
        /// </summary>
        [DisplayName("所属")]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [RegularExpression("[0-9]+",
                            ErrorMessageResourceName = "MEI0009", ErrorMessageResourceType = typeof(Resources))]
        public string SelectNumber { get; set; }
    }
}