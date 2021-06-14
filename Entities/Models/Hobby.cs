using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Hobby
    {
        [Column("HobbyId")]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Contact> Contacts { get; set; } 
    }
}
