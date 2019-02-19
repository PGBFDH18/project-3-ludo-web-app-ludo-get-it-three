using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestSharp;
using WebAppMVC.Models;
using Newtonsoft.Json;

namespace WebAppMVC.Code
{
    static public class ApiMethods
    {
        static public PlayerModel AddPlayer(Guid gameId, string playerName, string playerColor, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/players/addplayer", Method.POST);
            request.AddParameter(new Parameter("name", playerName, ParameterType.QueryString));
            request.AddParameter(new Parameter("colorID", int.Parse(playerColor), ParameterType.QueryString));
            IRestResponse<PlayerModel> addPlayerResponse = client.Execute<PlayerModel>(request);
        
            return addPlayerResponse.Data;
        }

        static public Guid CreateGame(IRestClient client)
        {
            var request = new RestRequest("api/ludo/createnewgame", Method.POST);
            IRestResponse<Guid> createGameResponse = client.Execute<Guid>(request);
            return createGameResponse.Data;
        }

        static public PlayerModel[] GetAllPlayers(Guid gameId, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/players/getallplayers", Method.GET);
            IRestResponse getAllPlayersResponse = client.Execute(request);
            PlayerModel[] deserializedPlayers = JsonConvert.DeserializeObject<PlayerModel[]>(getAllPlayersResponse.Content);

            return deserializedPlayers;
        }

        static public GameModel GetSpecificGame(Guid gameId, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/getgamedetails", Method.GET);
            IRestResponse<GameModel> getGameResponse = client.Execute<GameModel>(request);

            return getGameResponse.Data;
        }

        static public bool StartGame(Guid gameId, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/startgame", Method.PUT);
            IRestResponse<bool> startGameResponse = client.Execute<bool>(request);

            return startGameResponse.Data;
        }

        static public void AssignPlayerCookies(CreateGameModel model, Microsoft.AspNetCore.Http.HttpResponse Response)
        {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Append("gameid", model.GameId.ToString(), cookie);
            Response.Cookies.Append("playercolorid", model.PlayerColor, cookie);
            Response.Cookies.Append("playerid", model.PlayerId.ToString(), cookie);
            Response.Cookies.Append("playername", model.PlayerName, cookie);
        }


    }
}
