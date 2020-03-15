using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Common;
using TestWeb.Models.Error.ViewModel;

namespace TestWeb.Controllers
{
    /// <summary>
    /// エラー画面コントローラー
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// セッションマネージャー
        /// </summary>
        private ISessionManager _SessionManager;

        /// <summary>
        /// コンストラクター(UnityMVC)
        /// </summary>
        /// <param name="sessionManager">セッションマネージャー</param>
        public ErrorController(ISessionManager sessionManager)
        {
            _SessionManager = sessionManager;
        }

        /// <summary>
        /// エラー画面表示
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>エラー画面</returns>
        [HttpGet]
        [AllowAnonymous]
        // GET: Error
        public ActionResult Index(string errorMessage)
        {
            ErrorViewModel viewModel = new ErrorViewModel();
            viewModel.ErrorMessage = errorMessage;

            _SessionManager.ClearUserInfoModel();

            return View(viewModel);
        }
    }
}