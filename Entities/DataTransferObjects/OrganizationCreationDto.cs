using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OrganizationCreationDto
    {
        [Required(ErrorMessage = "Organization name is a required field.")]
        [MaxLength(50, ErrorMessage = "Max Length for Name is 50 characters.")]
        public string Name { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
