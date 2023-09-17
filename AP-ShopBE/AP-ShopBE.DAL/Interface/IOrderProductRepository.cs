using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface IOrderProductRepository
    {
        Task<List<OrderProduct>> GetOrderProducts(int id);

        Task<OrderProduct> GetOrderProduct(int id);

        Task<OrderProduct> AddOrderProduct(OrderProduct orderProduct);

    }
}
