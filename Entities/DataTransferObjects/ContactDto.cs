using Entities.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int CountLetters { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Hobby> Hobbies { get; set; }
    }
}
