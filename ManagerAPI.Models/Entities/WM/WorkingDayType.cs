using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Entities.WM
{
    public class WorkingDayType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public bool DayIsActive { get; set; }

        public virtual ICollection<WorkingDay> WorkingDays { get; set; }
    }
}
