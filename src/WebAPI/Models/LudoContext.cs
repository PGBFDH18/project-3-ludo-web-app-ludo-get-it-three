using LudoGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebAPI.Models
{
    public class LudoContext : ILudoContext
    {
        public Dictionary<Guid, LudoGame> ludoGames;
        public Guid g;

        public LudoContext()
        {
            ludoGames = new Dictionary<Guid, LudoGame>();
        }

        public Guid AddGame()
        {
            g = Guid.NewGuid();
            ludoGames.Add(g, new LudoGame(new Dice()));
            return g;
        }

        public bool RemoveGame(Guid id)
        {
            return ludoGames.Remove(id);
        }

        public Player AddPlayer(Guid id, string name, int colorID)
        {
            if (name == null)
            {
                return null;
            }
            return ludoGames[id].AddPlayer(name, colorID);
        }

        public bool RemovePlayer(Guid id, int colorID)
        {
            return ludoGames[id].RemovePlayer(colorID);
        }

        public Dictionary<Guid, LudoGame> GetAllGames()
        {
            return ludoGames;
        }

        public LudoGame GetGameDetail(Guid _id)
        {
            return ludoGames.First(id => id.Key == _id).Value;
        }

        public Player[] GetAllPlayers(Guid id)
        {
            return ludoGames[id].GetPlayers();
        }

        public Player GetPlayerDetail(Guid id, int colorID)
        {
            return ludoGames[id].GetPlayer(colorID);
        }

        public Piece MovePiece(Guid id, int pieceId, int numberOfFields)
        {
            return ludoGames[id].MovePiece(ludoGames[id].GetCurrentPlayer(), pieceId, numberOfFields);
        }

        public bool StartGame(Guid id)
        {
            return ludoGames[id].StartGame();
        }

        public int RollDice(Guid id)
        {
            return ludoGames[id].RollDice();
        }

        public void EndTurn(Guid id)
        {
            ludoGames[id].EndTurn(ludoGames[id].GetCurrentPlayer());
        }

        public Player GetWinner(Guid id)
        {
            return ludoGames[id].GetWinner();
        }
    }
}