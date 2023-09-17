using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface IShippingRepository
    {
        Task<List<Shipping>> GetShippings();
        Task<Shipping> GetShipping(int id);
    }
}
