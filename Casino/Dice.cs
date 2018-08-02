using System;

namespace Casino
{
    public class Dice : IDice
    {
        private readonly Random _random = new Random();

        public int GetDiceDropNumber()
        {
            return _random.Next(1, 6);
        }
    }
}