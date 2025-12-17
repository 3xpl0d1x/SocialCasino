using CasinoGame.CardsData;
using CasinoGame.InGameExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame.GameMechanics
{
    internal class BlackJack : CasinoGameBase
    {
        private List<Card> _cards;
        private List<Card> _dealer;
        private List<Card> _player;

        private Queue<Card> _deck;

        private int _playerScore = 0;
        private int _dealerScore = 0;

        private int _staticAmount = 0;
        private int _cardsAmount
        {
            get
            {
                return _staticAmount;
            }
            set
            {
                if (value > 1000 || value < 25)
                {
                    isError = true;
                    throw new WrongBlackJackCardsAmountException("----------WARNING----------\n" +
                    "The given amount of cards is out of permitted range\n" +
                    $"Given value: {value}\n" +
                    $"Permitted range: 25 - 1000");
                }
                _staticAmount = value;


            }

        }

        private bool isEnded = false;
        private bool isError = false;


        public override void PlayGame()
        {
            if (isError)
            {
                return;
            }
            Shuffle();
            StarterCards();
            CheckWin();
            while (!isEnded) 
            { 
                SingleCards();
                CheckWin();
            }
        }

        private void Shuffle()
        {
            Console.Clear();
            Console.WriteLine("Shuffling...");

            for (int i = 0; i < _cards.Count; i++)
            {
                var random = new Random();
                Card temp;
                temp = _cards[i];
                int a = random.Next(0, _cards.Count);
                _cards[i] = _cards[a];
                _cards[a] = temp;

            }
            foreach (Card card in _cards)
            {
                _deck.Enqueue(card);
            }
            Console.WriteLine("Completed!");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.Clear();
        }

        private void StarterCards()
        {
            Console.Clear();
            Console.WriteLine("Deck distribution...");
            _player.Add(_deck.Dequeue());
            _player.Add(_deck.Dequeue());
            _dealer.Add(_deck.Dequeue());
            _dealer.Add(_deck.Dequeue());
            Console.WriteLine("Cards are distributed!");
            Console.WriteLine("Your hand:");
            foreach (Card card in _player)
            {

                Console.WriteLine($"\t{card}");
            }
            Console.WriteLine("Dealer's hand:");
            foreach (Card card in _dealer)
            {
                Console.WriteLine($"\t{card}");
            }
        }
        private void SingleCards()
        {
            Console.WriteLine("Deck distribution...");
            _player.Add(_deck.Dequeue());
            _dealer.Add(_deck.Dequeue());
            Console.WriteLine("Cards are distributed!");
            Console.WriteLine("Your cards:");
            foreach (Card card in _player)
            {

                Console.WriteLine($"\t{card}");
            }
            Console.WriteLine("Dealer's cards:");
            foreach (Card card in _dealer)
            {
                Console.WriteLine($"\t{card}");
            }
        }

        private void CheckWin()
        {


            Type powerEnum = typeof(CardEnums.CardPower);
            Type suitEnum = typeof(CardEnums.CardSuit);

            Array powers = powerEnum.GetEnumValues();
            Array suits = suitEnum.GetEnumValues();

            int temp1 = _playerScore;
            int temp2 = _dealerScore;

            foreach (Card card in _player)
            {
                switch (card.Power)
                {
                    case CardEnums.CardPower.Two:
                        _playerScore += 2;
                        break;
                    case CardEnums.CardPower.Three:
                        _playerScore += 3;
                        break;
                    case CardEnums.CardPower.Four:
                        _playerScore += 4;
                        break;
                    case CardEnums.CardPower.Five:
                        _playerScore += 5;
                        break;
                    case CardEnums.CardPower.Six:
                        _playerScore += 6;
                        break;
                    case CardEnums.CardPower.Seven:
                        _playerScore += 7;
                        break;
                    case CardEnums.CardPower.Eight:
                        _playerScore += 8;
                        break;
                    case CardEnums.CardPower.Nine:
                        _playerScore += 9;
                        break;
                    case CardEnums.CardPower.Ten:
                        _playerScore += 10;
                        break;
                    case CardEnums.CardPower.Jack:
                        _playerScore += 10;
                        break;
                    case CardEnums.CardPower.Queen:
                        _playerScore += 10;
                        break;
                    case CardEnums.CardPower.King:
                        _playerScore += 10;
                        break;
                    case CardEnums.CardPower.Ace:
                        _playerScore += 11;
                        break;
                    default: break;
                }
            }
            _playerScore -= temp1;

            foreach (Card card in _dealer)
            {
                switch (card.Power)
                {
                    case CardEnums.CardPower.Two:
                        _dealerScore += 2;
                        break;
                    case CardEnums.CardPower.Three:
                        _dealerScore += 3;
                        break;
                    case CardEnums.CardPower.Four:
                        _dealerScore += 4;
                        break;
                    case CardEnums.CardPower.Five:
                        _dealerScore += 5;
                        break;
                    case CardEnums.CardPower.Six:
                        _dealerScore += 6;
                        break;
                    case CardEnums.CardPower.Seven:
                        _dealerScore += 7;
                        break;
                    case CardEnums.CardPower.Eight:
                        _dealerScore += 8;
                        break;
                    case CardEnums.CardPower.Nine:
                        _dealerScore += 9;
                        break;
                    case CardEnums.CardPower.Ten:
                        _dealerScore += 10;
                        break;
                    case CardEnums.CardPower.Jack:
                        _dealerScore += 10;
                        break;
                    case CardEnums.CardPower.Queen:
                        _dealerScore += 10;
                        break;
                    case CardEnums.CardPower.King:
                        _dealerScore += 10;
                        break;
                    case CardEnums.CardPower.Ace:
                        _dealerScore += 11;
                        break;
                    default: break;
                }
            }

            _dealerScore -= temp2;

            Console.WriteLine($"Your score: {_playerScore}");
            Console.WriteLine($"Dealer's score: {_dealerScore}");

            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.Clear();

            if ((_dealerScore > 21 && _playerScore > 21) || (_dealerScore == 21 && _playerScore == 21)) { isEnded = true; OnDrawInvoke(); }
            else if ((_dealerScore > _playerScore || _playerScore > 21) && _dealerScore <= 21) { isEnded = true; OnLosseInvoke(); }
            else if ((_playerScore > _dealerScore || _dealerScore > 21) && _playerScore <= 21) { isEnded = true; OnWinInvoke(); }
            else if ((_playerScore == _dealerScore))
            {
                Console.WriteLine("Next cards!");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ; Console.Clear();
            }
        }
        protected override void FactoryMethod()
        {
            _cards = new List<Card>();
            _deck = new Queue<Card>();
            _dealer = new List<Card>();
            _player = new List<Card>();

            Random random = new Random();
            Type powerEnum = typeof(CardEnums.CardPower);
            Type suitEnum = typeof(CardEnums.CardSuit);

            Array powers = powerEnum.GetEnumValues();
            Array suits = suitEnum.GetEnumValues();

            int tempest = _cardsAmount;
            for (int i = 0; i < tempest; i++)
            {
                int powerIndex = random.Next(powers.Length);
                int suitsIndex = random.Next(suits.Length);

                _cards.Add(new Card((CardEnums.CardPower)powers.GetValue(powerIndex), (CardEnums.CardSuit)suits.GetValue(suitsIndex)));

            }
            Console.WriteLine($"Generated {_cards.Count} cards");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }
        public BlackJack(int n)
        {


            try
            {
                _cardsAmount = n;
                _staticAmount = n;
                isEnded = false;
            }
            catch (WrongBlackJackCardsAmountException ex) { Console.WriteLine(ex.Message); }

            FactoryMethod();
        }
    }
}