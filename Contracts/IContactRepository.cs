using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContactsAsync(Guid organizationId, bool trackChanges);
        Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, bool trackChanges);
    }
}
