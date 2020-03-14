using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestWeb.Models.Attributes;
using TestWeb.Properties;

namespace TestWeb.Models.AttendanceTime.InputModel
{
    /// <summary>
    /// 出退勤時間入力モデル
    /// </summary>
    public class AttendanceTimeInputModel
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

        ///<summary>勤務区分</summary>
        [DisplayName("勤務区分")]
        [Required(ErrorMessageResourceName = "MEI0001", ErrorMessageResourceType = typeof(Resources))]
        public string WorkKind { get; set; }

        ///<summary>休日区分</summary>
        [DisplayName("休日区分")]
        public string HolidayKind { get; set; }

        ///<summary>開始</summary>
        [DisplayName("開始")]
        [TimeValidation(ErrorMessageResourceName = "MEI0011", ErrorMessageResourceType = typeof(Resources))]
        public string StartTime { get; set; }
        ///<summary>終了</summary>
        [DisplayName("終了")]
        [TimeValidation(ErrorMessageResourceName = "MEI0011", ErrorMessageResourceType = typeof(Resources))]
        public string EndTime { get; set; }
        ///<summary>休憩</summary>
        [DisplayName("休憩")]
        [TimeValidation(ErrorMessageResourceName = "MEI0011", ErrorMessageResourceType = typeof(Resources))]
        public string RestTime { get; set; }

        ///<summary>備考</summary>
        [DisplayName("備考")]
        public string Remarks { get; set; }

        ///<summary>更新ユーザーID</summary>
        public string UpdateUserId { get; set; }
        ///<summary>更新日時文字列</summary>
        public string UpdateDttmStr { get; set; }


        ///<summary>処理ボタン</summary>
        public string ProcBtn { get; set; }

    }
}