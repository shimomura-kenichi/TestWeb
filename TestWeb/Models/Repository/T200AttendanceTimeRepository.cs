using System.Collections.Generic;
using System.Linq;
using TestWeb.Models.AttendanceTime;
using TestWeb.Models.AttendanceTime.InputModel;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// 出退勤時間リポジトリ
    /// </summary>
    public class T200AttendanceTimeRepository : IT200AttendanceTimeRepository
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private AttendanceDbEntities _DbContext;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="dbContext">DBコンテキスト</param>
        public T200AttendanceTimeRepository(AttendanceDbEntities dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// 出退勤時間リスト取得
        /// </summary>
        /// <param name="condition">抽出条件</param>
        /// <returns>出退勤リスト</returns>
        public List<T200AttendanceTime> GetAttendanceTimeList(AttendanceTimeConditionModel condition)
        {
            IQueryable<T200AttendanceTime> query = this._DbContext.T200_ATTENDANCE_TIME.Where(
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
            List<T200AttendanceTime> resultList = query.ToList();

            return resultList;
        }

        /// <summary>
        /// 主キーによる出退勤時間取得
        /// </summary>
        /// <param name="key">主キー</param>
        /// <returns>出退勤時間（該当なしはnull）</returns>
        public T200AttendanceTime GetAttendanceTimeByKey(AttendanceTimeConditionModel key)
        {
            IQueryable<T200AttendanceTime> query = this._DbContext.T200_ATTENDANCE_TIME.Where(
                m => m.UserId == key.UserId && m.WorkYear == key.WorkYear && m.WorkMonth == key.WorkMonth
                && m.WorkDay == key.WorkDay && m.WorkNo == key.WorkNo);

            T200AttendanceTime result = query.Select(m => m).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 出退勤時間更新
        /// </summary>
        /// <param name="org">元データ</param>
        /// <param name="upd">更新データ</param>
        /// <returns>更新後データ</returns>
        public T200AttendanceTime UpdateAttendanceTime(T200AttendanceTime org, T200AttendanceTime upd)
        {
            org.WorkKind = upd.WorkKind;
            org.StartTime = upd.StartTime;
            org.EndTime = upd.EndTime;
            org.RestTime = upd.RestTime;
            org.HolidayKind = upd.HolidayKind;
            org.Remarks = upd.Remarks;
            org.UpdateDttm = upd.UpdateDttm;
            org.UpdateUserId = upd.UpdateUserId;

            return org;
        }

        /// <summary>
        /// 出退勤時間追加
        /// </summary>
        /// <param name="data">追加データ</param>
        /// <returns>更新後データ</returns>
        public T200AttendanceTime InsertAttendanceTime(T200AttendanceTime data)
        {
            this._DbContext.T200_ATTENDANCE_TIME.Add(data);

            return data;
        }

        /// <summary>
        /// 出退勤時間削除
        /// </summary>
        /// <param name="data">削除データ</param>
        /// <returns>削除後データ</returns>
        public T200AttendanceTime DeleteAttendanceTime(T200AttendanceTime data)
        {
            this._DbContext.T200_ATTENDANCE_TIME.Remove(data);

            return data;
        }

    }
}