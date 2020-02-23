using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Common;
using TestWeb.Models.Login.ViewModel;
using TestWeb.Models.Department;
using TestWeb.Models.Department.ViewModel;
using TestWeb.Models.Department.InputModel;
using TestWeb.Models.Attributes;

namespace TestWeb.Controllers
{
    /// <summary>
    /// 所属選択画面
    /// </summary>
    public class DepartmentController : Controller
    {
        /// <summary>
        /// セッションマネージャー
        /// </summary>
        private ISessionManager _SessionManager;

        /// <summary>
        /// コントローラーのヘルパークラス
        /// </summary>
        private ControllerSupport _ControllerSupport;

        /// <summary>
        /// 所属サービス
        /// </summary>
        private IServiceProxy<IDepartmentService> _DepartmentService;


        /// <summary>
        /// コンストラクター(UnityMVC)
        /// </summary>
        public DepartmentController(ISessionManager sessionManager, IServiceProxy<IDepartmentService> departmentService)
        {
            _SessionManager = sessionManager;
            _DepartmentService = departmentService;

            this._ControllerSupport = new ControllerSupport(this);
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PositionAuthorize(Roles = "")]
        public ActionResult Index()
        {
            // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
            this._ControllerSupport.LoadMessageForRedirect();

            DepartmentViewModel viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _DepartmentService, m => m.Init(), _SessionManager.GetUserInfoModel(), string.Empty);

            // ViewModelを使ってLoginビューを表示する
            return View("Department", viewModel);
        }

        [HttpPost]
        [PositionAuthorize(Roles = "")]
        public ActionResult SelectDepartment(DepartmentSelectInputModel inputModel)
        {
            // 入力エラーがない場合
            if (this.ModelState.IsValid)
            {
                // 所属選択
                UserInfoModel userInfoModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _DepartmentService, m => m.SelectDepartment(inputModel), this._SessionManager.GetUserInfoModel(), string.Empty);

                if (_DepartmentService.ServiceMessage.IsValid)
                {
                    // 成功時の処理

                    // 所属選択後のユーザー情報をセッションに格納する
                    this._SessionManager.SetUserInfoModel(userInfoModel);

                    // Top画面にリダイレクトする
                    return RedirectToAction("Index", "Top", null);
                }
            }

            // エラー時
            // モデルステートを引き継ぎ、初期表示にリダイレクトする
            this._ControllerSupport.SaveMessageForRedirect();
            return RedirectToAction("Index");
        }
    }
}