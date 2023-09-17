using AP_ShopBE.BLL.DTO;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task<List<Order>> GetUserOrders(int id);
        Task<Order> AddOrder(CreateOrderDto request);
        Task<Order> UpdateOrder(CreateOrderDto request, int id);
        Task<List<Order>> DeleteOrder(int id);
    }
}
