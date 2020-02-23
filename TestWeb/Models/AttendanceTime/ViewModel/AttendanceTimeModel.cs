using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models.AttendanceTime.ViewModel
{
    /// <summary>
    /// 出退勤時間モデル
    /// </summary>
    public class AttendanceTimeModel
    {
        ///<summary>ユーザーID</summary>
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }
        ///<summary>年</summary>
        [DisplayName("年")]
        public int WorkYear { get; set; }
        ///<summary>月</summary>
        [DisplayName("月")]
        public int WorkMonth { get; set; }
        ///<summary>日</summary>
        [DisplayName("日")]
        public int WorkDay { get; set; }

        ///<summary>勤務区分</summary>
        [DisplayName("勤務区分")]
        public string WorkKind { get; set; }
        ///<summary>開始</summary>
        [DisplayName("開始")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public Nullable<System.TimeSpan> StartTime { get; set; }
        ///<summary>終了</summary>
        [DisplayName("終了")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public Nullable<System.TimeSpan> EndTime { get; set; }
        ///<summary>休憩</summary>
        [DisplayName("休憩")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public Nullable<System.TimeSpan> RestTime { get; set; }
        ///<summary>備考</summary>
        [DisplayName("備考")]
        public string Remarks { get; set; }
        ///<summary>更新日時</summary>
        [DisplayName("更新日時")]
        public System.DateTime UpdateDttm { get; set; }
        ///<summary>更新ユーザーID</summary>
        [DisplayName("更新ユーザーID")]
        public string UpdateUserId { get; set; }
        ///<summary>行</summary>
        [DisplayName("行")]
        public int WorkNo { get; set; }
        ///<summary>部署コード</summary>
        [DisplayName("部署コード")]
        public string DepartmentCd { get; set; }
        ///<summary>週開始日</summary>
        [DisplayName("週開始日")]
        public System.DateTime WeekStartDate { get; set; }
        ///<summary>休日区分</summary>
        [DisplayName("休日区分")]
        public string HolidayKind { get; set; }

        ///<summary>勤務区分</summary>
        [DisplayName("勤務区分")]
        public string WorkKindName { get; set; }
        ///<summary>休日区分</summary>
        [DisplayName("休日区分")]
        public string HolidayKindName { get; set; }

        ///<summary>曜日</summary>
        [DisplayName("曜日")]
        public string WeekDay { get; set; }

        ///<summary>更新日時文字列</summary>
        public string UpdateDttmStr { get; set; }

        /// <summary>キー値Json文字列</summary>
        public string KeyValueJSon { get; set; }


    }

}