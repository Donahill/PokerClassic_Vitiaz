using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public delegate void MyMenuDelegate(object obj);
    public class MenuItem : IComparable<MenuItem>
    {
        public string MenuString { get; }
        public event MyMenuDelegate _action;

        public MenuItem(string str, MyMenuDelegate action)
        {
            MenuString = str;
            _action += action;
        }

        public int CompareTo(MenuItem other)
        {
            return MenuString.CompareTo(other.MenuString);

        }
        public void DoWork(object obj)
        {
            _action?.Invoke(obj);
        }
    }

    public class MyMenu
    {
        List<MenuItem> _items = new List<MenuItem>();

        public void Add(string str, MyMenuDelegate d)
        {
            MenuItem item = _items.Find(x => x.MenuString == str);
            if (item == null)
            {
                _items.Add(new MenuItem(str, d));
            }
            else
            {
                item._action += d;
            }
        }

        public void Play_Again(Bets bets)
        {
            
            int choose = 0;
            

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Current Chip Count: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(bets.Chips);

                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Minimum Bet: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(20);

                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("                      Play again?");
                Console.CursorVisible = false;

                for (int i = 0; i < _items.Count; ++i)
                {
                    if (i == choose)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("                    ===> ");
                        //Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("                      ");
                    }
                    Console.WriteLine(_items[i].MenuString);
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        --choose;
                        break;
                    case ConsoleKey.DownArrow:
                        ++choose;
                        break;
                    case ConsoleKey.Enter:
                        _items[choose].DoWork(new object());
                        return;

                }
                if (choose < 0) choose = _items.Count - 1;
                if (choose >= _items.Count) choose = 0;
            }
        }


        public void Show()
        {
            int choose = 0;
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();

                Console.WriteLine();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                
                Console.WriteLine("                  \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2588  \u2588  \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588");
                Console.WriteLine("                  \u2588  \u2588  \u2588  \u2588  \u2588 \u2588\u2580  \u2588     \u2588  \u2588");
                Console.WriteLine("                  \u2588\u2588\u2588\u2588  \u2588  \u2588  \u2588\u2588\u2588   \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588");
                Console.WriteLine("                  \u2588     \u2588  \u2588  \u2588 \u2588\u2584  \u2588     \u2588\u2588\u2584 ");
                Console.WriteLine("                  \u2588     \u2588\u2588\u2588\u2588  \u2588  \u2588  \u2588\u2588\u2588\u2588  \u2588 \u2580\u2588");

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("            \u2588\u2588\u2588\u2588  \u2588     \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2580  \u2588\u2588\u2588\u2588");
                Console.WriteLine("            \u2588  \u2580  \u2588     \u2588  \u2588  \u2588  \u2580  \u2588  \u2580  \u2588  \u2588  \u2580");
                Console.WriteLine("            \u2588     \u2588     \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2588  \u2588   ");
                Console.WriteLine("            \u2588  \u2584  \u2588     \u2588  \u2588  \u2584  \u2588  \u2584  \u2588  \u2588  \u2588  \u2584");
                Console.WriteLine("            \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2588  \u2588  \u2588\u2588\u2588\u2588  \u2588\u2588\u2588\u2588  \u2588  \u2588\u2588\u2588\u2588");

                Console.WriteLine();
                Console.WriteLine();

                Console.CursorVisible = false;

                for (int i = 0; i < _items.Count; ++i)
                {
                    if (i == choose)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("                        ===> ");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("                          ");
                    }
                    Console.WriteLine(_items[i].MenuString);
                }

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        --choose;
                        break;
                    case ConsoleKey.DownArrow:
                        ++choose;
                        break;
                    case ConsoleKey.Enter:
                        _items[choose].DoWork(new object());
                        break;
                    case ConsoleKey.Escape:
                        goto END;
                }
                if (choose < 0) choose = _items.Count - 1;
                if (choose >= _items.Count) choose = 0;
            }
        END:;//label to exit;
        }
    }
}
