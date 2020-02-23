using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestWeb.Models.AttendanceTime;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// V200出退勤時間ビューリポジトリ
    /// </summary>
    public class V200AttendanceTimeRepository : IV200AttendanceTimeRepository
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private AttendanceDbEntities _DbContext;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="dbContext">DBコンテキスト</param>
        public V200AttendanceTimeRepository(AttendanceDbEntities dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// 出退勤時間リスト取得
        /// </summary>
        /// <param name="condition">抽出条件</param>
        /// <returns>出退勤リスト</returns>
        public List<V200AttendanceTime> GetAttendanceTimeList(AttendanceTimeConditionModel condition)
        {
            IQueryable<V200AttendanceTime> query = this._DbContext.V200_ATTENDANCE_TIME.Where(
                m => m.UserId == condition.UserId && m.WorkYear == condition.WorkYear && m.WorkMonth == condition.WorkMonth);

            if (condition.WorkDay != 0)
            {
                query = query.Where(m => m.WorkDay == condition.WorkDay);
            }
            if (!string.IsNullOrEmpty(condition.WorkKind))
            {
                query = query.Where(m => m.WorkKind == condition.WorkKind);
            }
            query = query.Select(m => m).OrderBy(m => m.WorkDay).ThenBy(m => m.StartTime).ThenBy(m => m.WorkNo);
            List<V200AttendanceTime> resultList = query.ToList();

            return resultList;
        }

        /// <summary>
        /// 主キーによる出退勤時間取得
        /// </summary>
        /// <param name="key">主キー</param>
        /// <returns>出退勤時間（該当なしはnull）</returns>
        public V200AttendanceTime GetAttendanceTimeByKey(AttendanceTimeConditionModel key)
        {
            IQueryable<V200AttendanceTime> query = this._DbContext.V200_ATTENDANCE_TIME.Where(
                m => m.UserId == key.UserId && m.WorkYear == key.WorkYear && m.WorkMonth == key.WorkMonth
                && m.WorkDay == key.WorkDay && m.WorkNo == key.WorkNo);

            V200AttendanceTime result = query.Select(m => m).FirstOrDefault();

            return result;
        }

    }
}