using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext context;
        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await context.Orders.ToListAsync();
        }
        public async Task<Order> GetOrder(int id)
        {
            var order = await context.Orders
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new Exception("Order doesn't exist");
            }

            return order;
        }
        public async Task<List<Order>> GetUserOrders(int id)
        {
            var orders = await context.Orders
                .Where(x => x.userId== id)
                .ToListAsync();

            return orders;
        }
        public async Task<Order> AddOrder(Order newOrder)
        {

            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();

            return await GetOrder(newOrder.Id);
        }
        public async Task<Order> UpdateOrder(Order newOrder, int id)
        {
            var order = await GetOrder(id);

            order.User = newOrder.User;
            order.totalPrice = newOrder.totalPrice;

            await context.SaveChangesAsync();

            return await GetOrder(id);
        }
        public async Task<List<Order>> DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();

            return await GetOrders();
        }
    }
}
