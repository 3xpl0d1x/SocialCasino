using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.CardsData
{
    internal class Card
    {
        readonly CardEnums.CardPower Power;
        readonly CardEnums.CardSuit Suit;

        public Card (CardEnums.CardPower power, CardEnums.CardSuit suit)
        {
            Power = power;
            Suit = suit;
        }
    }
}
