using CasinoGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            Casino.Instance.StartGame();
        }
    }
}
