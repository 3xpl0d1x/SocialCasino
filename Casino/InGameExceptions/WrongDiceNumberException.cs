using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.InGameExceptions
{
    internal class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(string message) : base(message) {}
    }
}
