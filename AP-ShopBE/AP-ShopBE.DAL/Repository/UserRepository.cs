using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            return user;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user = await context.Users
                .Where(x => x.username == username)
                .FirstOrDefaultAsync();

            return user;
        }
        public async Task AddUser(User newUser)
        {
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
        }

        public async Task<User> UpdateUser(User newUser, int id)
        {
            
            context.Users.Update(newUser);
            await context.SaveChangesAsync();

            return await GetUser(id);
        }

        public async Task<List<User>> DeleteUser(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return await GetUsers();
        }

        public async Task<User> UpdateUserCartModify(int id, bool state)
        {
            var user = await context.Users
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            user.isCartNotified = state;
            await context.SaveChangesAsync();

            return await GetUser(id);
        }
    }
}
