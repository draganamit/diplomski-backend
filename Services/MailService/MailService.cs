using System.Threading.Tasks;
using diplomski_backend.Dtos;
using diplomski_backend.Services.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace diplomski_backend.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly object SecureSocketOptions;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequestDto mailRequest, string password)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.Email));
            email.Subject = "Naslov";
            var builder = new BodyBuilder();

            builder.HtmlBody = "Poslat je zahtjev za resetovanje Vaše lozinke. Vaš zahthev je uspješan. Možete se prijaviti sa novom lozinkom: <b>" + password + "</b>";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.Username, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}