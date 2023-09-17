using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string countryName { get; set; } = string.Empty;
        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
