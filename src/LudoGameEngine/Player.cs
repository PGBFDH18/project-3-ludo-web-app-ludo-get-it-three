namespace LudoGameEngine
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name {get; set;}
        public PlayerColor PlayerColor { get; set; }
        public Piece[] Pieces {get; set;}
        public int Offset
        {
            get
            {
                return (int)PlayerColor * 10;
            }
        }
    }
}