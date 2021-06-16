using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync(bool trackChenges, RequestParameters organizationParameters);
        Task<Organization> GetOrganizationAsync(Guid organizationId, RequestParameters organizationParameters, bool trackChenges);
        void CreateOrganization(Organization organization);
        void DeleteOrganization(Organization organization);
    }
}
