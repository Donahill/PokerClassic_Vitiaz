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

        public void Deal(ref Bets bets) {
            if (bets.Chips < 40)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nSorry. You don't have enough chips to start! \n");
                return;
            }
            bets.MinimumBet = 20;
            bets.Bet = 0;
            bets.AddBet(bets.MinimumBet); //Small Blind
            setUpDeck(); //создаём колоду карт и перемешиваем её
            getHand();
            sortCards();
            //System.Threading.Thread.Sleep(400);
            displayCards_Turn();
            bets.ShowChips();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("\n\n Your decision?\n");
            int number = 0;

            Console.Write(" Call - 1.\n");
            Console.Write(" Raise - 2.\n");
            Console.Write(" Fold - 3.\n");
            do
            {
                number = Convert.ToInt32(Console.ReadLine());


                switch (number)
                {

                    case 1:
                        bets.AddBet(bets.MinimumBet);

                        break;
                        
                    case 2:
                        int bet = 0;
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("\nEnter your bet: ");
                            bet = Convert.ToInt32(Console.ReadLine());
                            if ((bet > bets.Chips - bet) || (bet <= bets.MinimumBet))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nInvalid bet! ");
                            }
                        } while ((bet > bets.Chips - bet) || (bet <= bets.MinimumBet));

                        bets.AddBet(bet);
                        bets.MinimumBet = bet;

                        break;

                    case 3:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("\nPLAYER FOLD");
                        Console.WriteLine("COMPUTER WINS");
                        return;


                    default:
                        if ((number < 1) || (number > 3))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            
                            Console.Write("Invalid input!\n");
                            Console.ForegroundColor = ConsoleColor.Black;
                            
                        }
                        break;
                }
            } while ((number < 1) || (number > 3));
            //System.Threading.Thread.Sleep(400);
            displayCards_River();
            bets.ShowChips();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("\n\n Your decision?\n");
            number = 0;

            Console.Write(" Call - 1.\n");
            Console.Write(" Raise - 2.\n");
            Console.Write(" Fold - 3.\n");
            do
            {
                number = Convert.ToInt32(Console.ReadLine());


                switch (number)
                {

                    case 1:
                        bets.AddBet(bets.MinimumBet);

                        break;

                    case 2:
                        int bet = 0;
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("\nEnter your bet: ");
                            bet = Convert.ToInt32(Console.ReadLine());
                            if ((bet > bets.Chips) || (bet <= bets.MinimumBet))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nInvalid bet! ");
                            }
                        } while ((bet > bets.Chips) || (bet <= bets.MinimumBet));

                        bets.AddBet(bet);

                        break;

                    case 3:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("\nPLAYER FOLD");
                        Console.WriteLine("COMPUTER WINS");
                        return;


                    default:
                        if ((number < 1) || (number > 3))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write("Invalid input!\n");
                            Console.ForegroundColor = ConsoleColor.Black;

                        }
                        break;
                }
            } while ((number < 1) || (number > 3));
            //System.Threading.Thread.Sleep(400);
            displayCards();
            evaluateHands(ref bets);

            
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

        public void displayCards_Turn()
        {

            Console.Clear();
            int x = 0; //х позиция курсора. Её мы перемещаем влево-вправо
            int y = 1;// y позиция курсора. Её мы перемещаем вверх-вниз
            Card hidden = new Card();
            //отображаем руку компьютера
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("COMPUTER'S HAND");
            for (int i = 0; i < 3; i++)
            {

                DrawCards.DrawHiddenCard(hidden, x, y);
                
                x++;//движение вправо
            }
            y = 15; //опускаем курсор, для того, чтобы карты игрока вырисовывались ниже
            

            x = 0;
            Console.SetCursorPosition(x, 13);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPLAYER'S HAND");
            for (int i = 0; i < 3; i++)
            {
                DrawCards.DrawCard(playerHand[i], x, y);
                
                x++;//движение вправо
            }


        }

        public void displayCards_River()
        {

            Console.Clear();
            int x = 0; //х позиция курсора. Её мы перемещаем влево-вправо
            int y = 1;// y позиция курсора. Её мы перемещаем вверх-вниз
            Card hidden = new Card();
            //отображаем руку компьютера
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("COMPUTER'S HAND");
            for (int i = 0; i < 4; i++)
            {

                DrawCards.DrawHiddenCard(hidden, x, y);

                x++;//движение вправо
            }
            y = 15; //опускаем курсор, для того, чтобы карты игрока вырисовывались ниже


            x = 0;
            Console.SetCursorPosition(x, 13);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPLAYER'S HAND");
            for (int i = 0; i < 4; i++)
            {
                DrawCards.DrawCard(playerHand[i], x, y);

                x++;//движение вправо
            }


        }

        public void displayCards() {

            Console.Clear();
            int x = 0; //х позиция курсора. Её мы перемещаем влево-вправо
            int y = 1;// y позиция курсора. Её мы перемещаем вврех-вниз

            //отображаем руку компьютера
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("COMUTER'S HAND");
            for (int i = 0; i < 5; i++) {

                DrawCards.DrawCard(sortedComputerHand[i], x, y);
                
                x++;//движение вправо
            }
            y = 15; //опускаем курсор, для того, чтобы карты игрока вырисовывались ниже

            x = 0;
            Console.SetCursorPosition(x, 13);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPLAYER'S HAND");
            for (int i = 5; i < 10; i++)
            {

                DrawCards.DrawCard(sortedPlayerHand[i - 5], x, y);
              
                x++;//движение вправо
            }


        }

        public void evaluateHands(ref Bets bets)
        {

            //
            HandEvaluator playerHandEvaluator = new HandEvaluator(sortedPlayerHand);
            HandEvaluator computerHandEvaluator = new HandEvaluator(sortedComputerHand);

            //
            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            Console.ForegroundColor = ConsoleColor.Black;
            
            Console.WriteLine("\nComputer's Hand: " + computerHand);
            Console.WriteLine("\n\nPlayer's Hand: " + playerHand + "\n\n");

            //
            if (playerHand > computerHand) {

                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("PLAYER WINS");
                bets.Chips += (bets.Bet*2);


            }
            else if (playerHand < computerHand)
            {

                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("COMPUTER WINS");

            }
            else { //если руки равны

                //для начала рассмотрим у кого комбинация старше
                if (playerHandEvaluator.HandValues.Total > computerHandEvaluator.HandValues.Total)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("PLAYER WINS");
                    bets.Chips += (bets.Bet*2);
                }
                else if (playerHandEvaluator.HandValues.Total < computerHandEvaluator.HandValues.Total)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("COMPUTER WINS");
                }
                //если комбинации одинаковые - смотрим по старшей карте
                else if (playerHandEvaluator.HandValues.HighCard > computerHandEvaluator.HandValues.HighCard)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("PLAYER WINS");
                    bets.Chips += (bets.Bet*2);
                }
                else if (playerHandEvaluator.HandValues.HighCard < computerHandEvaluator.HandValues.HighCard)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("COMPUTER WINS");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("DRAW, YOU BOTH LOSERS!");
                    bets.Chips += (bets.Bet);
                }

            }

        }

    }
}
