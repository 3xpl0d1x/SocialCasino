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

        private int _playerScore;
        private int _dealerScore;

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
            Console.WriteLine("Your turn!");
            foreach (var dice in _dices) 
            {
                _playerScore += dice.Number;
            }
            Console.WriteLine($"Your score: {_playerScore}");

            Console.WriteLine("Dealer's turn!");
            foreach (var dice in _dices)
            {
                _dealerScore += dice.Number;
            }
            Console.WriteLine($"DEaler's score: {_dealerScore}");

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
