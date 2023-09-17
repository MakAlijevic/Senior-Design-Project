using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Model
{
    public class CartItem
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime dateTime { get; set; }
    }
}
