using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRoles();
        Task<Role> GetRole(int id);
    }
}
