using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// コードマスターリポジトリー
    /// </summary>
    public class M201CodeMstRepository : IM201CodeMstRepository
    {
        /// <summary>
        /// コード区分：所属
        /// </summary>
        public const string CD_KIND_DEPARTMENT = "01";
        /// <summary>
        /// コード区分：役職
        /// </summary>
        public const string CD_KIND_POSITION = "02";
        /// <summary>
        /// コード区分：勤務区分
        /// </summary>
        public const string CD_KIND_WORK_KIND = "03";
        /// <summary>
        /// コード区分：休日区分
        /// </summary>
        public const string CD_KIND_HOLIDAY_KIND = "04";

        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private AttendanceDbEntities _DbContext;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="dbContext">DBコンテキスト</param>
        public M201CodeMstRepository(AttendanceDbEntities dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// コードマスターリスト返却
        /// </summary>
        /// <param name="cdKind">コード区分</param>
        /// <returns>結果リスト</returns>
        public List<M201CodeMst> GetCodeMstList(string cdKind)
        {
            List<M201CodeMst> resultList = _DbContext.M201_CODE_MST.Where(m => m.CdKind == cdKind).Select(m => m).OrderBy(m => m.Cd).ToList();
            return resultList;
        }
    }
}