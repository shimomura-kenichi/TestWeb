using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// ユーザー所属モデル
    /// </summary>
    public class UserDepartmentModel
    {
        /// <summary>
        /// 部署コード
        /// </summary>
        public string DepartmentCd { get; set; }

        /// <summary>
        /// 役職コード
        /// </summary>
        public string PositionCd { get; set; }
    }
}