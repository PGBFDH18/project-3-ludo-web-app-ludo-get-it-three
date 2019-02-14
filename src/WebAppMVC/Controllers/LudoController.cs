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

        public IActionResult NewGame(ClientInfo info)
        {
            var request = new RestRequest("api/ludo/createnewgame", Method.POST);
            IRestResponse<Guid> response = client.Execute<Guid>(request);
            info.gameId = Guid.Parse(response.Data.ToString());
            
            return View(info);
        }

        public IActionResult JoinGame(ClientInfo info)
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