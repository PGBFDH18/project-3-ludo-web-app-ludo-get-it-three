using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class PieceModel
    {
        public int PieceId { get; set; }
        public PieceGameState State { get; set; }
        public int Position { get; set; }
        public PlayerColor PieceColor { get; set; }
    }
}
