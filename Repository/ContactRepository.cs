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

        public async Task<PagedList<Contact>> GetContactsAsync(Guid organizationId, ContactParameters contactParameters, bool trackChanges)
        {
            var contact = await FindByCondition(x => x.OrganizationId.Equals(organizationId), trackChanges)
            .OrderBy(x => x.LastName)
            .ToListAsync();

            return PagedList<Contact>.ToPagedList(contact, contactParameters.PageNumber, contactParameters.PageSize);
        }

        public async Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, bool trackChanges) =>
           await FindByCondition(x => x.OrganizationId.Equals(organizationId) && x.Id.Equals(id), trackChanges)
           .FirstOrDefaultAsync();
    }
}
