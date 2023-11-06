﻿using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult StaffIndex(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }
    }
}
