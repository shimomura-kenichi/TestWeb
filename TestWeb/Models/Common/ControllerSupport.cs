using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebGrease;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// コントローラー用のヘルパークラス
    /// </summary>
    public class ControllerSupport
    {
        /// <summary>
        /// TempDataキー：ModelState
        /// </summary>
        private static readonly string TEMP_KEY_MODEL_STATE = "TEMP_KEY_MODEL_STATE";

        /// <summary>
        /// TempDataキー：ViewData
        /// </summary>
        private static readonly string TEMP_KEY_VIEW_DATA = "TEMP_KEY_VIEW_DATA";

        /// <summary>
        /// コントローラー
        /// </summary>
        private Controller _Controller;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="controller">コントローラー</param>
        public ControllerSupport(Controller controller)
        {
            this._Controller = controller;
        }
        /// <summary>
        /// サービスの実行および返却メッセージ保存
        /// </summary>
        /// <typeparam name="TService">サービスクラス</typeparam>
        /// <typeparam name="TResult">返却モデルクラス</typeparam>
        /// <param name="serviceProxy">サービスプロキシ</param>
        /// <param name="expression">実行メソッド</param>
        /// <param name="userInfoModel">ユーザー情報</param>
        /// <param name="messageKey">正常メッセージや項目に結びつかないエラーメッセージのフィールド名を表すキー</param>
        /// <returns>サービスメソッドの戻り値</returns>
        /// <remarks>
        /// messageKeyは、Viewで使用する。ModelStateのエラー情報は、通常はキーが""のものが項目に結びつかないエラーメッセージとして処理されるが、
        /// 画面によっては、複数処理があり、処理によってメッセージ出力領域を変更する場合がある。
        /// そのような場合に対応できるように、Viewで指定されたメッセージキーのメッセージだけを出力するようなヘルパーを作成する。
        /// その際に指定するメッセージキーがmessageKeyに該当する。
        /// </remarks>
        public TResult InvokeServiceAndSetMessage<TService, TResult>(IServiceProxy<TService> serviceProxy, Expression<Func<TService, TResult>> expression
            , UserInfoModel userInfoModel, string messageKey)
            where TService : IService
        {
            string messageKeyWork = messageKey;
            if (messageKeyWork == null)
            {
                messageKeyWork = string.Empty;
            }

            // サービス実行
            TResult result = serviceProxy.InvokeService(expression, userInfoModel);

            // メッセージを格納する

            // サービスのエラーメッセージをModelStateにコピーする
            foreach (KeyValuePair<string, List<string>> data in serviceProxy.ServiceMessage.ErrorMessage)
            {
                foreach (string msg in data.Value)
                {
                    string messageKeyReplace = data.Key;
                    if (messageKeyReplace == string.Empty)
                    {
                        messageKeyReplace = messageKeyWork;
                    }
                    this._Controller.ModelState.AddModelError(messageKeyReplace, msg);
                }
            }

            // 正常メッセージをViewDataにコピーする
            this._Controller.ViewData[messageKeyWork] = serviceProxy.ServiceMessage.SuccessMessage;

            return result;
        }

        /// <summary>
        /// リダイレクトのためのメッセージおよびModelState保存
        /// </summary>
        public void SaveMessageForRedirect()
        {
            this._Controller.TempData.Add(TEMP_KEY_MODEL_STATE, this._Controller.ModelState);
            this._Controller.TempData.Add(TEMP_KEY_VIEW_DATA, this._Controller.ViewData);
        }

        /// <summary>
        /// リダイレクトのためのメッセージおよびModelState復帰
        /// </summary>
        public void LoadMessageForRedirect()
        {

            // リダイレクトされた場合でModelStateが引き渡された場合はModelStateをマージする。
            ModelStateDictionary modelState = (ModelStateDictionary)this._Controller.TempData[TEMP_KEY_MODEL_STATE];
            if (modelState != null)
            {
                this._Controller.ModelState.Merge(modelState);
            }
            ViewDataDictionary viewData = (ViewDataDictionary)this._Controller.TempData[TEMP_KEY_VIEW_DATA];
            if (viewData != null)
            {
                foreach (KeyValuePair<string, object> keyVal in viewData)
                {
                    this._Controller.ViewData.Add(keyVal.Key, keyVal.Value);
                }
            }
        }
    }
}
