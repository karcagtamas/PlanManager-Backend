using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities
{
    public class Movie {
        public int Id { get; set; }

        [Required]
        [MaxLength (150)]
        public string Title { get; set; }

        [MaxLength (999)]
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }

        public string CreaterId { get; set; }
        public virtual User Creater { get; set; }
    }
}
