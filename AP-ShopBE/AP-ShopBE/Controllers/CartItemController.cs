using AP_ShopBE.BLL.DTO;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private ICartItemService cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<List<CartItem>>> GetUserProducts(int id)
        {
            try
            {
                return Ok(await cartItemService.GetUserProducts(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> AddUserProduct([FromBody]CartItemDto cartItem)
        {
            return Ok(await cartItemService.AddUserProduct(cartItem));
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<CartItem>>> RemoveUserProduct(int id)
        {
            return Ok(await this.cartItemService.RemoveUserProduct(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartItem>> UpdateUserProductQuantity(int id, int quantity)
        {
            try
            {
            return Ok(await this.cartItemService.UpdateUserProductQuantity(id, quantity));
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cashout"), Authorize]
        public async Task<ActionResult<List<CartItem>>> BuyItems([FromBody]int[] cartItems)
        {
            try
            {
                return Ok(await this.cartItemService.BuyItems(cartItems));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("selected"), Authorize]
        public async Task<ActionResult<List<CartItem>>> GetSelectedCartItems([FromBody] int[] cartItems)
        {
            return Ok(await this.cartItemService.GetSelectedCartItems(cartItems));
        }

        [HttpPost("buyNow"), Authorize]
        public async Task<ActionResult<List<CartItem>>> BuyNow(CartItemDto cartItem)
        {
            try
            {
                return Ok(await this.cartItemService.BuyNow(cartItem));
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
