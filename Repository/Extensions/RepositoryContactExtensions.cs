using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
