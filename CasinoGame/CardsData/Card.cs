using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame.CardsData
{
    internal class Card
    {
        readonly public CardEnums.CardPower Power;
        readonly public CardEnums.CardSuit Suit;

        public Card(CardEnums.CardPower power, CardEnums.CardSuit suit)
        {
            Power = power;
            Suit = suit;
        }

        public override string ToString()
        {
            return ($"{Power} {Suit}");
        }
    }
}
