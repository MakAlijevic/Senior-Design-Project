using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using AP_ShopBE.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AP_ShopBE.BLL.Services
{
    public class DelayedEmailService
    {
        public DataContext context;
        public DelayedEmailService(DataContext context)
        {
            this.context = context;
        }
        public void Run()
        {
            int seconds = 1 * 1000;

            var timer = new Timer(TimerMethod, null, 0, seconds);

            Console.ReadKey();
        }

        public async void TimerMethod(object o)
        {
            //var cartItems = await context.Cart_Item.ToListAsync();

            var user = await context.Users.Where(x => x.Id == 31).FirstOrDefaultAsync();

            Console.WriteLine(user.isCartNotified);
        }
    }
}
