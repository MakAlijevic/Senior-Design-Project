namespace AP_ShopBE.BLL.DTO
{
    public class CreateUserDto
    {
        public string username { get; set; } = "username";
        public string firstName { get; set; } = "firstName";
        public string lastName { get; set; } = "lastName";
        public string email { get; set; } = "email@email.com";
        public string password { get; set; } = "password123";
        public string gender { get; set; } = "gender";
        public string address { get; set; } = "address 123";
        public int roleId { get; set; } = 1;
        public int countryId { get; set; } = 1;
    }
}
