using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Services.Description;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// サービスメッセージ
    /// </summary>
    public class ServiceMessage
    {
        /// <summary>
        /// 正常時メッセージ
        /// </summary>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// エラー時メッセージ
        /// </summary>
        public Dictionary<string, List<string>> ErrorMessage { get; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ServiceMessage()
        {
            this.ErrorMessage = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// エラーメッセージ追加
        /// </summary>
        /// <param name="fieldKey">エラーが発生した項目(全体にかかるメッセージの場合はnull）</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        public void AddErrorMessage(string fieldKey, string errorMessage)
        {
            string fieldKeyWork = fieldKey;
            if (fieldKeyWork == null)
            {
                fieldKeyWork = string.Empty;
            }
            // すでにfieldKeyが追加されている場合はリストにエラーメッセージを追加する
            if (this.ErrorMessage.ContainsKey(fieldKeyWork))
            {
                this.ErrorMessage[fieldKeyWork].Add(errorMessage);
            } else
            {
                // fieldKeyが追加されていない場合はリストを作成してメッセージを追加する
                var errorList = new List<string>();
                errorList.Add(errorMessage);
                this.ErrorMessage.Add(fieldKeyWork, errorList);
            }
        }

        /// <summary>
        /// 処理結果返却
        /// </summary>
        /// <returns>true:正常 false:エラー</returns>
        public bool IsValid
        {
            get {
                // エラーがなければ正常
                if (this.ErrorMessage.Keys.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}