using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TestWeb.Models.Common;

namespace TestWeb.Models.Attributes
{
    /// <summary>
    /// 時刻形式チェック
    /// </summary>
    public class TimeValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// 時刻形式チェック
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                // nullの場合はチェックしない
                return true;
            }
            
            // 時刻整形する
            if (DateConvUtil.FormatTimeStr((string)value) != null)
            {
                return true;
            }

            return false;
        }
    }
}