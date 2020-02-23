using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.Models.AttendanceTime;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// V200出退勤時間ビューリポジトリ
    /// </summary>
    public interface IV200AttendanceTimeRepository
    {

        /// <summary>
        /// 出退勤時間リスト取得
        /// </summary>
        /// <param name="condition">抽出条件</param>
        /// <returns>出退勤リスト</returns>
        List<V200AttendanceTime> GetAttendanceTimeList(AttendanceTimeConditionModel condition);

        /// <summary>
        /// 主キーによる出退勤時間取得
        /// </summary>
        /// <param name="key">主キー</param>
        /// <returns>出退勤時間（該当なしはnull）</returns>
        V200AttendanceTime GetAttendanceTimeByKey(AttendanceTimeConditionModel key);
    }
}
