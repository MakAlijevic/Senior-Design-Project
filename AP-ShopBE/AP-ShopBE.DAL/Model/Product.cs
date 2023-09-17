using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string productName { get; set; } = string.Empty;
        public string imagePath { get; set; } = string.Empty;
        public double price { get; set; } = 0.0;
        public string description { get; set; } = string.Empty;
        public bool isDeleted { get; set; } = false;
        public int quantity { get; set; } = 0;
        public Category Category { get; set; }
        public int categoryId { get; set; }
        public Condition Condition { get; set; }
        public int conditionId { get; set; }
        public Shipping Shipping { get; set; }
        public int shippingId { get; set; }
        public bool isModified { get; set; }
        [JsonIgnore]
        public List<CartItem> CartItems { get; set; }
        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
