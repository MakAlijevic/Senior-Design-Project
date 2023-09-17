using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly DataContext context;
        public OrderProductRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderProduct>> GetOrderProducts(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order does not exist");
            }
            var orderProducts = await context.OrderProduct
                .Include(x => x.Order)
                .Include(x => x.Product)
                .Where(x => x.Order.Id == id)
                .ToListAsync();

            if (!orderProducts.Any())
            {
                throw new Exception("User does not have items in cart");
            }
            return orderProducts;
        }

        public async Task<OrderProduct> GetOrderProduct(int id)
        {
            var orderProduct = await context.OrderProduct.FindAsync(id);
            return orderProduct;
        }

        public async Task<OrderProduct> AddOrderProduct(OrderProduct orderProduct)
        {
            context.OrderProduct.Add(orderProduct);
            await context.SaveChangesAsync();
            
            return await GetOrderProduct(orderProduct.Id);
        }
    }
}
