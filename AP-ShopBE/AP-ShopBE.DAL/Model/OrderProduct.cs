using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Model
{
    public class OrderProduct
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
