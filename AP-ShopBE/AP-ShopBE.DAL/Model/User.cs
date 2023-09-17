using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string gender { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public bool isCartNotified { get; set; } = false;
        [JsonIgnore]
        public Role Role { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
        public int CountryId { get; set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; }
        [JsonIgnore]
        public List<CartItem> CartItems { get; set; } 
    }
}
