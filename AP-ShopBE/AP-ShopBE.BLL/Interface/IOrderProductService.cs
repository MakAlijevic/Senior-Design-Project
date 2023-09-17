using AP_ShopBE.BLL.DTO;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface IOrderProductService
    {
        Task<List<OrderProduct>> GetOrderProducts(int id);

        Task<OrderProduct> AddOrderProduct(OrderProductDto orderProduct);
    }
}
