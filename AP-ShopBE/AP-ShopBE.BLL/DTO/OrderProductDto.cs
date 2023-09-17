using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.DTO
{
    public class OrderProductDto
    {
        public int OrderId { get; set; } = 1;

        public int ProductId { get; set; } = 1;

        public int Quantity { get; set; } = 1;
    }
}
