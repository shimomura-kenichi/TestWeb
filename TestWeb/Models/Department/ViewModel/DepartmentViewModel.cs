using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestWeb.Models.Common;

namespace TestWeb.Models.Department.ViewModel
{
    /// <summary>
    /// 所属選択画面ビューモデル
    /// </summary>
    public class DepartmentViewModel
    {
        /// <summary>
        /// 所属リスト
        /// </summary>
        public List<UserDepartmentModel> UserDepartmentList { get; set; }
    }
}