using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IOrganizationRepository Organizations { get; }
        IContactRepository Contacts { get; }
        IHobbyRepository Hybbies { get; }
        Task SaveAsync();
    }
}
