using Microsoft.AspNetCore.Mvc;
using AP_ShopBE.DAL.Model;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.BLL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(await userService.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser (int id)
        {
            try
            {
                return (await userService.GetUser(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<User>> AddUser(UserRegisterDto request)
        {
            try
            {
                return Ok(await userService.AddUser(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(UserRegisterDto request, int id)
        {
            try
            {
                await userService.UpdateUser(request, id);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
            return Ok(await userService.GetUser(id));
        }

        [HttpDelete("{id}"), Authorize(Roles = "2")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {

            try
            {
                await userService.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(await userService.GetUsers());
        }
    }
}
