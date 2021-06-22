using Contracts;
using Entities;
using Entities.Models;

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
