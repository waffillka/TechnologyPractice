using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class HobbyRepository : RepositoryBase<Organization>, IHobbyRepository
    {
        public HobbyRepository(RepositoryContext _repository)
            : base(_repository)
        {
        }
    }
}
