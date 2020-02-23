using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.AttendanceTime.ViewModel
{
    /// <summary>
    /// 出退勤時間画面ビューモデル
    /// </summary>
    public class AttendanceTimeViewModel
    {

        /// <summary>
        /// 出退勤時間リスト
        /// </summary>
        public List<AttendanceTimeModel> AttendanceTimeList { get; set; }
    }
}