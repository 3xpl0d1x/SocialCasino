using CasinoGame.GameMechanics;
using CasinoGame.ProfileManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame.Core
{
    internal class Casino : IGame
    {
        private string _root = Path.GetFullPath(Directory.GetCurrentDirectory() + "//..//..//..//");
        private string _usersDataPath;
        private UserManager _userManager;
        private User _currentUser;
        private BlackJack _blackJack;
        private DiceGame _dicegame;

        private bool pressedLeave = false;
        private bool _win;
        private bool _losse;
        private bool _draw;

        static public Casino Instance { get; private set; } = new Casino();

        public void StartGame()
        {
            _usersDataPath = Path.Combine(_root, "DataFolder");
            _userManager = new UserManager(_usersDataPath);


            SayHello();
            while (!LoggingIn());
            Console.Clear();
            Console.WriteLine($"Hello {_currentUser.Name}! Your balance is {_currentUser.Balance}p!");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            while(!ActionChoose());
        }

        private void SayHello()
        {
            Console.Clear();
            Console.WriteLine("Welcome to our casino!");
            Console.WriteLine("Here you can find BlackJack and dices!\nInterested?\nLet's get started");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;

        }

        private bool LoggingIn()
        {
            Console.Clear();
            Console.WriteLine("Log in or create new profile");
            Console.WriteLine("1. Sign up");
            Console.WriteLine("2. Log in");
            string? choice = Console.ReadLine();
            if (choice == null)
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }
            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }


            if (choice == "1")
            {
                while(!CreateDefaultUser());
                Console.WriteLine("Now you can log in!");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }

            Console.Clear();
            _userManager.DisplayAllUsers();
            while (!UserLogIn())
            {
                Console.Clear();
                _userManager.DisplayAllUsers();
            } 

            return true;
        }
        private bool UserLogIn()
        {
            Console.WriteLine("Enter your profile's ID:");
            string? userIDReader = Console.ReadLine();
            if (userIDReader == null) 
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (!userIDReader.All(char.IsDigit) || userIDReader == "") 
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            int userId = Convert.ToInt32(userIDReader);
            if (_userManager.GetUserByID(userId) == null)
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }

            if (!_userManager.UserVerification(_userManager.GetUserByID(userId)))
            {
                return false;
            }

            _currentUser = _userManager.GetUserByID(Convert.ToInt32(userId));
            return true;

        }
        private bool CreateDefaultUser()
        {
            Console.Clear();
            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();

            if (username is not string)
            {
                Console.Clear();
                Console.WriteLine("Incorrect username");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (username == "")
            {
                Console.Clear();
                Console.WriteLine("Incorrect username");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            Console.Clear();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();

            if (password is not string)
            {
                Console.Clear();
                Console.WriteLine("Incorrect password");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (password == "")
            {
                Console.Clear();
                Console.WriteLine("Incorrect password");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            Console.Clear();
            Console.WriteLine("Repeat password:");
            var checkPassword = Console.ReadLine();
            if (checkPassword is not string)
            {
                Console.Clear();
                Console.WriteLine("Wrong!");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (checkPassword == "")
            {
                Console.Clear();
                Console.WriteLine("Wrong!");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (checkPassword != password)
            {
                Console.Clear();
                Console.WriteLine("Wrong!");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }

            _userManager.AddUser(username, password, 1500, 0, 0, 0);
            _userManager.SaveUsers();
            _userManager.LoadUsers();

            return true;
        }

        private bool ActionChoose()
        {
            Console.Clear();
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Profile settings");
            Console.WriteLine("3. Change user");
            Console.WriteLine("4. Leave");
            string? choice = Console.ReadLine();
            if (choice == null)
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if(choice == "1") { while (!GameChoose()) ; return false; }


            return true;
        }

        private bool GameChoose()
        {
            Console.Clear();
            Console.WriteLine("1. BlackJack");
            Console.WriteLine("2. Dices");
            Console.WriteLine("3. Back to main menu");
            string? choice = Console.ReadLine();
            if (choice == null)
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }
            if (choice != "1" && choice != "2" && choice != "3")
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }
            if (choice == "1") { while (!BlackJackCore()) ; return false; }

            return true;
        }

        private bool BlackJackCore()
        {
            _win = false;
            _losse = false;
            _draw = false;

            Console.Clear();
            Console.WriteLine("Please, enter your bet (minimum value is 100)\n" +
                $"Your current balance is {_currentUser.Balance}:");
            string? bet = Console.ReadLine();
            if (bet == null)
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (!bet.All(char.IsDigit) || bet == "")
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (Convert.ToInt32(bet) < 100 || Convert.ToInt32(bet) > _currentUser.Balance)
            {
                Console.WriteLine("Not enough points or the bet is too low");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            Console.WriteLine("Please, enter the amount of cards you want to generate (from 25 to 1000):");
            string? amount = Console.ReadLine();
            if (amount == null)
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            if (!amount.All(char.IsDigit) || amount == "")
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }

            _blackJack = new BlackJack(Convert.ToInt32(amount));
            _blackJack.OnWin += OnWinHappened;
            _blackJack.OnLosse += OnLosseHappened;
            _blackJack.OnDraw += OnDrawHappened;
            _blackJack.PlayGame();
            if (_win)
            {
                Console.WriteLine("Congrats! You won!");
                _userManager.UpdateUserBalance(_currentUser.Id, _currentUser.Balance + Convert.ToInt32(bet));
                Console.WriteLine($"Now you have {_currentUser.Balance}p");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }

            else if (_losse)
            {
                Console.WriteLine("You lost! Loser =)");
                _userManager.UpdateUserBalance(_currentUser.Id, _currentUser.Balance - Convert.ToInt32(bet));
                Console.WriteLine($"Now you have {_currentUser.Balance}p");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }

            else if (_draw)
            {
                Console.WriteLine("Draw!");
                Console.WriteLine($"You still have {_currentUser.Balance}p");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            }

            _userManager.AddResultToUser(_currentUser.Id, _win, _losse, _draw);

            _blackJack.OnWin -= OnWinHappened;
            _blackJack.OnLosse -= OnLosseHappened;
            _blackJack.OnDraw -= OnDrawHappened;
            return true;
        }

        private void OnWinHappened()
        {
            _win = true;
        }
        private void OnLosseHappened()
        {
            _losse = true;
        }
        private void OnDrawHappened() 
        {
            _draw = true;
        }
    }
}
