﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Attributes;
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
        private IServiceProxy<ILoginService> _LoginService;

        /// <summary>
        /// コントローラーのヘルパークラス
        /// </summary>
        private ControllerSupport _ControllerSupport;

        /// <summary>
        /// セッションマネージャー
        /// </summary>
        private ISessionManager _SessionManager;

        /// <summary>
        /// コンストラクター(UnityMVC)
        /// </summary>
        /// <param name="sessionManager">セッションマネージャー</param>
        /// <param name="loginService">ログインサービス</param>
        public LoginController(ISessionManager sessionManager, IServiceProxy<ILoginService> loginService)
        {
            _SessionManager = sessionManager;
            _LoginService = loginService;

            this._ControllerSupport = new ControllerSupport(this);
        }

        /// <summary>
        /// 画面初期表示
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
            this._ControllerSupport.LoadMessageForRedirect();

            LoginInitInputModel inputModel = new LoginInitInputModel();

            // ユーザーIDをCookieから取得する
            HttpCookie userIdCookie = this.Request.Cookies.Get("UserId");
            if (userIdCookie != null)
            {
                inputModel.UserId = userIdCookie.Value;
            }
            else
            {
                inputModel.UserId = null;
            }
            // セッションをクリアする
            this.Session.Clear();

            // ViewModelを生成する

            LoginViewModel viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _LoginService, m => m.Init(inputModel), null, string.Empty);

            // ViewModelを使ってLoginビューを表示する
            return View("Login", viewModel);
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="inputModel">入力モデル</param>
        /// <returns>リダイレクト</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginInputModel inputModel)
        {
            // 入力エラーがない場合
            if (this.ModelState.IsValid)
            {
                // ログイン
                UserInfoModel userInfoModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _LoginService, m => m.Login(inputModel), null, string.Empty);

                if (_LoginService.ServiceMessage.IsValid)
                {
                    // ログイン成功時の処理


                    // UserIdをCookieに保存する
                    this.Response.AppendCookie(new HttpCookie("UserId", inputModel.UserId));

                    // ユーザー情報をセッションに格納する
                    this._SessionManager.SetUserInfoModel(userInfoModel);


                    // カレントの所属が未定の場合（所属が複数ある場合）
                    if (string.IsNullOrEmpty(userInfoModel.CurrentDepartmentCd))
                    {
                        // 所属選択画面に遷移する
                        return RedirectToAction("Index", "Department", null);
                    }
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