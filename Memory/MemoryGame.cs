using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Memory
{
    //Game logic class
    public class MemoryGame
    {
        // timer variables
        public System.Timers.Timer aTimer;
        public int secondsCounter = 0;
        public int turns = 0;

        //Card variables
        public Card[] cards;
        static Card[] ChosenCards = new Card[2];
        static char[] chars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private string[] urls;
        
        //Empty constructor for Console app
        public MemoryGame() : this(null)
        {

        }
        //Constructor for MAUI app
        public MemoryGame(string[] urls)
        {
            this.urls = urls;
        }

        //start Timer
        public void Start()
        {
            SetTimer();
        }

        //Initialize timer
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        //Stop timer
        public void stopTimer()
        {
            aTimer.Stop();
            aTimer.Dispose();
        }

        //Increase Seconds counter event
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            secondsCounter++;
        }

        //Fill the array with card objects
        public void FillCardList(int Amount)
        {
            cards = new Card[Amount];
            for (int i = 0; i < Amount; i++)
            {
                if ((i+1) % 2 == 0)
                {
                    if(urls == null)
                    {
                        cards[i] = new Card(i / 2, chars[i / 2]);
                        cards[i - 1] = new Card(i / 2, chars[i / 2]);
                    }
                    else
                    {
                        cards[i] = new Card(i / 2, chars[i / 2], urls[i/2]);
                        cards[i - 1] = new Card(i / 2, chars[i / 2], urls[i / 2]);
                    }
                    
                }
            }
            ShuffleCards();
        }

        //Shuffle the cards in the cards array
        public void ShuffleCards()
        {
            Random random = new Random();
            for (int i = 0; i < cards.Length; i++)
            {
                int randomIndex = random.Next(0, cards.Length);
                Card temp = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }

        //Method for flipping the selected card
        public void FlipCard(int card)
        {
            cards[card - 1].Flip();
            turns++;
            if (ChosenCards[0] == null)
            {
                ChosenCards[0] = cards[card - 1];
            }
            else
            {
                ChosenCards[1] = cards[card - 1];
            }
            
        }

        //Method for matching the 2 selected cards
        public bool MatchCards()
        {
            if (TwoCardsSelected())
            {
                if (ChosenCards[0].CardNumber == ChosenCards[1].CardNumber)
                {
                    ChosenCards[0].Match();
                    ChosenCards[1].Match();
                    ChosenCards[0] = null;
                    ChosenCards[1] = null;
                    return true;
                }
            }
            return false;
        }

        //Check if 2 cards have been selected
        public bool TwoCardsSelected()
        {
            return (ChosenCards[0] != null && ChosenCards[1] != null);
        }

        //Flip the 2 selected cards back
        public void reflipChosenCards()
        {
            ChosenCards[0].IsFlipped = false;
            ChosenCards[1].IsFlipped = false;
            ChosenCards[0] = null;
            ChosenCards[1] = null;
        }

        //Check if all cards are matched
        public bool IsFinished()
        {
            foreach (Card card in cards)
            {
                if (!card.IsMatched)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
