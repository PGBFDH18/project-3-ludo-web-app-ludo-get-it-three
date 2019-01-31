using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface ILudoContext
    {
        Guid AddGame();
        bool RemoveGame(Guid id);
        Player AddPlayer(Guid id, string name, int colorID);
        bool RemovePlayer(Guid id, int colorID);
        Dictionary<Guid, LudoGame> GetAllGames();
        LudoGame GetGameDetail(Guid id);
        Player[] GetAllPlayers(Guid id);
        Player GetPlayerDetail(Guid id, int ColorID);
        Piece MovePiece(Guid id, int pieceId, int numberOfFields);
        bool StartGame(Guid id);
        int RollDice(Guid id);
        void EndTurn(Guid id);
        Player GetWinner(Guid id);
    }
}