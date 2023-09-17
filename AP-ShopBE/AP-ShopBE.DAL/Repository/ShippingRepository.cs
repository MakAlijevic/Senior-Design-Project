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
    public class ShippingRepository : IShippingRepository
    {
        private readonly DataContext context;
        public ShippingRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Shipping>> GetShippings()
        {
            return await context.Shippings.ToListAsync();
        }

        public async Task<Shipping> GetShipping(int id)
        {
            var shipping = await context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                throw new Exception("Shipping type doesn't exist");
            }
            return shipping;
        }
    }
}
