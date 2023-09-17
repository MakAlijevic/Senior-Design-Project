using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            try
            {
                await roleService.GetRoles();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            return Ok(await roleService.GetRoles());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                await roleService.GetRole(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return (await roleService.GetRole(id));
        }
    }
}
