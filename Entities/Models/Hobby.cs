using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Hobby
    {
        [Column("HobbyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Hobby name is a required field.")]
        [MaxLength(50, ErrorMessage = "Max Length for Name is 50 characters.")]
        public string Name { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
