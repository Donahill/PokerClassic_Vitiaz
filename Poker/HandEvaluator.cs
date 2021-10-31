using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public enum Hand { 
    
        Nothing,
        OnePair,
        TwoPairs,
        Triple,
        Straight,
        Flash,
        FullHouse,
        Kare

    }

    public struct HandValue {

        public int Total { get; set; }
        public int HighCard { get; set; }

    }

    class HandEvaluator : Card
    {
        private int heartSum;
        private int diamondSum;
        private int clubSum;
        private int spadeSum;

        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(Card[] sortedHand) {

            heartSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadeSum = 0;
            cards = new Card[5];
            Cards = sortedHand;
            handValue = new HandValue();

        }
        public HandValue HandValues {

            get { return handValue; }
            set { handValue = value;  }

        }

        public Card[] Cards {

            get { return cards; }
            set { 
            
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];

            }
        }

        public Hand EvaluateHand() { 
        
            //получить количество каждой масти в руке
            getNumberOfSuit();
            if (Kare())
                return Hand.Kare;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flash())
                return Hand.Flash;
            else if (Straight())
                return Hand.Straight;
            else if (Triple())
                return Hand.Triple;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;
            //если ничего нет, то игрок с самой большей картой побеждает
            handValue.HighCard = (int)cards[4].MyValue;
            return Hand.Nothing;

        }

        private void getNumberOfSuit() { 
        
            foreach(var element in Cards){

                if (element.MySuit == Card.SUIT.HEARTS)
                    heartSum++;
                else if (element.MySuit == Card.SUIT.DIAMONDS)
                    diamondSum++;
                else if (element.MySuit == Card.SUIT.CLUBS)
                    clubSum++;
                else if (element.MySuit == Card.SUIT.SPADES)
                    spadeSum++; 
            }
        }

        private bool Kare() { 
        
            //если есть 4 одинаковые карты, добавим наминалы этих карт и последняя будет считаться по принципу старшенства
            if (cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[0].MyValue == cards[3].MyValue)
            {

                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue && cards[1].MyValue == cards[4].MyValue)
            {

                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[0].MyValue;
                return true;

            }
            else return false;

        }

        private bool FullHouse() { 
        
            //первые две карты и последние три карты имеют один номинал, или первые три карты и последние две карты имеют один номинал
            if ((cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue) ||
                (cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue))
            {

                handValue.Total = (int)(cards[0].MyValue) + (int)(cards[1].MyValue) +
                    (int)(cards[2].MyValue) + (int)(cards[3].MyValue) + (int)(cards[4].MyValue);
                return true;

            }
            else return false;

        }

        private bool Flash(){

            //должно быть пять карт одной масти
            if (heartSum == 5 || diamondSum == 5 || clubSum == 5 || spadeSum == 5)
            {
                //естественно, что у кого последняя карта в руке(или любая другая во время флеша) выше - тот и выиграл
                handValue.Total = (int)cards[4].MyValue;
                return true;
            }
            else return false;
        
        }

        private bool Straight(){
    
            //пять карт по порядку номиналов
            if (cards[0].MyValue + 1 == cards[1].MyValue && 
                cards[1].MyValue + 1 == cards[2].MyValue && 
                cards[2].MyValue + 1 == cards[3].MyValue && 
                cards[3].MyValue + 1 == cards[4].MyValue){
            
                //в случае ничьи по стритам - игрок с наибольшей картой побеждает
                handValue.Total = (int)cards[4].MyValue;
                return true;

            } else return false;

        }

        private bool Triple() { 
        
            //если 1,2,3 карты одного номинала или если 2,3,4 карты одного номинала, или если 3,4,5 карты одного номинала
            //третья карта - всегда часть тройки
            if ((cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue) ||
                (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue))
            {

                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue)
            {

                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[1].MyValue;
                return true;

            }
            else return false;
        }

        private bool TwoPairs() { 
        
            //если 1,2 и 3,4 - одинаковые, или если 2,3 и 4,5 - одинаковае, или 1,2 и 4,5 - одинаковые
            //в двух парах карта №2 - всегда часть одной пары, а карта №4 - всегда часть второй пары
            if (cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue)
            {

                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[0].MyValue == cards[1].MyValue && cards[3].MyValue == cards[4].MyValue)
            {

                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[2].MyValue;//
                return true;

            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue)
            {

                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[0].MyValue;//
                return true;

            }
            else return false;

        }
        private bool OnePair() {
        
            //две карты одного номинала 1,2 или 2,3 или 3,4 или 4,5

            if (cards[0].MyValue == cards[1].MyValue)
            {

                handValue.Total = (int)cards[0].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[1].MyValue == cards[2].MyValue)
            {

                handValue.Total = (int)cards[1].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[2].MyValue == cards[3].MyValue)
            {

                handValue.Total = (int)cards[2].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;

            }
            else if (cards[3].MyValue == cards[4].MyValue)
            {

                handValue.Total = (int)cards[3].MyValue * 4;
                handValue.HighCard = (int)cards[2].MyValue;
                return true;

            }
            else return false;

        }
        
    }
}
