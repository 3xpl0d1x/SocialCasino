using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CasinoGame.ProfileManager
{
    internal class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("balance")]
        public int Balance { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }
        [JsonPropertyName("losses")]
        public int Losses { get; set; }
        [JsonPropertyName("draws")]
        public int Draws { get; set; }
    }

    internal class UserList()
    {
        [JsonPropertyName("users")]
        public List<User> Users { get; set; } = new List<User>();
    }
}
