using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public PlayerColor PlayerColor;
        public IEnumerable<PieceModel> Pieces { get; set; }
        public int Offset
        {
            get
            {
                return (int)PlayerColor * 10;
            }
        }
    }
}
