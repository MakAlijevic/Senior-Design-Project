using AP_ShopBE.DAL.Model;
using AP_ShopBE.DAL;
using Microsoft.AspNetCore.Mvc;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.BLL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return Ok(await orderService.GetOrders());
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<List<Order>>> GetOrder(int id)
        {
            try
            {
                await orderService.GetOrder(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await orderService.GetOrder(id));
        }

        [HttpGet("user/{id}"), Authorize]
        public async Task<ActionResult<List<Order>>> GetUserOrders(int id)
        {
            return await orderService.GetUserOrders(id);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<Order>>> AddOrder(CreateOrderDto request)
        {
            try
            {
                await orderService.AddOrder(request);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await orderService.GetOrders());
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<Order>> UpdateOrder(CreateOrderDto request, int id)
        {
            try
            {
                await orderService.UpdateOrder(request, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await orderService.GetOrder(id));
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            try
            {
                await orderService.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await orderService.GetOrders());
        }
    }
}
