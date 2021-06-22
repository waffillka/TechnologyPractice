using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(RepositoryContext _repository)
            : base(_repository)
        {
        }

        public void CreateCollectionOrganizations(IEnumerable<Organization> organizations)
        {
            foreach (var organization in organizations)
            {
                CreateOrganization(organization);
            }
        }

        public void CreateOrganization(Organization organization) => Create(organization);

        public void DeleteOrganization(Organization organization) => Delete(organization);

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync(bool trackChenges, OrganizationParameters organizationParameters) =>
            await FindByCondition(x => x.Name.Contains(organizationParameters.SearchTerm), organizationParameters, trackChenges)
            .ToListAsync();

        public async Task<Organization> GetOrganizationAsync(Guid organizationId, OrganizationParameters organizationParameters, bool trackChenges) =>
           await FindByCondition(x => x.Id.Equals(organizationId), organizationParameters, trackChenges)
           .SingleOrDefaultAsync();

        public async Task<IEnumerable<Organization>> GetOrganizationsByIdsAsync(IEnumerable<Guid> organizationIds, OrganizationParameters organizationParameters, bool trackChenges) =>
            await FindByCondition(x => organizationIds.Contains(x.Id), organizationParameters, trackChenges)
            .ToListAsync();
    }
}
