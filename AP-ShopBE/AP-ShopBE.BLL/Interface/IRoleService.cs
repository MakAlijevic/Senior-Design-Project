using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface IRoleService
    {
        Task<List<Role>> GetRoles();
        Task<Role> GetRole(int id);
    }
}
