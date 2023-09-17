using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountry(int id);
    }
}
