using AP_ShopBE.BLL.DTO;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AP_ShopBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private IUserService userService;

        public AuthController(IConfiguration configuration, IUserService userService) 
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<ActionResult<User>> Register(CreateUserDto request)
        {
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserRegisterDto
            {
                username = request.username,
                firstName = request.firstName,
                lastName = request.lastName,
                email = request.email,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                gender = request.gender,
                address = request.address,
                roleId = 1,
                countryId = request.countryId
            };
            try
            {
                return Ok(await userService.AddUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            try
            {
                var user = await userService.GetUserByUsername(request.Username);

                if (!VerifyPasswordHash(request.Password, user.passwordHash, user.passwordSalt))
                {
                    return BadRequest("Wrong password");
                }

                string token = CreateToken(user);

                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim(ClaimTypes.SerialNumber, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
