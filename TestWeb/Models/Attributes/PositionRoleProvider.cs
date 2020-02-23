using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TestWeb.Models.Common;
using Unity;

namespace TestWeb.Models.Attributes
{
    /// <summary>
    /// 役職用ロール
    /// </summary>
    public class PositionRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ユーザーのロールを取得する
        /// </summary>
        /// <param name="username">ユーザー名</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            // UnityMvcからセッションマネージャーを取得する
            ISessionManager sessionManager = UnityConfig.Container.Resolve<ISessionManager>();
            
            UserInfoModel userInfoModel = sessionManager.GetUserInfoModel();
            return new string[] { userInfoModel.CurrentPositionCd };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}