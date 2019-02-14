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
    public class LudoController : Controller
    {
        private IRestClient client;

        public LudoController(IRestClient _client)
        {
            client = _client;
            client.BaseUrl = new Uri("https://ludogame.azurewebsites.net");
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> JoinGame(ClientInfo info)
        {
            var response = new RestRequest("api/ludo/getallgames", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            var allGames = Game.FromJson(restResponse.Content);
            GameList output = new GameList() { AllGames = allGames };

            return View(output);
        }

        public IActionResult Lobby()
        {
            return View();
        }

        public IActionResult NewGame(ClientInfo info)
        {
            var request = new RestRequest("api/ludo/createnewgame", Method.POST);
            IRestResponse<Guid> response = client.Execute<Guid>(request);
            info.gameId = Guid.Parse(response.Data.ToString());

            return View(info);
        }

        public IActionResult Rules()
        {
            return View();
        }
    }
}