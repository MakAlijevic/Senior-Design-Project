using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryService countryService;
        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            try
            {
                await countryService.GetCountries();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(await countryService.GetCountries());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            try
            {
                await countryService.GetCountry(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(await countryService.GetCountry(id));
        }
    }
}
