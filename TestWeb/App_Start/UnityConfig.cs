using System;
using System.Data.Entity;
using TestWeb.Models;
using TestWeb.Models.Common;
using TestWeb.Models.Login;
using TestWeb.Models.Repository;
using Unity;
using Unity.AspNet.Mvc;

namespace TestWeb
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // ログインサービス
            container.RegisterType<ILoginService, LoginService>(new PerRequestLifetimeManager());

            // ログインサービス(プロキシ)
            container.RegisterType<IServiceProxy<ILoginService>, ServiceProxy<ILoginService>>(new PerRequestLifetimeManager());

            // ユーザー情報リポジトリ

            container.RegisterType<IUserInfoRepository, UserInfoRepository>(new PerRequestLifetimeManager());

            // DbContext
            // 下記は現時点では、コメントアウトしたRegisterTypeでも問題ないが、
            // 最終的にはSQLの実行結果をログに出力する設定を入れ込みたいので
            // RegisterFactoryで登録しておく
            // container.RegisterType<AttendanceDbEntities>(new PerRequestLifetimeManager());
            container.RegisterFactory<AttendanceDbEntities>(c => new AttendanceDbEntities(), new PerRequestLifetimeManager());

            container.RegisterFactory<DbContext>(c => c.Resolve<AttendanceDbEntities>());

        }
    }
}