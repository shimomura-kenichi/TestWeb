﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWeb.Controllers
{
    public class TopController : Controller
    {
        // GET: Top
        public ActionResult Index()
        {
            return View("Top");
        }
    }
}