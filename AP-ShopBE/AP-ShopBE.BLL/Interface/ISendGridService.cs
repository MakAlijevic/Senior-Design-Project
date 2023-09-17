using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface ISendGridService
    {
        Task SendMail(int userId, int orderId);
    }
}
