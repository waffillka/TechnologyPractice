using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required field.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        [Phone(ErrorMessage = "Phone is invalid.")]
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
