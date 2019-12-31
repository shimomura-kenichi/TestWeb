using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Common;
using TestWeb.Models.Login;
using TestWeb.Models.Login.InputModel;
using TestWeb.Models.Login.ViewModel;

namespace TestWeb.Controllers
{
    /// <summary>
    /// ログイン画面コントローラー
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// ログインサービス
        /// </summary>
        private LoginService _LoginService;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LoginController()
        {
            _LoginService = new LoginService();
        }

        /// <summary>
        /// 画面初期表示
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
            ModelStateDictionary modelState = (ModelStateDictionary)this.TempData["ModelState"];
            if (modelState != null)
            {
                this.ModelState.Merge(modelState);
            }

            LoginInitInputModel inputModel = new LoginInitInputModel();

            // ユーザーIDをCookieから取得する
            HttpCookie userIdCookie = this.Request.Cookies.Get("UserId");
            if (userIdCookie != null)
            {
                inputModel.UserId = userIdCookie.Value;
            } else
            {
                inputModel.UserId = null;
            }
            // セッションをクリアする
            this.Session.Clear();

            // ViewModelを生成する

            LoginViewModel viewModel = _LoginService.Init(inputModel);

            // ViewModelを使ってLoginビューを表示する
            return View("Login", viewModel);
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="inputModel">入力モデル</param>
        /// <returns>リダイレクト</returns>
        public ActionResult Login(LoginInputModel inputModel)
        {
            // 入力エラーがあった場合
            if (!this.ModelState.IsValid)
            {
                // 初期表示にリダイレクト
                // リダイレクトするとModelStateが失われ、入力内容がViewに反映されなくなるのでModelStateを退避する。
                this.TempData.Add("ModelState", this.ModelState);
                return RedirectToAction("Index");
            }

            // ログイン
            UserInfoModel userInfoModel = _LoginService.Login(inputModel);
            if (userInfoModel == null)
            {
                // エラー時は初期表示にリダイレクトする
                return RedirectToAction("Index");
            }
            // ログイン成功時の処理

            // UserIdをCookieに保存する
            this.Response.AppendCookie(new HttpCookie("UserId", inputModel.UserId));

            // ユーザー情報をセッションに格納する
            this.Session.Add("UserInfoModel", userInfoModel);

            // Top画面にリダイレクトする
            return RedirectToAction("Index", "Top", null);

        }
    }
}