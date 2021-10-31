using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class DeckOfCards : Card
    {
        const int NUMBER_OF_CARDS = 52; //количество всех карт
        private Card[] deck; //массив всех игральных карт

        public DeckOfCards() {

            deck = new Card[NUMBER_OF_CARDS];

        }

        public Card[] getDeck { get {return deck;} } //вызов текущей колоды

        //создаём колоду из 52-х карт: 13 Значений, 4 масти

        public void setUpDeck() {

            int i = 0;
            
            foreach (SUIT s in Enum.GetValues(typeof(SUIT))) {

                foreach (VALUE v in Enum.GetValues(typeof(VALUE)))
                {

                    deck[i] = new Card { MySuit = s, MyValue = v };
                    i++;

                }

            }

            ShuffleCards();

        }

        public void ShuffleCards() {

            Random rand = new Random();
            Card temp;
            //Помешаем карты

            for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++) {

                for (int i = 0; i < NUMBER_OF_CARDS; i++) { 
                
                    //Махнём карты местами
                    int secondCardIndex = rand.Next(7);
                    temp = deck[i];
                    deck[i] = deck[secondCardIndex];
                    deck[secondCardIndex] = temp;

                }
            
            }

        }

    }
}
