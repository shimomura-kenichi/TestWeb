using System;
using System.Data.Entity;
using TestWeb.Models;
using TestWeb.Models.Common;
using TestWeb.Models.Login;
using TestWeb.Models.Repository;
using TestWeb.Models.Department;
using Unity;
using Unity.AspNet.Mvc;
using TestWeb.Models.AttendanceTime;

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
            // DbContext
            // ���L�͌����_�ł́A�R�����g�A�E�g����RegisterType�ł����Ȃ����A
            // �ŏI�I�ɂ�SQL�̎��s���ʂ����O�ɏo�͂���ݒ����ꍞ�݂����̂�
            // RegisterFactory�œo�^���Ă���
            // container.RegisterType<AttendanceDbEntities>(new PerRequestLifetimeManager());
            container.RegisterFactory<AttendanceDbEntities>(c => new AttendanceDbEntities(), new PerRequestLifetimeManager());

            container.RegisterFactory<DbContext>(c => c.Resolve<AttendanceDbEntities>());

            // �Z�b�V�����}�l�[�W���[
            container.RegisterType<ISessionManager, SessionManager>(new PerRequestLifetimeManager());

            // �R�[�h�}�X�^�[���|�W�g��
            container.RegisterType<IM201CodeMstRepository, M201CodeMstRepository>(new PerRequestLifetimeManager());


            // ���O�C���T�[�r�X
            container.RegisterType<ILoginService, LoginService>(new PerRequestLifetimeManager());
            // ���O�C���T�[�r�X(�v���L�V)
            container.RegisterType<IServiceProxy<ILoginService>, ServiceProxy<ILoginService>>(new PerRequestLifetimeManager());
            // ���[�U�[��񃊃|�W�g��
            container.RegisterType<IUserInfoRepository, UserInfoRepository>(new PerRequestLifetimeManager());

            // �����T�[�r�X
            container.RegisterType<IDepartmentService, DepartmentService>(new PerRequestLifetimeManager());
            // �����T�[�r�X(�v���L�V)
            container.RegisterType<IServiceProxy<IDepartmentService>, ServiceProxy<IDepartmentService>>(new PerRequestLifetimeManager());

            // �o�ދΎ��ԃT�[�r�X
            container.RegisterType<IAttendanceTimeService, AttendanceTimeService>(new PerRequestLifetimeManager());
            // �o�ދΎ��ԃT�[�r�X(�v���L�V)
            container.RegisterType<IServiceProxy<IAttendanceTimeService>, ServiceProxy<IAttendanceTimeService>>(new PerRequestLifetimeManager());
            // T200�o�ދΎ��ԃ��|�W�g��
            container.RegisterType<IT200AttendanceTimeRepository, T200AttendanceTimeRepository>(new PerRequestLifetimeManager());
            // V200�o�ދΎ��ԃr���[���|�W�g��
            container.RegisterType<IV200AttendanceTimeRepository, V200AttendanceTimeRepository>(new PerRequestLifetimeManager());



        }
    }
}