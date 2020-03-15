using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Common
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 休日区分:通常
        /// </summary>
        public const string HOLIDAY_KIND_NORMAL = "01";


        /// <summary>
        /// 削除区分:通常
        /// </summary>
        public const string DELETE_FLG_NORMAL = "0";
        /// <summary>
        /// 削除区分:削除
        /// </summary>
        public const string DELETE_FLG_DELETE = "1";

        /// <summary>
        /// 役職コード:SM
        /// </summary>
        public const string POSITION_CD_SM = "01";
        /// <summary>
        /// 役職コード:AM
        /// </summary>
        public const string POSITION_CD_AM = "02";
        /// <summary>
        /// 役職コード:一般
        /// </summary>
        public const string POSITION_CD_MN = "03";

        /// <summary>
        /// ロール:全員
        /// </summary>
        public const string ROLE_ALL = "01,02,03,04,05";
        /// <summary>
        /// ロール:社員
        /// </summary>
        public const string ROLE_EMPLOYEE = "01,02,03,04";
        /// <summary>
        /// ロール:管理職
        /// </summary>
        public const string ROLE_MANAGER = "01,02";
        /// <summary>
        /// ロール:管理者
        /// </summary>
        public const string ROLE_ADMIN = "05";
        /// <summary>
        /// ロール:経理
        /// </summary>
        public const string ROLE_ACT = "04";

        /// <summary>
        /// 週漢字
        /// </summary>
        public static readonly string[] WEEK_DAY_KANJI = { "日", "月", "火", "水", "木", "金", "土" };

    }
}