using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string roleType { get; set; } = "user";
        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
