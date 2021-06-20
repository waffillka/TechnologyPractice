using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(RepositoryContext _repository)
            : base(_repository)
        {
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(Guid organizationId, RequestParameters contactParameters, bool trackChanges)
        {
            var contact = await FindByCondition(x => x.OrganizationId.Equals(organizationId), contactParameters, trackChanges)
            .OrderBy(x => x.LastName)
            .ToListAsync();

            return PagedList<Contact>.ToPagedList(contact, contactParameters.PageNumber, contactParameters.PageSize);
        }

        public async Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, RequestParameters contactParameters, bool trackChanges) =>
           await FindByCondition(x => x.OrganizationId.Equals(organizationId) && x.Id.Equals(id), contactParameters, trackChanges)
           .FirstOrDefaultAsync();

        public void CreateContact(Guid organizationId, Contact contact)
        {
            contact.OrganizationId = organizationId;
            Create(contact);
        }

        public void DeleteContact(Contact contact) => Delete(contact);

        public void CreateCollectionContacts(Guid organizationId, IEnumerable<Contact> contacts)
        {
            foreach(var contact in contacts)
            {
                CreateContact(organizationId, contact);
            }
        }
    }
}
