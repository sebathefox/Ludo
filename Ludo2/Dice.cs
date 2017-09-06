using System;
using System.Linq;

namespace Ludo2
{
    public class Dice
    {
        private int diceValue;

        public Dice()
        {
            this.ThrowDice();
        }
        
        public int ThrowDice()
        {
            Random rand = new Random();

            this.diceValue = rand.Next(1, 7);

            return this.diceValue;
        }
        public int GetValue()
        {
            return this.diceValue;
        }
    }
}
