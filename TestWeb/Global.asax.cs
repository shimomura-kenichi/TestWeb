using log4net;
using Microsoft.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestWeb.Properties;

namespace TestWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILog _Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Log4Net�̒ǉ�
            log4net.Config.XmlConfigurator.Configure();

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string errorMessage = Resources.MEP0006;

            // ��O�̓��e���擾���AStringBuilder �Ɋi�[����
            StringBuilder message = new StringBuilder();
            if (Server != null)
            {
                Exception ex;

                // ��O�����擾����
                for (ex = Server.GetLastError(); ex != null; ex = ex.InnerException)
                {
                    message.AppendFormat("{0}: {1}{2}", ex.GetType().FullName, ex.Message, ex.StackTrace);
                    var httpException = ex as HttpException;
                    if (httpException == null)
                    {
                        errorMessage = Resources.MEP0006;
                    }
                    else
                    {
                        switch (httpException.GetHttpCode())
                        {
                            case 404:
                                errorMessage = Resources.MEP0005;
                                Response.StatusCode = 404;
                                break;
                            case 403:
                                errorMessage = Resources.MEP0007;
                                break;
                            case 500:
                                errorMessage = Resources.MEP0006;
                                break;
                        }
                    }
                }

                //
                // ��O���Ɠ�����O�������O�Ƃ��ďo�͂���
                _Log.Error(message.ToString());

                // ��O���N���A�i�J�X�^���G���[�y�[�W��ݒ肵�Ă���ꍇ�̓R�����g�A�E�g���邱�Ɓj
                Server.ClearError();

            }
            // �G���[��ʂɑJ�ڂ���
            if (Response.StatusCode != 404)
            {
                Response.Redirect("/Error/Index?ErrorMessage=" + HttpUtility.UrlEncode(errorMessage));
            }
        }

        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            // Event handler code
        }

        /// <summary>
        /// �A�N�Z�X���O���o�͂���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ILog log = log4net.LogManager.GetLogger("AccessLog");

            string accessLog = string.Format("{0} {1} {2} {3}", Request.RawUrl, Request.UserAgent, Request.UserHostAddress, Response.StatusCode.ToString());
            log.Info(accessLog);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

    }
    
}
