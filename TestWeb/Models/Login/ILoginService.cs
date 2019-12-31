using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.Models.Common;
using TestWeb.Models.Login.InputModel;
using TestWeb.Models.Login.ViewModel;

namespace TestWeb.Models.Login
{
    /// <summary>
    /// ログインサービスインタフェース
    /// </summary>
    public interface ILoginService : IService
    {
        /// <summary>
        /// ログイン画面初期表示
        /// </summary>
        /// <param name="inputModel">入力モデル</param>
        /// <returns>Viewモデル</returns>
        LoginViewModel Init(LoginInitInputModel inputModel);

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="inputModel">入力モデル</param>
        /// <returns>ユーザー情報</returns>
        UserInfoModel Login(LoginInputModel inputModel);
    }
}
