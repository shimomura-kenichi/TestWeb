using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        /// 月選択
        /// </summary>
        /// <param name="inputModel">年月選択</param>
        /// <returns>出退勤時間ビュー</returns>
        [HttpGet]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult SelectMonth(AttendanceTimeSelectMonthInputModel inputModel)
        {
            AttendanceTimeViewModel viewModel = null;
            if (!ModelState.IsValid)
            {
                // 通常の方法では入力エラーにならないため例外を発生させる
                throw new ArgumentException();
            }

            viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
            _AttendanceTimeService, m => m.SelectMonth(inputModel), _SessionManager.GetUserInfoModel(), string.Empty);

            if (ModelState.IsValid)
            {
                ModelState.Clear();
            } else
            {
                // 通常の方法では入力エラーにならないため例外を発生させる
                throw new ArgumentException();
            }

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
            if (!ModelState.IsValid)
            {
                // 通常の方法では入力エラーにならないため例外を発生させる
                throw new ArgumentException();
            }
            viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _AttendanceTimeService, m => m.GetDetail(inputModel), _SessionManager.GetUserInfoModel(), string.Empty);
            ModelState.Clear();

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
            AttendanceTimeDetailViewModel viewModel = null;
            if (!ModelState.IsValid)
            {
                // 通常の方法では入力エラーにならないため例外を発生させる
                throw new ArgumentException();
            }
            ModelState.Clear();

            // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
            this._ControllerSupport.LoadMessageForRedirect();

            viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                _AttendanceTimeService, m => m.InitDetail(inputModel), _SessionManager.GetUserInfoModel(), string.Empty);

            return PartialView("DetailInput", viewModel);
        }

        /// <summary>
        /// 出退勤登録
        /// </summary>
        /// <param name="detailModel">入力モデル</param>
        /// <returns></returns>
        [HttpPost]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult RegistDetail(AttendanceTimeInputModel detailModel)
        {
            // 入力エラーがない場合
            AttendanceTimeModel viewModel = null;
            if (ModelState.IsValid)
            {
                viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                    _AttendanceTimeService, m => m.UpdateDetail(detailModel), _SessionManager.GetUserInfoModel(), string.Empty);

                viewModel.ProcBtn = detailModel.ProcBtn;

            }
            if (ModelState.IsValid)
            {
                return Json(viewModel);
            } else
            {
                // エラー時はリダイレクトする
                this._ControllerSupport.SaveMessageForRedirect();
                AttendanceTimeKeyInputModel keyInputModel = new AttendanceTimeKeyInputModel();
                ModelUtil.CopyModelToModel(detailModel, keyInputModel);
                return RedirectToAction("DetailInputIndex", keyInputModel);
            }
        }

        /// <summary>
        /// 空行追加
        /// </summary>
        /// <param name="keyModel">入力モデル</param>
        /// <returns></returns>
        [HttpPost]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult AddEmptyDetail(AttendanceTimeKeyInputModel keyModel)
        {
            // 入力エラーがない場合
            AttendanceTimeModel viewModel = null;
            if (ModelState.IsValid)
            {
                viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                    _AttendanceTimeService, m => m.AddEmptyDetail(keyModel), _SessionManager.GetUserInfoModel(), string.Empty);
            }
            if (ModelState.IsValid)
            {
                return Json(viewModel);
            }
            else
            {
                // エラー時はメッセージを返却する
                return Json(new { ErrorMessage = "登録に失敗しました。" });
            }
        }

        /// <summary>
        /// 行削除
        /// </summary>
        /// <param name="keyModel">入力モデル</param>
        /// <returns></returns>
        [HttpPost]
        [PositionAuthorize(Roles = Constants.ROLE_EMPLOYEE)]
        public ActionResult DeleteDetail(AttendanceTimeKeyInputModel keyModel)
        {
            // 入力エラーがない場合
            AttendanceTimeModel viewModel = null;
            if (ModelState.IsValid)
            {
                viewModel = this._ControllerSupport.InvokeServiceAndSetMessage(
                    _AttendanceTimeService, m => m.DeleteDetail(keyModel), _SessionManager.GetUserInfoModel(), string.Empty);
            }
            if (ModelState.IsValid)
            {
                return Json(viewModel);
            }
            else
            {
                // エラー時は先頭のメッセージを返却する
                string errorMessage = "削除は失敗しました。";
                foreach (KeyValuePair<string, ModelState> keyVal in ModelState.ToList())
                {
                    if (keyVal.Value.Errors.Count > 0)
                    {
                        errorMessage = keyVal.Value.Errors[0].ErrorMessage;
                        break;
                    }
                }
                return Json(new { ErrorMessage = errorMessage });
            }
        }

    }
}