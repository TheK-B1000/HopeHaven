﻿using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
