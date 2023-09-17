using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : Controller
    {
        private IOrderProductService orderProductService;
        public OrderProductController(IOrderProductService orderProductService)
        {
            this.orderProductService = orderProductService;
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<List<OrderProduct>>> GetOrderProducts(int id)
        {
            try
            {
                return Ok(await orderProductService.GetOrderProducts(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
