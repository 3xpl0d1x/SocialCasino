using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasinoGame.InGameExceptions;

namespace CasinoGame.DicesData
{
    internal class Dice
    {
        private int _minT;
        private int _maxT;
        public int Number
        {
            get
            {
                var rand = new Random();
                return rand.Next(_min, _max);
            }
        }

        private int _min
        {
            get { return _minT; }
            set
            {
                if (value < 1 || value > int.MaxValue)
                {
                    throw new WrongDiceNumberException("----------WARNING----------\n" +
                    "The minimum value is out of permitted range \n" +
                    $"Given value: {value}\n" +
                    $"Permitted range: 1 - {int.MaxValue}\n");
                }
            }
        }
        private int _max
        {
            get { return _maxT; }
            set
            {
                if (value < 1 || value > int.MaxValue)
                {
                    throw new WrongDiceNumberException("----------WARNING----------\n" + 
                    "The maximum value is out of permitted range \n" +
                    $"Given value: {value}\n" +
                    $"Permitted range: 1 - {int.MaxValue}");
                }
            }
        }


        public Dice(int min, int max)
        {
            try
            {
                _minT = min;
                _maxT = max;
                _min = min;
                _max = max;
            }
            catch (WrongDiceNumberException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
