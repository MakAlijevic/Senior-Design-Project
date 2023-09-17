using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Condition
    {
        public int Id { get; set; }
        public string conditionType { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
