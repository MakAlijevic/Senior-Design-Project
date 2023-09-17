using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AP_ShopBE.DAL.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string categoryName { get; set; }
        public int? parentId { get; set; }
        [JsonIgnore]
        [ForeignKey("parentId")]
        public virtual List<Category> Subcategories { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set;}
    }
}
