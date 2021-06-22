using System;

namespace Entities.DataTransferObjects
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int CountLetters { get; set; }
    }
}
