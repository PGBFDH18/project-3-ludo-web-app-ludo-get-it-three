﻿using System;
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

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult NewGame()
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

        public IActionResult Rules()
        {
            return View();
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

        public IActionResult Game()
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"].ToString());
            GameModel game = ApiMethods.GetSpecificGame(gameId, client);

            return View(game);
        }

        public IActionResult CreateGame(CreateGameModel model)
        {
            // Create game
            model.GameId = ApiMethods.CreateGame(client);

            // Add player that created game
            PlayerModel addedPlayer = ApiMethods.AddPlayer(model.GameId, model.PlayerName, model.PlayerColor, client);
            model.PlayerId = addedPlayer.PlayerId;

            // Set cookies
            ApiMethods.AssignPlayerCookies(model, Response);

            return RedirectToAction("Lobby");
        }

        public IActionResult SelectGame(CreateGameModel model)
        {
            // Add the joining player to the game
            PlayerModel addedPlayer = ApiMethods.AddPlayer(model.GameId, model.PlayerName, model.PlayerColor, client);
            model.PlayerId = addedPlayer.PlayerId;

            // Set cookies
            ApiMethods.AssignPlayerCookies(model, Response);

            return RedirectToAction("Lobby");
        }

        public IActionResult StartGame()
        {
            Guid gameId = Guid.Parse(Request.Cookies["gameid"]);
            bool startGameSuccess = ApiMethods.StartGame(gameId, client);

            if(startGameSuccess)
            {
                return RedirectToAction("Game");
            }
            else
            {
                ViewBag.errorMessage = "Couldn't start game.";
                return View("Lobby", new PlayerModelContainer { gameId = gameId ,Players = ApiMethods.GetAllPlayers(gameId, client)});
            }
        }

        public IActionResult RollDice()
        {
            // Move this to api methods
            var gameId = Request.Cookies["gameid"].ToString();
            var request = new RestRequest($"api/ludo/{gameId}/rolldice");
            IRestResponse<int> rollDiceResponse = client.Execute<int>(request);


            return View("Game");
        }
    }
}