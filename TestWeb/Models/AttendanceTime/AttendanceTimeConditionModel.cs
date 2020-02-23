using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.AttendanceTime
{
    /// <summary>
    /// 出退勤時間抽出条件モデル
    /// </summary>
    public class AttendanceTimeConditionModel
    {
        ///<summary>ユーザーID</summary>
        public string UserId { get; set; }
        ///<summary>年</summary>
        public int WorkYear { get; set; }
        ///<summary>月</summary>
        public int WorkMonth { get; set; }
        ///<summary>日</summary>
        public int WorkDay { get; set; }

        ///<summary>行</summary>
        public int WorkNo { get; set; }


        ///<summary>勤務区分</summary>
        public string WorkKind { get; set; }


    }
}