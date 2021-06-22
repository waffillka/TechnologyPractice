using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataTransferObjects
{
    public abstract class ContactForManipulationDto
    {
        [Required(ErrorMessage = "Last name is a required field.")]
        [MaxLength(50, ErrorMessage = "Max Length for Last name is 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(50, ErrorMessage = "Max Length for First name is 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CountLetters is a required field.")]
        [Range(0, int.MaxValue, ErrorMessage = "CountLetters is less then 0.")]
        public int CountLetters { get; set; }

        [ForeignKey(nameof(Organization))]
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public ICollection<Hobby> Hobbies { get; set; }
    }
}
