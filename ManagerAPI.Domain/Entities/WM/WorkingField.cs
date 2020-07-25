using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities.WM
{
    public class WorkingField : IEntity
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

        public override bool Equals(object obj)
        {
            return obj != null && this.Id == ((WorkingField)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Description, Length, WorkingDayId);
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Title}";
        }
    }
}
