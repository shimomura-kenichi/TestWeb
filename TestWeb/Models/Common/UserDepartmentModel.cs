using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("部署コード")]
        public string DepartmentCd { get; set; }

        /// <summary>
        /// 部署名
        /// </summary>
        [DisplayName("部署名")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 役職コード
        /// </summary>
        [DisplayName("役職コード")]
        public string PositionCd { get; set; }

        /// <summary>
        /// 役職名
        /// </summary>
        [DisplayName("役職名")]
        public string PositionName { get; set; }

    }
}