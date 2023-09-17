using AP_ShopBE.BLL.DTO;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> AddUser(UserRegisterDto request);
        Task<User> UpdateUser(UserRegisterDto request, int id);
        Task<List<User>> DeleteUser(int id);
        Task<User> UpdateUserCartModify(int id, bool state);
    }
}
