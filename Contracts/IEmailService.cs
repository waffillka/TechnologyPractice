using Entities.EmailServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmailService
    {
        Task SendAsync(string email, string name, string topic, string message);

        Task SendAsync(MessageContext message);
    }
}
