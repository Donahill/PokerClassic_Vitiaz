using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Poker
{
    class Program {

        public static void PlayGame()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;


            Bets bets = new Bets();
            DealCards dc = new DealCards();
            bool quit = false;

            while (!quit)
            {

                dc.Deal(ref bets);

                string selection = " ";
                while (!selection.Equals("Yes") && !selection.Equals("No"))
                {

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Current Chip Count: ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(bets.Chips);

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Minimum Bet: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(20);

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Press any key to continue...");

                    Console.ReadKey();

                    MyMenu menu = new MyMenu();
                    menu.Add("Yes      ", (obj) => Yes(ref selection));
                    menu.Add("No      ", (obj) => No(ref selection));

                    menu.Play_Again(bets);

                    //selection = Console.ReadLine();


                    if (selection.Equals("Yes"))
                        quit = false;
                    else if (selection.Equals("No"))
                        quit = true;
                    else
                        Console.WriteLine("Invalid Selection. Try again");
                }


            }
        }

        public static void Exit()
        {
            SendKeys.SendWait("{ESC}");
        }

        public static void Yes(ref string selection)
        {
            selection = "Yes";
        }

        public static void No(ref string selection)
        {
            selection = "No";
        }



        static void Main(string[] args) {

            
            
            Console.SetWindowSize(65, 40); 
            //уберём скроллеры по бокам, задав размеры окна
            Console.BufferWidth = 65;
            Console.BufferHeight = 40;
           
            Console.Title = "Poker Classic";
            Console.OutputEncoding = Encoding.UTF8;

            MyMenu menu = new MyMenu();
            menu.Add("Play Game               ", (obj) => PlayGame());
            menu.Add("Exit               ", (obj) => Exit());

            menu.Show();

        
        }
        
    }
}
