using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private IShippingService shippingService;
        public ShippingController(IShippingService shippingService)
        {
            this.shippingService = shippingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Shipping>>> GetShippings()
        {
            return Ok(await shippingService.GetShippings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Shipping>>> GetShipping(int id)
        {
            try
            {
                return Ok(await shippingService.GetShippings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
