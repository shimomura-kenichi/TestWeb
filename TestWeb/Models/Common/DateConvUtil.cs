using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// 日付変換クラス
    /// </summary>
    public static class DateConvUtil
    {
        /// <summary>
        /// 時刻文字列(HH:MM)を時刻型に変換する
        /// </summary>
        /// <param name="timeStr">時刻文字列</param>
        /// <returns>時刻型</returns>
        public static TimeSpan? ConvertTimeStrHmToTimeSpan(string timeStr)
        {
            // 時刻整形
            string timeStrFormat = FormatTimeStr(timeStr);
            if (timeStrFormat == null)
            {
                return null;
            }
            return TimeSpan.Parse(timeStrFormat);
        }

        /// <summary>
        /// 時刻型を時刻文字列(HH:MM)に変換する
        /// </summary>
        /// <param name="time">時刻型</param>
        /// <returns>時刻文字列</returns>
        public static string ConvertTimeSpanToTimeStrHm(TimeSpan? time)
        {
            if (time == null)
            {
                return null;
            }

            return time.Value.ToString("hh\\:mm");
        }

        /// <summary>
        /// 日時型を日時ミリ秒文字列(yyy/MM/dd HH:mm:ss.fffff)に変換する
        /// </summary>
        /// <param name="dateTm">日時型</param>
        /// <returns>日時ミリ秒文字列</returns>
        public static string ConvertDateToStrDtmm(DateTime? dateTm)
        {
            if (dateTm == null)
            {
                return null;
            }
            return dateTm.Value.ToString("yyyy/MM/dd HH:mm:ss.fffffff");
        }

        /// <summary>
        /// 日時文字列を日時型に変換する
        /// </summary>
        /// <param name="dtmStr">日時文字列</param>
        /// <returns>日時型</returns>
        public static DateTime? ConvertDtmStrToDate(string dtmStr)
        {
            if (string.IsNullOrEmpty(dtmStr))
            {
                return null;
            }
            return DateTime.Parse(dtmStr);
        }

        /// <summary>
        /// 時刻整形
        /// 09:01 9:01 9:1 0901 901を09:01の形式にして返却する
        /// 9: は 9:00として返却する 
        /// 9 は 00:09として返却する
        /// 整形できない場合はnullを返却する
        /// </summary>
        /// <param name="timeStr">時刻文字列</param>
        /// <returns>整形後の時刻文字列</returns>
        public static string FormatTimeStr(string timeStr)
        {
            if (string.IsNullOrEmpty(timeStr))
            {
                return null;
            }

            // 桁を合わせる
            string valueStr = timeStr;
            if (valueStr.Contains(":"))
            {
                // :を含む場合
                string[] timeSplit = valueStr.Split(':');
                if (timeSplit[0].Length < 2)
                {
                    timeSplit[0] = timeSplit[0].PadLeft(2, '0');
                }
                if (timeSplit[1].Length < 2)
                {
                    timeSplit[1] = timeSplit[1].PadRight(2, '0');
                }
                valueStr = timeSplit[0] + timeSplit[1];
            }
            else
            {
                if (valueStr.Length < 4)
                {
                    valueStr = valueStr.PadLeft(4, '0');
                }
            }
            // 4桁より大きい場合はnullを返却する
            if (valueStr.Length > 4)
            {
                return null;
            }
            // 数字で、４桁で、時が00-48の範囲で、分が00-59の場合
            if (Regex.IsMatch(valueStr, @"^([0-3][0-9]|4[0-8])([0-5][0-9])$"))
            {
                return valueStr.Substring(0, 2) + ":" + valueStr.Substring(2, 2);
            } else
            {
                return null;
            }
        }
    }
}