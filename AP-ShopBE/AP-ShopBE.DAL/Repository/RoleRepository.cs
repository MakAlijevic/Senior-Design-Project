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
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext context;
        public RoleRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Role>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }
        public async Task<Role> GetRole(int id)
        {
            var role = await context.Roles.FindAsync(id);
            if (role == null)
            {
                throw new Exception("Role doesn't exist");
            }
            return role;
        }
    }
}
