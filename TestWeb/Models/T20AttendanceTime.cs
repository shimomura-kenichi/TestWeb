//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    
    public partial class T20AttendanceTime
    {
        ///<summary>ユーザーID</summary>
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }
        public int WorkYear { get; set; }
        public int WorkMonth { get; set; }
        public int WorkDay { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<System.TimeSpan> RestTime { get; set; }
        public string Remarks { get; set; }
    }
}