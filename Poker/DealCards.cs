using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class DealCards : DeckOfCards
    {
        private Card[] playerHand;
        private Card[] computerHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedComputerHand;

        public DealCards() { 
        
            playerHand = new Card[5];
            computerHand = new Card[5];
            sortedPlayerHand = new Card[5];
            sortedComputerHand = new Card[5];
        
        }

        public void Deal() {

            setUpDeck(); //создаём колоду карт и перемешиваем её
            getHand();
            sortCards();
            displayCards();
            evaluateHands();
            
        }

        public void getHand() { 
        
            //5 карт игроку
            for (int i = 0; i < 5; i++)
            {
                playerHand[i] = getDeck[i];
            }
            //5 карт компьютеру
            for (int i = 5; i < 10; i++)
            {
                computerHand[i - 5] = getDeck[i];
            }

        }

        public void sortCards() {

            var queryPlayer = from hand in playerHand
                              orderby hand.MyValue
                              select hand;

            var queryComputer = from hand in computerHand
                              orderby hand.MyValue
                              select hand;

            var index = 0;
            foreach (var element in queryPlayer.ToList()) {
            
                sortedPlayerHand[index] = element;
                index++;

            }

            index = 0;
            foreach (var element in queryComputer.ToList())
            {

                sortedComputerHand[index] = element;
                index++;

            }
        }

        public void displayCards() {

            Console.Clear();
            int x = 0; //х позиция курсора. Её мы перемещаем влево-вправо
            int y = 1;// y позиция курсора. Её мы перемещаем вврех-вниз

            //отображаем руку игрока
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("PLAYER'S HAND");
            for (int i = 0; i < 5; i++) {

                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerHand[i], x, y);
                x++;//движение вправо
            }
            y = 15; //опускаем курсор, для того, чтобы карты компьютера вырисовывались ниже

            x = 0;
            Console.SetCursorPosition(x, 14);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("COMPUTER'S HAND");
            for (int i = 5; i < 10; i++)
            {

                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedComputerHand[i-5], x, y);
                x++;//движение вправо
            }


        }

        public void evaluateHands()
        {

            //
            HandEvaluator playerHandEvaluator = new HandEvaluator(sortedPlayerHand);
            HandEvaluator computerHandEvaluator = new HandEvaluator(sortedComputerHand);

            //
            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            Console.WriteLine("\n\n\n\n\nPlayer's Hand: " + playerHand);
            Console.WriteLine("\n\n\n\n\nComputer's Hand: " + computerHand);

            //
            if (playerHand > computerHand) {

                Console.WriteLine("PLAYER WINS");

            }
            else if (playerHand < computerHand)
            {

                Console.WriteLine("COMPUTER WINS");

            }
            else { //если руки равны
            
                //для начала рассмотрим у кого комбинация старше
                if (playerHandEvaluator.HandValues.Total > computerHandEvaluator.HandValues.Total)
                    Console.WriteLine("PLAYER WINS");
                else if (playerHandEvaluator.HandValues.Total < computerHandEvaluator.HandValues.Total)
                    Console.WriteLine("COMPUTER WINS");
                //если комбинации одинаковые - смотрим по старшей карте
                else if (playerHandEvaluator.HandValues.HighCard > computerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("PLAYER WINS");
                else if (playerHandEvaluator.HandValues.HighCard < computerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("COMPUTER WINS");
                else
                    Console.WriteLine("DRAW, YOU BOTH LOSERS!");

            }

        }

    }
}
