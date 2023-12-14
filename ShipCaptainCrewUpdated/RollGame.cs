using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipCaptainCrew
{
 public class ShipCaptainCrewGame
    {
        private Random random;

        public ShipCaptainCrewGame()
        {
            random = new Random();
        }

        public int[] RollDice()
        {
            return Enumerable.Range(0, 5).Select(_ => random.Next(1, 7)).ToArray();
        }

        public int CountOccurrences(int[] dice, int target)
        {
            return dice.Count(die => die == target);
        }

        public int SumOfRemainingDice(int[] dice)
        {
            return dice.Where(die => die != 4 && die != 5 && die != 6).Sum();
        }
    }
}