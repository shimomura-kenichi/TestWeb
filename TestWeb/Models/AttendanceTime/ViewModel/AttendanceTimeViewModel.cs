using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestWeb.Models.AttendanceTime.ViewModel
{
    /// <summary>
    /// 出退勤時間画面ビューモデル
    /// </summary>
    public class AttendanceTimeViewModel
    {
        ///<summary>ユーザーID</summary>
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }

        ///<summary>ユーザー名</summary>
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }

        ///<summary>年</summary>
        [DisplayName("年")]
        public int WorkYear { get; set; }
        ///<summary>月</summary>
        [DisplayName("月")]
        public int WorkMonth { get; set; }

        /// <summary>
        /// 出退勤時間リスト
        /// </summary>
        public List<AttendanceTimeModel> AttendanceTimeList { get; set; }
    }
}