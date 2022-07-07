using System.Threading.Tasks;
using diplomski_backend.Dtos;
using diplomski_backend.Services.Settings;

namespace diplomski_backend.Services.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestDto mailRequest, string password);
    }
}