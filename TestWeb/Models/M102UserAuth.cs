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
    
    public partial class M102UserAuth
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string DeleteFlg { get; set; }
        public string UpdateUserId { get; set; }
        public Nullable<System.DateTime> LastLoginDttm { get; set; }
        public System.DateTime CreateDttm { get; set; }
        public System.DateTime UpdateDttm { get; set; }
    }
}
