using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class OrganizationParameters : RequestParameters
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
