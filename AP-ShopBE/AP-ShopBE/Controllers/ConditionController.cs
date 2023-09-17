using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private IConditionService conditionService;
        public ConditionController(IConditionService conditionService)
        {
            this.conditionService = conditionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Condition>>> GetConditions()
        {
            return Ok(await this.conditionService.GetConditions());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Condition>> GetCondition(int id)
        {
            try
            {
                return Ok(await conditionService.GetCondition(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
