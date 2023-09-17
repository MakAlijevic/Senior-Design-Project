using AP_ShopBE.BLL.DTO;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository orderRepository;
        private IUserRepository userRepository;
        private ISendGridService sendGridService;
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, ISendGridService sendGridService)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.sendGridService = sendGridService;
        }
        public async Task<List<Order>> GetOrders()
        {
            return await orderRepository.GetOrders();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await orderRepository.GetOrder(id);
        }

        public async Task<Order> AddOrder(CreateOrderDto request)
        {
            var newOrder = new Order
            {
                User = await userRepository.GetUser(request.userId),
                totalPrice = request.totalPrice,
                dateTime = DateTime.Now,
            };
            var createdOrder = await orderRepository.AddOrder(newOrder);

            await sendGridService.SendMail(createdOrder.User.Id, createdOrder.Id);

            return createdOrder;
        }

        public async Task<Order> UpdateOrder(CreateOrderDto request, int id)
        {
            var newOrder = new Order
            {
                User = await userRepository.GetUser(request.userId),
                totalPrice = request.totalPrice,
            };

            return await orderRepository.UpdateOrder(newOrder, id);
        }

        public async Task<List<Order>> DeleteOrder(int id)
        {
            var order = await GetOrder(id);

            return await orderRepository.DeleteOrder(order);
        }

        public async Task<List<Order>> GetUserOrders(int id)
        {
            return await this.orderRepository.GetUserOrders(id);
        }
    }
}
