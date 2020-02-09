using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestWeb.Models.Common;

namespace TestWeb.Models.Repository
{
    /// <summary>
    /// ユーザー情報リポジトリ
    /// </summary>
    public class UserInfoRepository : IUserInfoRepository
    {
        /// <summary>
        /// 結果コード：排他エラー（更新時該当なし）
        /// </summary>
        public static readonly string RESULT_ERROR_EXCLUSIVE = "1";

        /// <summary>
        /// 結果コード：正常
        /// </summary>
        public static readonly string RESULT_NORMAL = "0";

        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private AttendanceDbEntities _DbContext;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="dbContext">DBコンテキスト</param>
        public UserInfoRepository(AttendanceDbEntities dbContext)
        {
            _DbContext = dbContext;
        }
        /// <summary>
        /// ユーザー情報取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>ユーザー情報（該当なし時はnullを返却する）</returns>
        public UserInfoModel GetUserInfo(string userId)
        {
            // M101とM102をUserIdで内部結合してUserInfoModel型に変換して返却する
            // FirstOrDefaultは該当データの先頭を返す。該当データがない場合はnullを返す。
            UserInfoModel userInfoModel = _DbContext.M101_USER
                .Join(_DbContext.M102_USER_AUTH, m101 => m101.UserId, m102 => m102.UserId, (m101, m102) => new { m101, m102 })
                .Where(m => m.m101.UserId == userId && m.m101.DeleteFlg == "0").Select(m => new UserInfoModel()
                {
                    UserId = m.m101.UserId,
                    Password = m.m102.Password,
                    UserName = m.m101.UserName,
                    LastLoginDttm = m.m102.LastLoginDttm
                }).FirstOrDefault();

            // Left Join
            //UserInfoModel userInfoModel2 = _DbContext.M101_USER.GroupJoin(_DbContext.M102_USER_AUTH
            //    , m101 => m101.UserId, m102 => m102.UserId, (m101, m102) => new { m101, m102 })
            //    .Where(m => m.m101.UserId == userId).SelectMany(m => m.m102.DefaultIfEmpty(), (m, m102) => new UserInfoModel()
            //    {
            //        UserId = m.m101.UserId,
            //        UserName = m.m101.UserName,
            //        LastLoginDttm = m102.LastLoginDttm
            //    }).FirstOrDefault();

            if (userInfoModel != null)
            {
                // 所属情報を取得する
                List<UserDepartmentModel> userDepartmentList = _DbContext.M103_USER_DEPARTMENT
                    .Where(m => m.UserId == userId && m.DeleteFlg == "0").Select(m => new UserDepartmentModel()
                    {
                        DepartmentCd = m.DepartmentCd,
                        PositionCd = m.PositionCd
                    }).ToList();
                userInfoModel.UserDepartmentList = userDepartmentList;

            }

            return userInfoModel;
        }

        /// <summary>
        /// 最終ログイン情報更新
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="lastLoginDttm">最終ログイン日時</param>
        /// <returns></returns>
        public string UpdateLastLoginInfo(string userId, DateTime lastLoginDttm)
        {
            // 該当データを取得（該当データを取得してからでないと更新できない）
            M102UserAuth userAuth = this._DbContext.M102_USER_AUTH.Where(m => m.UserId == userId 
                    && m.DeleteFlg == RepositoryConst.DELETE_FLG_NORMAL).FirstOrDefault();

            // 該当データが存在しない場合は、他のユーザーがすでに削除したと判定する
            if (userAuth == null)
            {
                return RESULT_ERROR_EXCLUSIVE;
            }

            // 更新設定（ここで更新されるのではなく、SaveChangesが呼び出された際に更新される）
            userAuth.LastLoginDttm = lastLoginDttm;

            return RESULT_NORMAL;
        }
    }
}