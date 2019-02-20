using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class GameModel
    {
        public int currentPlayerId { get; set; }
        public Dice dice { get; set; }
        public int diceValue { get; set; }
        public GameState gameState { get; set; }
        public MovePiece movePiece { get; set; }
        public int pieceId { get; set; }
        public List<PlayerModel> players { get; set; }
    }
}