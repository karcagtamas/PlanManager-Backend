using System.Threading.Tasks;

namespace ManagerAPI.Services.Common
{
    public interface IMailService
    {
        Task SendEmailAsync(Mail mail);
    }
}