﻿using Microsoft.AspNetCore.Mvc;

namespace ArchBackend.Web.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
