using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.WM
{
    public class WorkingDayType : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public bool DayIsActive { get; set; }

        public virtual ICollection<WorkingDay> WorkingDays { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((WorkingDayType)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, DayIsActive);
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Title}";
        }
    }
}
