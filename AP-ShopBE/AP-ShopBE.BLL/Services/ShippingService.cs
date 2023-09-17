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
    public class ShippingService : IShippingService
    {
        private IShippingRepository shippingRepository;
        public ShippingService(IShippingRepository shippingRepository)
        {
            this.shippingRepository = shippingRepository;
        }
        public Task<List<Shipping>> GetShippings()
        {
            return shippingRepository.GetShippings();
        }
        public Task<Shipping> GetShipping(int id)
        {
            return shippingRepository.GetShipping(id);
        }

    }
}
