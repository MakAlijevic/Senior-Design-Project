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
    public class RoleService : IRoleService
    {
        private IRoleRepository roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public Task<List<Role>> GetRoles()
        {
            return roleRepository.GetRoles();
        }
        public Task<Role> GetRole(int id)
        {
            return roleRepository.GetRole(id);
        }

    }
}
