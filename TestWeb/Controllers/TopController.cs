using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Attributes;
using TestWeb.Models.Common;

namespace TestWeb.Controllers
{
    public class TopController : Controller
    {
        [HttpGet]
        [PositionAuthorize(Roles = Constants.ROLE_ALL)]
        // GET: Top
        public ActionResult Index()
        {
            return View("Top");
        }
    }
}