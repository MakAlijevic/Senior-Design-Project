using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace AP_ShopBE.DAL.Interface

{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByUsername(string username);
        Task AddUser(User newUser);
        Task<User> UpdateUser(User newUser, int id);
        Task<List<User>> DeleteUser(User user);
        Task<User> UpdateUserCartModify(int id, bool state);
    }
}