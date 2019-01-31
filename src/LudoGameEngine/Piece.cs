namespace LudoGameEngine
{
    public class Piece
    {
        public int PieceId { get; set; }
        public PieceGameState State { get; set; }
        public int Position { get; set; }
        public PlayerColor PieceColor {get; set;} 
    }
}