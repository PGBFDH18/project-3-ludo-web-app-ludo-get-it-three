using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class GameBoardModel
    {
        private Guid gameId = new Guid();
        public Guid GameId { get => gameId; set => gameId = value; }
        public int AmountOfPlayers { get; set; }
        public Player Player { get; set; }
        public Piece Piece { get; set; }
    }
}