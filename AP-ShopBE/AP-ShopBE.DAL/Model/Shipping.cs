using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Shipping
    {
        public int Id { get; set; }
        public string shippingType { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
