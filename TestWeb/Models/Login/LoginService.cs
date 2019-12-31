using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
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
        private UserInfoRepository _UserInfoRepository;

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
        /// コンストラクター
        /// </summary>
        public LoginService()
        {
            _DbContext = new AttendanceDbEntities();
            _UserInfoRepository = new UserInfoRepository(_DbContext);
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
            try
            {
                _DbContext.SaveChanges();
                userInfoModel.LastLoginDttm = lastLoginDttm;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 整合性例外が発生した場合は排他エラー発生
                foreach (var entry in ex.Entries)
                {
                    // DbContextに加えた変更をすべて元に戻す
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                // エラーメッセージセット
                this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEP0004, inputModel.UserId));
                return null;
            }

            return userInfoModel;

        }

    }
}