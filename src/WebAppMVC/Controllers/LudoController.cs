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
using WebAppMVC.Code;

namespace WebAppMVC.Controllers
{
    public class LudoController : Controller
    {
        private IRestClient client;

        public LudoController(IRestClient client)
        {
            this.client = client;
            client.BaseUrl = new Uri("https://ludogame.azurewebsites.net");
        }

        public IActionResult CreateGame(CreateGameModel model)
        {
            // Create game
            model.GameId = ApiMethods.CreateGame(client);

            // Add all players that aren't null to the game.
            if (model.Player1Name != null)
            {
                ApiMethods.AddPlayer(model.GameId, model.Player1Name, "0", client);
            }
            if (model.Player2Name != null)
            {
                ApiMethods.AddPlayer(model.GameId, model.Player2Name, "1", client);
            }
            if (model.Player3Name != null)
            {
                ApiMethods.AddPlayer(model.GameId, model.Player3Name, "2", client);
            }
            if (model.Player4Name != null)
            {
                ApiMethods.AddPlayer(model.GameId, model.Player4Name, "3", client);
            }

            // Set cookies
            ApiMethods.AssignPlayerCookies(model, Response);

            return RedirectToAction("Lobby");
        }

        public IActionResult Game()
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"].ToString());
            GameModel game = ApiMethods.GetSpecificGame(gameId, client);
            if (Request.Cookies["lastdicevalue"] != null)
            {
                game.diceValue = int.Parse(Request.Cookies["lastdicevalue"]);
            }

            return View(game);
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> JoinGame()
        {
            //var response = new RestRequest("api/ludo/getallgames", Method.GET);
            //var restResponse = await client.ExecuteTaskAsync(response);
            //var allGames = Game.FromJson(restResponse.Content);
            //GameList output = new GameList() { AllGames = allGames };

            return View(/*output*/);
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

            // Get players for the game and deserialize them
            PlayerModelContainer model = new PlayerModelContainer
            {
                gameId = Guid.Parse(gameId),
                Players = ApiMethods.GetAllPlayers(Guid.Parse(gameId), client)
            };

            return View(model);
        }

        public IActionResult MovePiece(GameModel model)
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
            ApiMethods.MovePiece(gameId, model.pieceId, int.Parse(Request.Cookies["lastdicevalue"]), client);
            Response.Cookies.Append("lastdicevalue", "0");
            TempData["rolled"] = "false";
            return RedirectToAction("game");
        }

        public IActionResult NewGame()
        {
            return View();
        }

        public IActionResult RollDice(GameModel model)
        {
            if (TempData["rolled"] != "true")
            {
                Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Append("lastdicevalue", ApiMethods.RollDice(gameId, client).ToString(), cookie);
            }
            TempData["rolled"] = "true";
            return RedirectToAction("game");
        }

        public IActionResult Rules()
        {
            return View();
        }

        //public IActionResult SelectGame(CreateGameModel model)
        //{
        //    // Add the joining player to the game
        //    PlayerModel addedPlayer = ApiMethods.AddPlayer(model.GameId, model.PlayerName, model.PlayerColor, client);
        //    model.PlayerId = addedPlayer.PlayerId;

        //    // Set cookies
        //    ApiMethods.AssignPlayerCookies(model, Response);

        //    return RedirectToAction("Lobby");
        //}

        public IActionResult StartGame()
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
            bool startGameSuccess = ApiMethods.StartGame(gameId, client);

            if (startGameSuccess)
            {
                return RedirectToAction("Game");
            }
            else
            {
                ViewBag.errorMessage = "Couldn't start game.";
                return View("Lobby", new PlayerModelContainer { gameId = gameId, Players = ApiMethods.GetAllPlayers(gameId, client) });
            }
        }
    }
}