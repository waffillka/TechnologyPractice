using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repository;
        private IOrganizationRepository _organizationRepository;
        private IContactRepository _contactRepository;
        private IHobbyRepository _hobbyRepository;

        public RepositoryManager(RepositoryContext repository)
        {
            _repository = repository;
        }

        public IOrganizationRepository Organizations
        {
            get
            {
                if (_organizationRepository == null)
                    _organizationRepository = new OrganizationRepository(_repository);

                return _organizationRepository;
            }
        }

        public IContactRepository Contacts
        {
            get
            {
                if (_contactRepository == null)
                    _contactRepository = new ContactRepository(_repository);

                return _contactRepository;
            }
        }

        public IHobbyRepository Hybbies
        {
            get
            {
                if (_hobbyRepository == null)
                    _hobbyRepository = new HobbyRepository(_repository);

                return _hobbyRepository;
            }
        }

        public Task SaveAsync() => _repository.SaveChangesAsync();
    }
}
