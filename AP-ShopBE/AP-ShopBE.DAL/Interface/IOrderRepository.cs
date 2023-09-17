using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task<List<Order>> GetUserOrders(int id);
        Task<Order> AddOrder(Order newOrder);
        Task<Order> UpdateOrder(Order newOrder, int id);
        Task<List<Order>> DeleteOrder(Order order);
    }
}
