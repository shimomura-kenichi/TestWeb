using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestWeb.Properties;

namespace TestWeb.Models.AttendanceTime.InputModel
{
    /// <summary>
    /// 出勤時間キーモデル
    /// </summary>
    public class AttendanceTimeKeyInputModel
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
        ///<summary>日</summary>
        [DisplayName("日")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceName = "MEI0010", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(2, ErrorMessageResourceName = "MEI0002", ErrorMessageResourceType = typeof(Resources))]
        public string WorkDay { get; set; }
        ///<summary>行</summary>
        [DisplayName("行")]
        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceName = "MEI0010", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        public string WorkNo { get; set; }
    }
}