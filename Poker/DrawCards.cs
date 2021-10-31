using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class DrawCards
    {

        //рисуем границы карт
        public static void DrawCardOutline(int xcoor, int ycoor) {

            Console.ForegroundColor = ConsoleColor.White;
 
            int x = xcoor * 12;
            int y = ycoor;

            Console.SetCursorPosition(x, y);
            Console.Write(" __________\n"); //верхняя граница карты

            for (int i = 0; i < 10; i++) {

                Console.SetCursorPosition(x, y + 1+i);

                if (i != 9)
                    Console.WriteLine("|          |");//левая и правая граница карт
                else
                    Console.WriteLine("|__________|");//нижняя граница карты
            }

        }
        //рисуем значение карты и её масть внутри границ карты
        public static void DrawCardSuitValue(Card card, int xcoor, int ycoor) { 
        
            char cardSuit = ' ';
            int x = xcoor * 12;
            int y = ycoor;

            //кодируем каждый байт массива из CodePage437 в мастиевый номинал
            //червы и бубни - красные, пики и кресты - чёрные;

            switch(card.MySuit) {

                case Card.SUIT.HEARTS:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 3 })[0];
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case Card.SUIT.DIAMONDS:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 4 })[0];
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case Card.SUIT.CLUBS:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 5 })[0];
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;

                case Card.SUIT.SPADES:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 6 })[0];
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
        }

            //отобразить закодированный символ и номинал карты
            Console.SetCursorPosition(x+5, y+5);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x+4, y+7);
            Console.Write(card.MyValue);
        }

    }
}
