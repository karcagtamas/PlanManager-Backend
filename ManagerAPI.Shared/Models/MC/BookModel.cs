using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.MC
{
    public class BookModel
    {
        [Required]
        [MaxLength (150)]
        public string Name { get; set; }

        [Required]
        [MaxLength (150)]
        public string Author { get; set; }

        public string Description { get; set; }
        
        public DateTime? Publish { get; set; }
    }
}