using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.DTO
{
    public class UserRegisterDto
    {
        public string username { get; set; } = "username";
        public string firstName { get; set; } = "firstName";
        public string lastName { get; set; } = "lastName";
        public string email { get; set; } = "email@email.com";
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string gender { get; set; } = "gender";
        public string address { get; set; } = "address 123";
        public int roleId { get; set; } = 1;
        public int countryId { get; set; } = 1;
    }
}
