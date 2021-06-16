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
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(RepositoryContext _repository)
            : base(_repository)
        {
        }

        public void CreateOrganization(Organization organization) => Create(organization);

        public void DeleteOrganization(Organization organization) => Delete(organization);

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync(bool trackChenges, RequestParameters organizationParameters) =>
            await FindAll(organizationParameters, trackChenges)
            .ToListAsync();

        public async Task<Organization> GetOrganizationAsync(Guid organizationId, RequestParameters organizationParameters, bool trackChenges) =>
           await FindByCondition(x => x.Id.Equals(organizationId), organizationParameters, trackChenges)
           .SingleOrDefaultAsync();

    }
}
