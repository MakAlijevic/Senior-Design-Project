namespace AP_ShopBE.BLL.DTO
{
    public class CreateProductDto
    {
        public string productName { get; set; } = "productName";
        public string imagePath { get; set; } = "imagePath";
        public double price { get; set; } = 0.0;
        public string description { get; set; } = "description";
        public int quantity { get; set; } = 0;
        public int categoryId { get; set; } = 1;
        public int conditionId { get; set; } = 1;
        public int shippingId { get; set; } = 1;
    }
}
