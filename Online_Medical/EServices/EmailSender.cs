using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration; 

namespace Online_Medical.EServices
{
    public class EmailSender : IEmailSender
    {
       
        private readonly IConfiguration _configuration;

       
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
           
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var appPassword = _configuration["EmailSettings:AppPassword"];

          
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword)
            };

            var fromAddress = new MailAddress(senderEmail, "Sehatk Live Support");

            var mailMessage = new MailMessage
            {
                From = fromAddress,
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}