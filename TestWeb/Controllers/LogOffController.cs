using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Common;

namespace TestWeb.Controllers
{
    /// <summary>
    /// ログオフ画面コントローラー
    /// </summary>
    public class LogOffController : Controller
    {
        /// <summary>
        /// セッションマネージャー
        /// </summary>
        private ISessionManager _SessionManager;

        /// <summary>
        /// コンストラクター(UnityMVC)
        /// </summary>
        /// <param name="sessionManager">セッションマネージャー</param>
        public LogOffController(ISessionManager sessionManager)
        {
            _SessionManager = sessionManager;
        }

        /// <summary>
        /// ログオフ画面
        /// </summary>
        /// <returns>ビュー</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            _SessionManager.ClearUserInfoModel();
            return View();
        }
    }
}