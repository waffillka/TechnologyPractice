using Contracts;
using Entities;
using Entities.Models;
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

        public async Task<IEnumerable<Contact>> GetContactsAsync(Guid organizationId, bool trackChanges) =>
            await FindByCondition(x => x.OrganizationId.Equals(organizationId), trackChanges)
            .OrderBy(x => x.LastName)
            .ToListAsync();

        public async Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, bool trackChanges) =>
           await FindByCondition(x => x.OrganizationId.Equals(organizationId) && x.Id.Equals(id), trackChanges)
           .FirstOrDefaultAsync();
    }
}
