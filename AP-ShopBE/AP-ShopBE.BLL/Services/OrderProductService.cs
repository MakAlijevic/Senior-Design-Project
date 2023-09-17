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
    public class OrderProductService : IOrderProductService
    {
        private IOrderProductRepository orderProductRepository;
        private IOrderService orderService;
        private IProductService productService;
        public OrderProductService(IOrderProductRepository orderProductRepository, IOrderService orderService, IProductService productService)
        {
            this.orderProductRepository = orderProductRepository;
            this.orderService = orderService;
            this.productService = productService;
        }

        public Task<List<OrderProduct>> GetOrderProducts(int id)
        {
            return orderProductRepository.GetOrderProducts(id);
        }

        public async Task<OrderProduct> AddOrderProduct(OrderProductDto orderProduct)
        {
            var newOrderProduct = new OrderProduct
            {
                Order = await this.orderService.GetOrder(orderProduct.OrderId),
                OrderId= orderProduct.OrderId,
                Product = await this.productService.GetProduct(orderProduct.ProductId),
                ProductId= orderProduct.ProductId,
                Quantity= orderProduct.Quantity
            };
            return await this.orderProductRepository.AddOrderProduct(newOrderProduct);  
        }
    }
}
