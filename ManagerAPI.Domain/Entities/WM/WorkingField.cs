using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.WM
{
    public class WorkingField 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Length { get; set; }

        [Required]
        public int WorkingDayId { get; set; }

        public virtual WorkingDay WorkingDay { get; set; }
    }
}
