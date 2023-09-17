using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Order
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int userId { get; set; }
        public double totalPrice { get; set; }
        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }
        public DateTime dateTime { get; set; }
    }
}
