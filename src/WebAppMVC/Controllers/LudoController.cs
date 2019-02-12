using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class LudoController : Controller
    {
        GameInfo gameInfo;

        public LudoController(GameInfo _gameInfo)
        {
            gameInfo = _gameInfo;
        }

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

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult Lobby()
        {
            return View();
        }
    }
}