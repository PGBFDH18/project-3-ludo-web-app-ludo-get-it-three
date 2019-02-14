using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebAppMVC.Models;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace WebAppMVC.Controllers
{
    public class LudoController : Controller
    {
        IRestClient client;

        public LudoController(IRestClient _client)
        {
            client = _client;
            client.BaseUrl = new Uri("https://ludogame.azurewebsites.net");
        }


        public IActionResult Home()
        {
            return View();
        }

        public IActionResult NewGame()
        {
            //var request = new RestRequest("api/ludo/createnewgame", Method.POST);
            //IRestResponse<Guid> response = client.Execute<Guid>(request);
            //info.gameId = Guid.Parse(response.Data.ToString());
            
            return View();
        }

        public IActionResult CreateGame(CreateGameModel model)
        {
            // Create game
            var request = new RestRequest("api/ludo/createnewgame", Method.POST);
            IRestResponse<Guid> response = client.Execute<Guid>(request);
            model.GameId = Guid.Parse(response.Data.ToString());

            
            // Add player that created game
            request = new RestRequest($"api/ludo/{model.GameId}/players/addplayer", Method.POST);
            request.AddParameter(new Parameter("name", model.PlayerName, ParameterType.QueryString));
            request.AddParameter(new Parameter("colorID", int.Parse(model.PlayerColor), ParameterType.QueryString));
            client.Execute<PlayerModel>(request);

            return RedirectToAction("Lobby", model);
        }

        public IActionResult JoinGame()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult Lobby(CreateGameModel model)
        {
            return View(model);
        }
    }
}