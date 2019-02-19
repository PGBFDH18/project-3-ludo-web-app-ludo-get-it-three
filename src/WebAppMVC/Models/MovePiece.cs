using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class MovePiece
    {
        public int numberOfFields { get; set; }
        public Guid gameId { get; set; }
        public int pieceId { get; set; }
    }
}
