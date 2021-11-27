using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class Bets
    {
        public int Chips { get; set; } = 1000;
        public int Bet { get; set; }
        public int MinimumBet { get; set; }
     //   public int HandsCompleted { get; set; } = 0;


        // <summary>
        // Add Player's chips to their bet.
        // </summary>
        // <param name="bet">The number of Chips to bet</param>
        public void AddBet(int bet)
        {
            Bet += bet;
            Chips -= bet;
        }

        // <summary>
        // Set Bet to 0
        // </summary>
        public void ClearBet()
        {
            Bet = 0;
        }

        // <summary>
        // Cancel player's bet. They will neither lose nor gain any chips.
        // </summary>
        public void ReturnBet()
        {
            Chips += Bet;
            ClearBet();
        }

        public void ShowChips()
        {
            Console.Write("Current Chip Count: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Chips);

            Console.ForegroundColor = ConsoleColor.Black ;
            Console.Write("Minimum Bet: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(MinimumBet);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Bank: ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(Bet*2);



        }

    }
}
