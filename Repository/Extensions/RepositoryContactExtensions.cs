using Entities.Models;
using Entities.RequestFeatures;

namespace Repository.Extensions
{
    public static class RepositoryContactExtensions
    {
        public static bool Filtring(this Contact contact, ContactParameters contactParameters)
        {
            return contact.CountLetters >= contactParameters.CountLettersMin && contact.CountLetters < contactParameters.CountLettersMax;
        }
    }
}
