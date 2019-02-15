using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebAppMVC.Models;
using Microsoft.AspNetCore.Http;
using RestSharp;
using Newtonsoft.Json;

namespace WebAppMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Controlpanel()
        {
            if (HttpContext.Session.GetString("adminUser") == "LudoBoss" && HttpContext.Session.GetString("adminPassword") == "1337")
            {
                ViewBag.User = HttpContext.Session.GetString("adminUser");
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Home()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        [HttpPost]
        public IActionResult Home(AdminLoginModel model)
        {
            if (model.Username == "LudoBoss" && model.Password == "1337")
            {
                HttpContext.Session.SetString("adminUser", model.Username);
                HttpContext.Session.SetString("adminPassword", model.Password);
                return RedirectToAction("Controlpanel");
            }
            else
            {
                ViewBag.ErrorMessage = "Username or password is invalid.";
                return View();
            }
        }
    }
}