﻿using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
