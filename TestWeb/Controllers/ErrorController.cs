using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Error.ViewModel;

namespace TestWeb.Controllers
{
    /// <summary>
    /// エラー画面コントローラー
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// エラー画面表示
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>エラー画面</returns>
        [HttpGet]
        // GET: Error
        public ActionResult Index(string errorMessage)
        {
            ErrorViewModel viewModel = new ErrorViewModel();
            viewModel.ErrorMessage = errorMessage;

            return View(viewModel);
        }
    }
}