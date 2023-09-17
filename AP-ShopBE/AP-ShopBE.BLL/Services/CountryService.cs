using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Services
{
    public class CountryService : ICountryService
    {
        private ICountryRepository countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public async Task<List<Country>> GetCountries()
        {
            return await countryRepository.GetCountries();
        }

        public async Task<Country> GetCountry(int id)
        {
            return await countryRepository.GetCountry(id);
        }
    }
}
