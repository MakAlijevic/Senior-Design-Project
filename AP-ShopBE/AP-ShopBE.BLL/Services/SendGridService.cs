using AP_ShopBE.BLL.Interface;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AP_ShopBE.DAL.Interface;

namespace AP_ShopBE.BLL.Services
{
    public class SendGridService : ISendGridService
    {
        private IUserService userService;
        private readonly IConfiguration configuration;
        private IOrderRepository orderRepository;
        public SendGridService(IUserService userService, IConfiguration configuration, IOrderRepository orderRepository)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.orderRepository = orderRepository;
        }
        public async Task SendMail(int userId, int orderId)
        {
            var user = await userService.GetUser(userId);

            //var apiKey = configuration.GetSection("AppSettings:SENDGRID_API_KEY").Value;
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("cargear@gmail.com", "ShopUser");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress(user.email, "User");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);

            var order = await orderRepository.GetOrder(orderId);

            var apiKey = configuration.GetSection("AppSettings:SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mak.alijevic@authoritypartners.com", "CarGear Shop");
            var subject = "CarGear Shop Order Receipt";
            var to = new EmailAddress(user.email, "User");
            var plainTextContent = "";
            var htmlContent = 
                "<h1 style='font-style:italic;'>You successfully made an order!</h1>" +
                "<h2> Your products will arrive in approximately 7 working days!</h2>" + 
                "<br><strong>Total Price: $" + order.totalPrice +".00</strong>" +
                "<h1 style='font-style:italic;'>Thanky You for your purchase!</h1>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
