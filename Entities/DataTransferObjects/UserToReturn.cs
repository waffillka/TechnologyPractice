using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class UserToReturn
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required field.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        [Phone(ErrorMessage = "Phone is invalid.")]
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
