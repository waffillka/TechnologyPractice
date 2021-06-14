using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync(bool trackChenges);
        Task<Organization> GetOrganizationAsync(Guid organizationId, bool trackChenges);
    }
}
