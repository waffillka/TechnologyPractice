using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repository;
        private IOrganizationRepository _organizationRepository; 
        private IContactRepository _contactRepository;

        public RepositoryManager(RepositoryContext repository)
        {
            _repository = repository;
        }

        public IOrganizationRepository Organization
        {
            get
            {
                if (_organizationRepository == null)
                    _organizationRepository = new OrganizationRepository(_repository);

                return _organizationRepository;
            }
        }

        public IContactRepository Contact
        {
            get
            {
                if (_contactRepository == null)
                    _contactRepository = new ContactRepository(_repository);

                return _contactRepository;
            }
        }

        public Task SaveAsync() => _repository.SaveChangesAsync();
    }
}
