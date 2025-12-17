using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame.InGameExceptions
{
    internal class WrongBlackJackCardsAmountException : Exception
    {
        public WrongBlackJackCardsAmountException(string message) : base(message) { }
    }
}
