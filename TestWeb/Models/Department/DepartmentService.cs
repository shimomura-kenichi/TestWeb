using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Common;
using TestWeb.Models.Department.InputModel;
using TestWeb.Models.Department.ViewModel;
using TestWeb.Properties;

namespace TestWeb.Models.Department
{
    /// <summary>
    /// 所属サービス
    /// </summary>
    public class DepartmentService : IDepartmentService
    {
        /// <summary>
        /// サービスメッセージ
        /// </summary>
        public ServiceMessage ServiceMessage { get; set; }

        /// <summary>
        /// ユーザー情報
        /// </summary>
        public UserInfoModel UserInfoModel { get; set; }

        /// <summary>
        /// DB保存用ファンクション
        /// </summary>
        public Func<bool> SaveChangeFunc { get; set; }

        /// <summary>
        /// 所属選択画面初期表示
        /// </summary>
        /// <returns>ビューモデル</returns>
        public DepartmentViewModel Init()
        {
            DepartmentViewModel viewModel = new DepartmentViewModel();
            viewModel.UserDepartmentList = new List<UserDepartmentModel>();

            // コピー
            this.UserInfoModel.UserDepartmentList.ForEach(
                m => viewModel.UserDepartmentList.Add(
                    (UserDepartmentModel)ModelUtil.CopyModelToModel(m, new UserDepartmentModel())));

            return viewModel;
        }

        /// <summary>
        /// 所属選択
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>所属選択後のユーザー情報</returns>
        public UserInfoModel SelectDepartment(DepartmentSelectInputModel inputModel)
        {
            // ユーザー情報をコピーする
            UserInfoModel userInfoModel = new UserInfoModel();
            userInfoModel.UserDepartmentList = new List<UserDepartmentModel>();
            ModelUtil.CopyModelToModel(this.UserInfoModel, userInfoModel);
            this.UserInfoModel.UserDepartmentList.ForEach(
                m => userInfoModel.UserDepartmentList.Add(
                    (UserDepartmentModel)ModelUtil.CopyModelToModel(m, new UserDepartmentModel())));

            // 選択行がリストのインデックス範囲内の場合
            int selectNumberNum = int.Parse(inputModel.SelectNumber);
            if (selectNumberNum >= 0 && selectNumberNum <= this.UserInfoModel.UserDepartmentList.Count-1)
            {
                userInfoModel.CurrentDepartmentCd = this.UserInfoModel.UserDepartmentList[selectNumberNum].DepartmentCd;
                userInfoModel.CurrentPositionCd = this.UserInfoModel.UserDepartmentList[selectNumberNum].PositionCd;
            } else
            {
                // 存在しない場合はエラー
                this.ServiceMessage.AddErrorMessage(null, string.Format(Resources.MEI0009
                    , ModelUtil.GetDisplayName<DepartmentSelectInputModel>(m => m.SelectNumber)));
            }
            return userInfoModel;
        }
    }
}