using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWeb.Models.Attributes;

namespace TestWeb.Controllers
{
    public class TopController : Controller
    {
        [HttpGet]
        [PositionAuthorize(Roles = "01")]
        // GET: Top
        public ActionResult Index()
        {
            return View("Top");
        }
    }
}