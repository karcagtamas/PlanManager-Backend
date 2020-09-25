using System.Threading.Tasks;

namespace ManagerAPI.Services.Common.Mail
{
    public interface IMailService
    {
        Task SendEmailAsync(Mail mail);
    }
}