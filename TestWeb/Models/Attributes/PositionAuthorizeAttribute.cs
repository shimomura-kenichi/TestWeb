using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestWeb.Properties;

namespace TestWeb.Models.Attributes
{
    public class PositionAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// ロール権限不足の場合のエラー画面変更
        /// </summary>
        /// <param name="filterContext">コンテキスト</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new {action = "Index", controller = "Error", errorMessage = Resources.MEP0007 }));
        }
    }
}