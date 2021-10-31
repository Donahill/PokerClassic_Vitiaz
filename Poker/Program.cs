using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program {

        static void Main(string[] args) {

            
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetWindowSize(65, 40); 
            //уберём скроллеры по бокам, задав размеры окна
            Console.BufferWidth = 65;
            Console.BufferHeight = 40;
           
            Console.Title = "Poker Classic";
            Console.OutputEncoding = Encoding.UTF8;
            DealCards dc = new DealCards();
            bool quit = false;

            while (!quit) {

                dc.Deal();

                char selection = ' ';
                while(!selection.Equals('Y') && !selection.Equals('N')){
                
                    Console.WriteLine("Play again? (y - yes; n - no)");
                    selection = Convert.ToChar(Console.ReadLine().ToUpper());

                    if (selection.Equals('Y'))
                        quit = false;
                    else if (selection.Equals('N'))
                        quit = true;
                    else
                        Console.WriteLine("Invalid Selection. Try again");
                }

            }

            Console.ReadKey();

        }
       
    }
}
