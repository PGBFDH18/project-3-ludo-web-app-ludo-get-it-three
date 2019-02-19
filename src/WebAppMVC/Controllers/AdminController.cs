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
        private IRestClient client;

        public AdminController(IRestClient _client)
        {
            client = new RestClient();
            client.BaseUrl = new Uri("https://ludogame.azurewebsites.net");
        }

        public IActionResult Controlpanel()
        {
            if (HttpContext.Session.GetString("adminUser") == "LudoBoss" && HttpContext.Session.GetString("adminPassword") == "1337")
            {
                ViewBag.User = HttpContext.Session.GetString("adminUser");
                ViewBag.DeleteAllGames = TempData["DeleteAllGames"];
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> DeleteAllGames()
        {
            var response = new RestRequest("api/ludo/getallgames", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            var allGames = Game.FromJson(restResponse.Content);
            foreach (var game in allGames)
            {
                string gamekey = game.Key.ToString();
                var response2 = new RestRequest($"api/ludo/{gamekey}/removegame", Method.DELETE);
                var restResponse2 = await client.ExecuteTaskAsync(response2);
            }
            TempData["DeleteAllGames"] = "All Games Deleted!";
            return RedirectToAction("Controlpanel");
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