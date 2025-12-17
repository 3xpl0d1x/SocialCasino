using CasinoGame.SaveLoadData;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CasinoGame.ProfileManager
{
    internal class UserManager
    {
        private readonly string _dataFolderPath;
        private readonly string _jsonFileName = "Users.json";
        private UserList _userList;
        private JsonSerializerOptions _jsonOptions;

        private FileSystemSaveLoadService _fileSaveLoadService;

        public UserManager(string path)
        {
            _dataFolderPath = path;
            _fileSaveLoadService = new FileSystemSaveLoadService(path);
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            InitializeFile();
            LoadUsers();
        }

        private void InitializeFile()
        {
            string filePath = Path.Combine(_dataFolderPath, _jsonFileName);
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Json file not found! Creating new one!");
                    CreateInitialFile();
                }
                else
                {
                    var fileInfo = new FileInfo(filePath);

                    if (fileInfo.Length == 0)
                    {
                        Console.WriteLine("File is empty! Filling!");
                        CreateInitialFile();
                    }
                    else
                    {
                        string content = File.ReadAllText(filePath);
                        if (String.IsNullOrEmpty(content))
                        {
                            Console.WriteLine($"File contains only spaces");
                            CreateInitialFile();
                        }
                    }
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Failed to initialize a file: {ex.Message}");
                Console.WriteLine("Attempt to create a new file!");
                CreateInitialFile();
            }
        }

        private void CreateInitialFile()
        {
            try
            {
                _userList = new UserList()
                {
                    Users = new List<User>()
                    {
                        new User()
                        {
                            Id = 1,
                            Name = "admin",
                            Password = "admin",
                            Balance = 1500,
                            Wins = 0,
                            Losses = 0,
                            Draws = 0,
                        },
                        new User()
                        {
                            Id = 2,
                            Name = "guest",
                            Password = "guest",
                            Balance = 1500,
                            Wins = 0,
                            Losses = 0,
                            Draws = 0,
                        }
                    }
                };
                SaveUsers();
                Console.WriteLine("Created a guest and admin profiles");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create a file: {ex.Message}");
                _userList = new UserList();
            }
        }

        public void LoadUsers()
        {
            try
            {
                var json = _fileSaveLoadService.LoadData(_jsonFileName);
                _userList = JsonSerializer.Deserialize<UserList>(json, _jsonOptions) ?? new UserList();
            }
            catch (Exception ex)
            {
                _userList = new UserList();
                Console.WriteLine($"Failed to load a file: {ex.Message}");
            }
        }

        public void SaveUsers()
        {
            try
            {
                var json = JsonSerializer.Serialize(_userList, _jsonOptions);
                _fileSaveLoadService.SaveData(json, _jsonFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save file: {ex.Message}");
            }
        }
        public List<User> GetAllUsers()
        {
            return _userList.Users;
        }

        public User? GetUserByID(int id)
        {
            return _userList.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool AddUser(string name, string password, int balance, int wins, int losses, int draws)
        {
            try
            {
                int newID = _userList.Users.Count > 0 ? _userList.Users.Max(u => u.Id) + 1: 1;
                var user = new User
                {
                    Name = name,
                    Password = password,
                    Balance = balance,
                    Wins = wins,
                    Losses = losses,
                    Draws = draws,
                    Id = newID
                };
                _userList.Users.Add(user);

                Console.WriteLine($"User created with id: {newID}");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create a user: {ex.Message}");
                return false;
            }

        }

        public bool DeleteUser(int id)
        {
            var user = GetUserByID(id);
            if (user == null)
            {
                Console.WriteLine($"User with id {id} not found");
                return false;
            }
            try
            {
                _userList.Users.Remove(user);
                SaveUsers();
                Console.WriteLine($"User with id {id} deleted");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete a user with id {id}: {ex.Message}");
                return false;
            }
        }

        public bool UpdateUserName(int id, string newName)
        {
            var user = GetUserByID(id);
            if (user == null)
            {
                Console.WriteLine($"User with id {id} not found");
                return false;
            }
            try
            {
                var oldName = user.Name;
                user.Name = newName;
                SaveUsers();
                Console.WriteLine($"User with id {id} changed his name form {oldName} to {newName}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to change {id}'s name: {ex.Message}");
                return false;
            }
        }
        public bool UpdateUserPassword(int id, string newPassword, string passwordCheck)
        {
            var user = GetUserByID(id);
            if (user == null)
            {
                Console.WriteLine($"User with id {id} not found");
                return false;
            }

            try
            {
                if(passwordCheck != user.Password) { Console.WriteLine("Incorrect password"); return false; }
                user.Password = newPassword;
                SaveUsers();
                Console.WriteLine($"Password changed!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to change {id}'s password: {ex.Message}");
                return false;
            }
        }
        public bool UpdateUserBalance(int id, int newBalance)
        {
            var user = GetUserByID(id);
            if (user == null)
            {
                Console.WriteLine($"User with id {id} not found");
                return false;
            }
            try
            {
                var oldBalance = user.Balance;
                user.Balance = newBalance;
                SaveUsers();
                Console.WriteLine($"Changed balance of user {id} from {oldBalance}p to {newBalance}p");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to change {id}'s balance: {ex.Message}");
                return false;
            }
        }

        public bool AddResultToUser(int id, bool isWin = false, bool isLoss = false, bool isDraw = false)
        {
            var user = GetUserByID(id);
            if (user == null)
            {
                Console.WriteLine($"User with id {id} not found");
                return false;
            }
            try
            {
                if (isWin) user.Wins++;
                if (isLoss) user.Losses++;
                if (isDraw) user.Draws++;
                SaveUsers();
                string result = isWin ? "Win" : isLoss ? "Loss" : "Draw";
                Console.WriteLine($"{user.Name} got a result: {result}!");
                Console.WriteLine("Current stats:");
                Console.WriteLine($"Wins: {user.Wins}\nLosses: {user.Losses}\nDraws: {user.Draws}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add result to {id}: {ex.Message}");
                return false;
            }
        }

        public void GetUserStats(int id)
        {
            var user = GetUserByID(id);
            if (user == null)
            {
                Console.WriteLine($"User with id {id} not found");
                return;
            }

            int totalGames = user.Wins + user.Losses + user.Draws;

            double winRate = Convert.ToDouble(totalGames) > 0 ? Convert.ToDouble(user.Wins) / Convert.ToDouble(totalGames) : 0;

            Console.WriteLine("----------User Stats----------");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Id: {user.Id}");
            Console.WriteLine($"Balance: {user.Balance}");
            Console.WriteLine($"Total games: {totalGames}");
            Console.WriteLine($"Wins: {user.Wins}");
            Console.WriteLine($"Losses: {user.Losses}");
            Console.WriteLine($"Draws: {user.Draws}");
            Console.WriteLine($"Win rate: {winRate}");
            Console.WriteLine("------------------------------");
        }

        public void DisplayAllUsers()
        {
            if (_userList.Users.Count == 0) { Console.WriteLine("User list is empty"); return; }

            Console.WriteLine($"Users {_userList.Users.Count}");
            Console.WriteLine("------------------------------");
            foreach (var user in _userList.Users)
            {
                Console.WriteLine($"ID: {user.Id} Name: {user.Name} W/L/D: {user.Wins}/{user.Losses}/{user.Draws}");
            }
            Console.WriteLine("------------------------------");
        }

        public bool UserVerification(User user)
        {
            if (user == null)
            {
                Console.WriteLine("User not found!");
                return false;
            }
            Console.WriteLine("Enter password");
            string? password = Console.ReadLine();
            if( password != user.Password)
            {
                Console.Clear();
                Console.WriteLine("Wrong password!");
                Console.WriteLine("Press 'Enter' button to continue");
                while (Console.ReadKey().Key != ConsoleKey.Enter) ;
                return false;
            }
            Console.Clear();
            Console.WriteLine("Logged in");
            Console.WriteLine("Press 'Enter' button to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            return true;

        }
        public List<User> SearchUsersByName(string searchTerm)
        {
            return _userList.Users.Where(u => u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        } 


    }
}
