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

        public IActionResult NewGame()
        {
            return View();
        }

        public IActionResult CreateGame(CreateGameModel model)
        {
            // Create game
            var request = new RestRequest("api/ludo/createnewgame", Method.POST);
            IRestResponse<Guid> createGameResponse = client.Execute<Guid>(request);
            model.GameId = Guid.Parse(createGameResponse.Data.ToString());

            // Add player that created game
            // Duplicate code, refactor?
            request = new RestRequest($"api/ludo/{model.GameId}/players/addplayer", Method.POST);
            request.AddParameter(new Parameter("name", model.PlayerName, ParameterType.QueryString));
            request.AddParameter(new Parameter("colorID", int.Parse(model.PlayerColor), ParameterType.QueryString));
            IRestResponse<PlayerModel> addPlayerResponse = client.Execute<PlayerModel>(request);
            model.PlayerId = addPlayerResponse.Data.PlayerId;

            // Set cookies
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Append("gameid", model.GameId.ToString(), cookie);
            Response.Cookies.Append("playercolorid", model.PlayerColor, cookie);
            Response.Cookies.Append("playerid", model.PlayerId.ToString(), cookie);
            Response.Cookies.Append("playername", model.PlayerName, cookie);

            return RedirectToAction("Lobby");
        }

        public async Task<IActionResult> JoinGame()
        {
            //var response = new RestRequest("api/ludo/getallgames", Method.GET);
            //var restResponse = await client.ExecuteTaskAsync(response);
            //var allGames = Game.FromJson(restResponse.Content);
            //GameList output = new GameList() { AllGames = allGames };

            return View(/*output*/);
        }

        public IActionResult SelectGame(CreateGameModel model)
        {
            // Duplicate code, refactor?
            var request = new RestRequest($"api/ludo/{model.GameId}/players/addplayer", Method.POST);
            request.AddParameter(new Parameter("name", model.PlayerName, ParameterType.QueryString));
            request.AddParameter(new Parameter("colorID", int.Parse(model.PlayerColor), ParameterType.QueryString));
            IRestResponse<PlayerModel> addPlayerResponse = client.Execute<PlayerModel>(request);
            model.PlayerId = addPlayerResponse.Data.PlayerId;

            // Set cookies
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Append("gameid", model.GameId.ToString(), cookie);
            Response.Cookies.Append("playercolorid", model.PlayerColor, cookie);
            Response.Cookies.Append("playerid", model.PlayerId.ToString(), cookie);
            Response.Cookies.Append("playername", model.PlayerName, cookie);

            return RedirectToAction("Lobby");
        }

        public IActionResult Lobby()
        {
            if (Request.Cookies["gameid"] == null)
            {
                ViewBag.message = "You are currently not assigned to a game.";
                return View();
            }

            // Get the cookie that represents which game the user is playing
            var gameId = Request.Cookies["gameid"].ToString();
            ViewBag.gameid = gameId;

            // Get players for the game and deserialize them
            var request = new RestRequest($"api/ludo/{gameId}/players/getplayers", Method.GET);
            IRestResponse getAllPlayersResponse = client.Execute(request);
            PlayerModel[] deserializedPlayers = JsonConvert.DeserializeObject<PlayerModel[]>(getAllPlayersResponse.Content);
            PlayerModelContainer model = new PlayerModelContainer
            {
                Players = deserializedPlayers
            }; 

            return View(model);
        }

        

        public IActionResult Rules()
        {
            return View();
        }
    }
}