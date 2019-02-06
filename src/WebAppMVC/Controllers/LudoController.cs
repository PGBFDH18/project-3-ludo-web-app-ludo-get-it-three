using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class LudoController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult NewGame()
        {
            return View();
        }

        public IActionResult JoinGame()
        {
            return View();
        }
    }
}