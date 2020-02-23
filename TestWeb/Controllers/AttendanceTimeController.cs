using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.AttendanceTime;
using TestWeb.Models.AttendanceTime.InputModel;
using TestWeb.Models.AttendanceTime.ViewModel;
using TestWeb.Models.Attributes;
using TestWeb.Models.Common;
using TestWeb.Properties;

namespace TestWeb.Controllers
{
    /// <summary>
    /// 出退勤時間コントローラー
    /// </summary>
    public class AttendanceTimeController : Controller
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
        /// 出退勤時間サービス
        /// </summary>
        private IServiceProxy<IAttendanceTimeService> _AttendanceTimeService;

        /// <summary>
        /// コンストラクター(UnityMVC)
        /// </summary>
        public AttendanceTimeController(ISessionManager sessionManager, IServiceProxy<IAttendanceTimeService> attendanceTimeService)
        {
            _SessionManager = sessionManager;
            _AttendanceTimeService = attendanceTimeService;

            this._ControllerSupport = new ControllerSupport(this);
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult Index()
        {
            // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
            this._ControllerSupport.LoadMessageForRedirect();

            AttendanceTimeViewModel viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _AttendanceTimeService, m => m.Init(), _SessionManager.GetUserInfoModel(), string.Empty);

            return View("AttendanceTime", viewModel);
        }

        /// <summary>
        /// 明細書き換え（１件分）
        /// </summary>
        /// <param name="inputModel">キー情報</param>
        /// <returns>明細HTML</returns>
        [HttpGet]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult ReDrowDetail(AttendanceTimeKeyInputModel inputModel)
        {
            AttendanceTimeModel viewModel = null;
            if (ModelState.IsValid)
            {
                viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                    _AttendanceTimeService, m => m.GetDetail(inputModel), _SessionManager.GetUserInfoModel(), string.Empty);
            }
            else
            {
                viewModel = new AttendanceTimeModel();
                ModelState.AddModelError(string.Empty, Resources.MEP0005);
            }

            return PartialView("ListRow", viewModel);
        }

        /// <summary>
        /// 入力画面表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult DetailInputIndex(AttendanceTimeKeyInputModel inputModel)
        {
            AttendanceTimeModel viewModel = null;
            if (ModelState.IsValid)
            {
                ModelState.Clear();

                // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
                this._ControllerSupport.LoadMessageForRedirect();

                viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                    _AttendanceTimeService, m => m.InitDetail(inputModel), _SessionManager.GetUserInfoModel(), string.Empty);
            } else
            {
                viewModel = new AttendanceTimeModel();
                ModelState.AddModelError(string.Empty, Resources.MEP0005);
            }

            return PartialView("DetailInput", viewModel);
        }

        /// <summary>
        /// 出退勤登録
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult RegistDetail(AttendanceTimeInputModel inputModel)
        {
            // 入力エラーがない場合
            AttendanceTimeModel viewModel = null;
            if (ModelState.IsValid)
            {
                viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                    _AttendanceTimeService, m => m.UpdateDetail(inputModel), _SessionManager.GetUserInfoModel(), string.Empty);

            }
            if (ModelState.IsValid)
            {
                return Json(viewModel);
            } else
            {
                // エラー時はリダイレクトする
                this._ControllerSupport.SaveMessageForRedirect();
                AttendanceTimeKeyInputModel keyInputModel = new AttendanceTimeKeyInputModel();
                ModelUtil.CopyModelToModel(inputModel, keyInputModel);
                return RedirectToAction("DetailInputIndex", keyInputModel);
            }
        }
    }
}