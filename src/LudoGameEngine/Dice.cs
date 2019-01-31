using System;

namespace LudoGameEngine
{
    public class Dice : IDice
    {
        public int RollDice()
        {
            Random random = new Random();
            return random.Next(1, 7);
        }
    }
}