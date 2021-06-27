using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByName(string username);
    }
}
