﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestWeb.Models.Common;
using TestWeb.Models.Login.InputModel;
using TestWeb.Models.Login.ViewModel;
using TestWeb.Models.Repository;

namespace TestWeb.Models.Login
{
    /// <summary>
    /// ログインサービスクラス
    /// </summary>
    public class LoginService
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
        /// コンストラクター
        /// </summary>
        public LoginService()
        {
            _DbContext = new AttendanceDbEntities();
            _UserInfoRepository = new UserInfoRepository(_DbContext);
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
                return null;
            }
            // ユーザー情報が取得できた場合はパスワードを比較する
            if (userInfoModel.Password != inputModel.Password)
            {
                // 不一致の場合はログイン失敗
                return null;
            }
            // 一致していた場合は最終ログイン日時を更新
            DateTime lastLoginDttm = DateTime.Now;

            string result = _UserInfoRepository.UpdateLastLoginInfo(inputModel.UserId, lastLoginDttm);
            if (result != UserInfoRepository.RESULT_NORMAL)
            {
                return null;
            }

            // Db更新確定
            _DbContext.SaveChanges();

            userInfoModel.LastLoginDttm = lastLoginDttm;
            return userInfoModel;

        }

    }
}