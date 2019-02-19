using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class GameModel
    {
        public Dice dice { get; set; }
        public GameState gameState { get; set; }
        public List<PlayerModel> players { get; set; }
        public int currentPlayerId { get; set; }
    }
}
