using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TestWeb.Models.Attributes;
using TestWeb.Models.Common;
using TestWeb.Models.Login.InputModel;
using TestWeb.Models.Login.ViewModel;
using TestWeb.Models.Repository;
using TestWeb.Properties;

namespace TestWeb.Models.Login
{
    /// <summary>
    /// ログインサービスクラス
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// ユーザー情報リポジトリ
        /// </summary>
        private IUserInfoRepository _UserInfoRepository;

        /// <summary>
        /// Dbコンテキスト
        /// </summary>
        private AttendanceDbEntities _DbContext;

        /// <summary>
        /// サービスメッセージ
        /// </summary>
        public ServiceMessage ServiceMessage { get;}

        /// <summary>
        /// ユーザー情報
        /// </summary>
        public UserInfoModel UserInfoModel { get; set; }

        /// <summary>
        /// DB保存用ファンクション
        /// </summary>
        public Func<bool> SaveChangeFunc { get;  set; }

        /// <summary>
        /// コンストラクター(UnityMvc)
        /// </summary>
        /// <param name="userInfoRepository">ユーザー情報リポジトリ</param>
        public LoginService(AttendanceDbEntities dbContext, IUserInfoRepository userInfoRepository)
        {
            this._DbContext = dbContext;
            this._UserInfoRepository = userInfoRepository;
            this.ServiceMessage = new ServiceMessage();
        }


        /// <summary>
        /// ログイン画面初期表示
        /// </summary>
        /// <param name="inputModel">入力モデル</param>
        /// <returns>Viewモデル</returns>
        public LoginViewModel Init(LoginInitInputModel inputModel)
        {
            LoginViewModel viewModel = new LoginViewModel()
            {
                UserId = inputModel.UserId,
                Password = "",
                Infomation = "お知らせはありません"
            };
            return viewModel;
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="inputModel">入力モデル</param>
        /// <returns>ユーザー情報</returns>
        [TransactionStart()]
        public UserInfoModel Login(LoginInputModel inputModel)
        {
            // ユーザー情報を取得する
            UserInfoModel userInfoModel = _UserInfoRepository.GetUserInfo(inputModel.UserId);

            // userInfoModelがnullの場合はログイン失敗
            if (userInfoModel == null)
            {
                this.ServiceMessage.AddErrorMessage(null, Resources.MEP0003);
                return null;
            }
            // ユーザー情報が取得できた場合はパスワードを比較する
            if (userInfoModel.Password != inputModel.Password)
            {
                // 不一致の場合はログイン失敗
                this.ServiceMessage.AddErrorMessage(null, Resources.MEP0003);
                return null;
            }
            // 一致していた場合は最終ログイン日時を更新
            DateTime lastLoginDttm = DateTime.Now;

            string result = _UserInfoRepository.UpdateLastLoginInfo(inputModel.UserId, lastLoginDttm);
            if (result != UserInfoRepository.RESULT_NORMAL)
            {
                this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, inputModel.UserId));
                return null;
            }

            // Db更新確定
            if (this.SaveChangeFunc())
            {
                // 成功時
                userInfoModel.LastLoginDttm = lastLoginDttm;
            }
            else
            {
                // エラー時
                // エラーメッセージセット
                this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, inputModel.UserId));
            }

            // 部署が1つしかない場合は選択部署としてuserInfoModelにセットする
            if (userInfoModel.UserDepartmentList.Count == 1)
            {
                userInfoModel.CurrentDepartmentCd = userInfoModel.UserDepartmentList[0].DepartmentCd;
                userInfoModel.CurrentPositionCd = userInfoModel.UserDepartmentList[0].PositionCd;
            }


            return userInfoModel;

        }

    }
}