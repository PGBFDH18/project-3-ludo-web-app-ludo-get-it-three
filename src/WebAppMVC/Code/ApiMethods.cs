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
        }

        static public int RollDice(Guid gameId, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/rolldice", Method.GET);
            IRestResponse<int> rollDiceResponse = client.Execute<int>(request);

            return rollDiceResponse.Data;
        }

        static public PieceModel MovePiece(Guid gameId, int pieceId, int numberOfFields, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/movepiece", Method.PUT);
            request.AddParameter("pieceId", pieceId, ParameterType.QueryString);
            request.AddParameter("numberOfFields", numberOfFields, ParameterType.QueryString);

            IRestResponse<PieceModel> movePieceResponse = client.Execute<PieceModel>(request);

            EndTurn(gameId, client);
            return movePieceResponse.Data;
        }

        static public void EndTurn(Guid gameId, IRestClient client)
        {
            var request = new RestRequest($"api/ludo/{gameId}/endturn", Method.PUT);
            IRestResponse endTurnResponse = client.Execute(request);
        }

    }
}