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
        Task<IEnumerable<Contact>> GetContactsAsync(Guid organizationId, RequestParameters contactParameters, bool trackChanges);
        Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, RequestParameters contactParameters,  bool trackChanges);
        void CreateContact(Guid organizationId, Contact contact);
        void DeleteContact(Contact contact);
        void CreateCollectionContacts(Guid organizationId, IEnumerable<Contact> contacts);
    }
}
