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
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(RepositoryContext _repository)
            : base(_repository)
        {
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync(bool trackChenges) =>
            await FindAll(trackChenges)
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<Organization> GetOrganizationAsync(Guid organizationId, bool trackChenges) =>
           await FindByCondition(x => x.Id.Equals(organizationId), trackChenges)
           .SingleOrDefaultAsync();

    }
}
