using Microsoft.Extensions.Options;
using Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class SmtpService
    {
        private readonly SmtpOptions _options;
        public SmtpService(IOptions<SmtpOptions> options) 
        {
            _options = options.Value;
        }

        public void SendMail2Step(string To, string Subject = default, string Body = default)
        {
            var smtpClient = new SmtpClient(_options.Server, _options.Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true
            };
            smtpClient.Credentials = new NetworkCredential(_options.Email, _options.Password);
            var message = new MailMessage(new MailAddress(_options.Email, "Shiki admin"), new MailAddress(To, To));
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = true;
            smtpClient.Send(message);

        }
    }
}
