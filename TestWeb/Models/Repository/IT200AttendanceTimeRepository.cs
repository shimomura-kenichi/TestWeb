using System.Collections.Generic;
using TestWeb.Models.AttendanceTime;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// 出退勤時間リポジトリインタフェース
    /// </summary>
    public interface IT200AttendanceTimeRepository
    {
        /// <summary>
        /// 出退勤時間リスト取得
        /// </summary>
        /// <param name="condition">抽出条件</param>
        /// <returns>出退勤リスト</returns>
        List<T200AttendanceTime> GetAttendanceTimeList(AttendanceTimeConditionModel condition);

        /// <summary>
        /// 主キーによる出退勤時間取得
        /// </summary>
        /// <param name="key">主キー</param>
        /// <returns>出退勤時間（該当なしはnull）</returns>
        T200AttendanceTime GetAttendanceTimeByKey(AttendanceTimeConditionModel key);

        /// <summary>
        /// 出退勤時間更新
        /// </summary>
        /// <param name="org">元データ</param>
        /// <param name="upd">更新データ</param>
        /// <returns>更新後データ</returns>
        T200AttendanceTime UpdateAttendanceTime(T200AttendanceTime org, T200AttendanceTime upd);

        /// <summary>
        /// 出退勤時間追加
        /// </summary>
        /// <param name="data">追加データ</param>
        /// <returns>更新後データ</returns>
        T200AttendanceTime InsertAttendanceTime(T200AttendanceTime data);

        /// <summary>
        /// 出退勤時間削除
        /// </summary>
        /// <param name="data">削除データ</param>
        /// <returns>削除後データ</returns>
        T200AttendanceTime DeleteAttendanceTime(T200AttendanceTime data);
    }
}