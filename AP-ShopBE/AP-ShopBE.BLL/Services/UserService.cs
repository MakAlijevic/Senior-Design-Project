using AP_ShopBE.BLL.DTO;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private IRoleService roleService;
        private ICountryService countryService;
        public UserService(IUserRepository userRepository, IRoleService roleService, ICountryService countryService)
        {
            this.userRepository = userRepository;
            this.roleService = roleService;
            this.countryService = countryService;
        }

        public async Task<List<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }

        public async Task<User> GetUser(int id)
        {
            return await userRepository.GetUser(id);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user = await userRepository.GetUserByUsername(username);
            if(user == null)
            {
                throw new Exception("User doesn't exist");
            }
            return user;
        }
        public async Task<User> AddUser(UserRegisterDto request)
        {
            var existingUser = await userRepository.GetUserByUsername(request.username);
            if(existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var newUser = new User
            {
                username = request.username,
                firstName = request.firstName,
                lastName = request.lastName,
                email = request.email,
                passwordHash = request.passwordHash,
                passwordSalt = request.passwordSalt,
                gender = request.gender,
                address = request.address,
                Role = await roleService.GetRole(request.roleId),
                Country = await countryService.GetCountry(request.countryId)
            };
            await userRepository.AddUser(newUser);

            var res =  await GetUser(newUser.Id);
            return res;
        }

        public async Task<User> UpdateUser(UserRegisterDto request, int id)
        {
            var newUser = new User
            {
                username = request.username,
                firstName = request.firstName,
                lastName = request.lastName,
                email = request.email,
                passwordHash = request.passwordHash,
                passwordSalt = request.passwordSalt,
                gender = request.gender,
                address = request.address,
                Role = await roleService.GetRole(request.roleId),
                Country = await countryService.GetCountry(request.countryId),
            };

            return await userRepository.UpdateUser(newUser, id);
        }

        public async Task<List<User>> DeleteUser(int id)
        {
            var user = await GetUser(id);

            return await userRepository.DeleteUser(user);
        }

        public async Task<User> UpdateUserCartModify(int id, bool state)
        {
            return await userRepository.UpdateUserCartModify(id, state);
        }
    }
}
