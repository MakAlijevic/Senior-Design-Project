using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext context;
        public CountryRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Country>> GetCountries()
        {
            return await context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountry(int id)
        {
            var country = await context.Countries.FindAsync(id);
            if (country == null)
            {
                throw new Exception("Country doesn't exist");
            }
            return country;
        }
    }
}
