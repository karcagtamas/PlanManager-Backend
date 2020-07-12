using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.WM
{
    public class WorkingField 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Length { get; set; }

        [Required]
        public int WorkingDayId { get; set; }

        public virtual WorkingDay WorkingDay { get; set; }
    }
}
