using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return Ok(await categoryService.GetCategories());
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                await categoryService.GetCategory(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await categoryService.GetCategory(id));
        }

        [HttpGet("GetChildCategories/{id}")]
        public async Task<ActionResult<List<Category>>> GetChildCategories(int id)
        {
            try
            {
                await categoryService.GetChildCategories(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await categoryService.GetChildCategories(id));
        }

        [HttpGet("GetParentCategory/{id}")]
        public async Task<ActionResult<Category>> GetParentCategory(int id)
        {
            try
            {
                await categoryService.GetParentCategory(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(await categoryService.GetParentCategory(id));
        }
    }
}
