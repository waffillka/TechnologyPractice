using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync(bool trackChenges, OrganizationParameters organizationParameters);
        Task<Organization> GetOrganizationAsync(Guid organizationId, OrganizationParameters organizationParameters, bool trackChenges);
        Task<IEnumerable<Organization>> GetOrganizationsByIdsAsync(IEnumerable<Guid> organizationIds, OrganizationParameters organizationParameters, bool trackChenges);
        void CreateOrganization(Organization organization);
        void CreateCollectionOrganizations(IEnumerable<Organization> organizations);
        void DeleteOrganization(Organization organization);
    }
}
