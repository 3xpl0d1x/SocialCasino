using CasinoGame.DicesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame.GameMechanics
{
    internal class DiceGame : CasinoGameBase
    {
        private int _diceAmount;
        private int _minimumValue;
        private int _maximumValue;

        private long _playerScore;
        private long _dealerScore;

        private List<Dice> _dices;
        public DiceGame(int n, int minimum, int maximum) 
        {
            _diceAmount = n;
            _minimumValue = minimum;
            _maximumValue = maximum;

            FactoryMethod();
        }

        public override void PlayGame()
        {
            Console.Clear();
            Console.WriteLine("Your turn!");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.Clear();
            foreach (var dice in _dices) 
            {
                long n = dice.Number;
                _playerScore += n;
                Console.WriteLine($"+{n}");
            }
            Console.WriteLine($"Your score: {_playerScore}");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.Clear();

            Console.WriteLine("Dealer's turn!");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            foreach (var dice in _dices)
            {
                long n = dice.Number;
                _dealerScore += n;
                Console.WriteLine($"+{n}");
            }
            Console.WriteLine($"Dealer's score: {_dealerScore}");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.Clear();

            if (_playerScore > _dealerScore) { OnWinInvoke(); }
            else if (_playerScore < _dealerScore) { OnLosseInvoke(); }
            else { OnDrawInvoke(); }

        }

        protected override void FactoryMethod()
        {
            _playerScore = 0;
            _dealerScore = 0;

            _dices = new List<Dice>();
            for (int i = 0; i < _diceAmount; i++) 
            {
                _dices.Add(new Dice(_minimumValue, _maximumValue));
            }
        }
    }
}
