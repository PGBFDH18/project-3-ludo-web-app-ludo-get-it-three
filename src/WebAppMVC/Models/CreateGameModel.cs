using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class CreateGameModel
    {
        private Guid gameId = new Guid();
        public Guid GameId { get => gameId; set => gameId = value; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Player3Id { get; set; }
        public int Player4Id { get; set; }
        public string Player1Name { get; set; }
        public string Player1Color { get; set; }
        public string Player2Name { get; set; }
        public string Player2Color { get; set; }
        public string Player3Name { get; set; }
        public string Player3Color { get; set; }
        public string Player4Name { get; set; }
        public string Player4Color { get; set; }
    }
}