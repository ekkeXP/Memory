using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    //Card Class
    public class Card
    {
        //Front and back of card using Path to file
        public string FrontCardURL;
        private string _backCardURL = "backside_card.jpg";
        //Number associated with pair
        public int CardNumber;
        //Bool for flipping card
        public bool IsFlipped;
        //bool for matching card with pair
        public bool IsMatched;
        //char for console app visual
        private char _symbol;

        //Constructor for Console app
        public Card(int cardNumber, char symbol)
        {
            this.CardNumber = cardNumber;
            _symbol = symbol;
            IsMatched = false;
            IsFlipped = false;
        }
        //Constructor for MAUI app
        public Card(int cardNumber, char symbol, string FrontURL) : this(cardNumber, symbol)
        {
            FrontCardURL = FrontURL;
        }

        //Flip this card
        public void Flip()
        {
            IsFlipped = !IsFlipped;
        }
        //Set matched = true
        public void Match()
        {
            IsMatched = true;
        }
        //Return the symbol
        public string GetSymbol()
        {
            if (IsFlipped)
            {
                return _symbol.ToString();
            }
            else
            {
                return "#";
            }
        }
        //return the url of the card face
        public string GetCardFace()
        {
            if (IsFlipped)
            {
                return FrontCardURL;
            }
            else
            {
                return _backCardURL;
            }
        }
    }
}
