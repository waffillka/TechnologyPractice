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

        public async Task<PagedList<Organization>> GetAllOrganizationsAsync(bool trackChenges, OrganizationParameters organizationParameters)
        {
            var organization = await FindAll(trackChenges)
            .OrderBy(x => x.Name)
            .ToListAsync();

            return PagedList<Organization>.ToPagedList(organization, organizationParameters.PageNumber, organizationParameters.PageSize);
        }

        public async Task<Organization> GetOrganizationAsync(Guid organizationId, bool trackChenges) =>
           await FindByCondition(x => x.Id.Equals(organizationId), trackChenges)
           .SingleOrDefaultAsync();

    }
}
