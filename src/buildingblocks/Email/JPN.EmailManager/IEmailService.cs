using System.Threading.Tasks;
using JPN.EmailService.Model;

namespace JPN.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailModel email);
    }
}