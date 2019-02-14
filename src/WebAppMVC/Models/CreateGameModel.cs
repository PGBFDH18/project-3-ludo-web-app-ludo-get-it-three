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
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerColor { get; set; }
    }
}
