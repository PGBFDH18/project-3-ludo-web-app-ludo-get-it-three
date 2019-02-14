using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class PlayerModel
    {
        public int PlayerId;
        public string Name;
        public PlayerColor PlayerColor;
        public PieceModel[] Pieces { get; set; }
        public int Offset
        {
            get
            {
                return (int)PlayerColor * 10;
            }
        }
    }
}
