using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.Models.Common;
using TestWeb.Models.Department.InputModel;
using TestWeb.Models.Department.ViewModel;

namespace TestWeb.Models.Department
{
    /// <summary>
    /// 所属サービスインタフェース
    /// </summary>
    public interface IDepartmentService :IService
    {
        /// <summary>
        /// 所属選択画面初期表示
        /// </summary>
        /// <returns>ビューモデル</returns>
        DepartmentViewModel Init();

        /// <summary>
        /// 所属選択
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>所属選択後のユーザー情報</returns>
        UserInfoModel SelectDepartment(DepartmentSelectInputModel inputModel);
    }
}
