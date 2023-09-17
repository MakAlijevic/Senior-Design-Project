using Microsoft.AspNetCore.Mvc;
using AP_ShopBE.DAL.Model;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.BLL.DTO;
using AP_ShopBE.BLL.Interface;
using Microsoft.AspNetCore.Authorization;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet] 
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await productService.GetProducts());
        }

        [HttpGet("sort/{sortingType}")]
        public async Task<ActionResult<List<Product>>> GetProductsSorted(int sortingType)
        {
            return Ok(await productService.GetProductsSorted(sortingType));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProduct(int id)
        {
            try
            {
                var response = await productService.GetProduct(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await productService.GetProduct(id));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> GetFullSearchProducts([FromQuery]SearchDto searchCriteria)
        {
            return Ok(await productService.GetFullSearchProducts(searchCriteria));
        }

        [HttpGet("search/{searchParams}")]
        public async Task<ActionResult<List<Product>>> GetSearchProducts(string searchParams, int sortingType)
        {
            if(searchParams == null && sortingType == null)
            {
                return Ok(await productService.GetProductsSorted(sortingType));
            }
            return Ok(await productService.GetSearchProducts(searchParams, sortingType));
        }

        [HttpPost, Authorize(Roles = "2")]
        public async Task<ActionResult<List<Product>>> AddProduct (CreateProductDto request)
        {
            try
            {
                var response = await productService.AddProduct(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await productService.GetProducts());
        }

        [HttpPut("{id}"), Authorize(Roles = "2")]
        public async Task<ActionResult<Product>> UpdateProduct(CreateProductDto request, int id)
        {
            try
            {
                var response = await productService.UpdateProduct(request, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await productService.GetProduct(id));
        }

        [HttpDelete("{id}"), Authorize(Roles = "2")]
        public async Task<ActionResult<List<Product>>> DeleteProduct (int id)
        {
            try
            {
                var response = await productService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await productService.GetProducts());
        }

        [HttpPut("modify/{id}")]
        public async Task<ActionResult<Product>> UpdateIsModified(int id)
        {
            return Ok(await productService.UpdateIsModified(id));
        }

        [HttpGet("totalSearchedProducts")]
        public async Task<ActionResult<int>> GetProductCount([FromQuery] SearchDto searchCriteria)
        {
            return Ok(await productService.GetProductCount(searchCriteria));
        }
    }
}
