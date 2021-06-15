using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContactRepository
    {
        Task<PagedList<Contact>> GetContactsAsync(Guid organizationId, ContactParameters contactParameters, bool trackChanges);
        Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, bool trackChanges);
    }
}
