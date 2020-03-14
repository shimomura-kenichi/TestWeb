using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using TestWeb.Properties;

namespace TestWeb.Models.AttendanceTime.InputModel
{
    /// <summary>
    /// 出退勤時間年月変更入力モデル
    /// </summary>
    public class AttendanceTimeSelectMonthInputModel
    {
        ///<summary>ユーザーID</summary>
        [DisplayName("ユーザーID")]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(200, ErrorMessageResourceName = "MEI0002", ErrorMessageResourceType = typeof(Resources))]
        public string UserId { get; set; }
        ///<summary>年</summary>
        [DisplayName("年")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceName = "MEI0010", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(4, ErrorMessageResourceName = "MEI0002", ErrorMessageResourceType = typeof(Resources))]
        public string WorkYear { get; set; }
        ///<summary>月</summary>
        [DisplayName("月")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceName = "MEI0010", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(2, ErrorMessageResourceName = "MEI0002", ErrorMessageResourceType = typeof(Resources))]
        public string WorkMonth { get; set; }

        ///<summary>選択ボタン</summary>
        public string MonthBtn { get; set; }

    }
}