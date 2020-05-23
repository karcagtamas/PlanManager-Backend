using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities
{
    public class Gender
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}