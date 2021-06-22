using Entities.EmailServiceModels;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmailService
    {
        Task SendAsync(string email, string name, string topic, string message);

        Task SendAsync(MessageContext message);
    }
}
