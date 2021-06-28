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
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(RepositoryContext _repository)
            : base(_repository)
        {
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(Guid organizationId, ContactParameters contactParameters, bool trackChanges) =>
             await FindByCondition(x =>
             x.OrganizationId.Equals(organizationId) &&
             x.CountLetters >= contactParameters.CountLettersMin &&
             x.CountLetters < contactParameters.CountLettersMax &&
             (x.Email.Contains(contactParameters.SearchTerm) || x.FirstName.Contains(contactParameters.SearchTerm) ||
             x.LastName.Contains(contactParameters.SearchTerm)), contactParameters, trackChanges)
            .OrderBy(x => x.LastName)
            .ToListAsync();

        //   return PagedList<Contact>.ToPagedList(contact, contactParameters.PageNumber, contactParameters.PageSize);
        //}

        public async Task<Contact> GetContactByIdAsync(Guid organizationId, Guid id, ContactParameters contactParameters, bool trackChanges) =>
           await FindByCondition(x => x.OrganizationId.Equals(organizationId) && x.Id.Equals(id), contactParameters, trackChanges)
           .FirstOrDefaultAsync();

        public void CreateContact(Guid organizationId, Contact contact)
        {
            contact.OrganizationId = organizationId;
            Create(contact);
        }

        public void DeleteContact(Contact contact) => Delete(contact);

        public void CreateCollectionContacts(Guid organizationId, IEnumerable<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                CreateContact(organizationId, contact);
            }
        }

        public override IQueryable<Contact> FindAll(RequestParameters parameters, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<Contact>()
                .Include(u => u.OrganizationId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .AsNoTracking()
            : RepositoryContext.Set<Contact>()
                .Include(organ => organ.Organization)
                .Include(organ => organ.Organization)
                .Include(hobby => hobby.Hobbies)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        public override IQueryable<Contact> FindByCondition(System.Linq.Expressions.Expression<Func<Contact, bool>> expression, RequestParameters parameters, bool trackChanges) =>
            !trackChanges ? RepositoryContext.Set<Contact>()
                .Where(expression)
                .Include(organ => organ.Organization)
                .Include(hobby => hobby.Hobbies)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .AsNoTracking()
            : RepositoryContext.Set<Contact>()
                .Where(expression)
                .Include(organ => organ.Organization)
                .Include(hobby => hobby.Hobbies)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);
    }
}
